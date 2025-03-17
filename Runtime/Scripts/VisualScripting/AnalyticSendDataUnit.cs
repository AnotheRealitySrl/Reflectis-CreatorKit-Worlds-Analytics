using Reflectis.SDK.Core.SystemFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Unity.VisualScripting;

using UnityEngine;

namespace Reflectis.CreatorKit.Worlds.Analytics
{
    [UnitTitle(UNIT_TITLE)]
    [UnitSurtitle("Reflectis Analytic")]
    [UnitShortTitle("Send Data")]
    [UnitCategory("Reflectis\\Flow")]
    public class AnalyticSendDataUnit : Unit
    {

        public const string UNIT_TITLE = "Reflectis Analytic: Send Data";

        [SerializeAs(nameof(Verb))]
        private EAnalyticVerb verb = EAnalyticVerb.ExpStart;

        [SerializeAs(nameof(SendToXAPI))]
        private bool sendToXAPI = false;

        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable(nameof(verb))]
        public EAnalyticVerb Verb
        {
            get => verb;
            set => verb = value;
        }

        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable(nameof(sendToXAPI))]
        public bool SendToXAPI
        {
            get => sendToXAPI;
            set => sendToXAPI = value;
        }

        //[SerializeAs(nameof(CustomEntriesCount))]
        //private int customEntriesCount;

        //[DoNotSerialize]
        //[Inspectable, UnitHeaderInspectable("Custom entries")]
        //public int CustomEntriesCount
        //{
        //    get => customEntriesCount;
        //    set => customEntriesCount = value;
        //}


        [DoNotSerialize]
        public List<ValueInput> Arguments { get; private set; }
        [DoNotSerialize]
        public List<ValueInput> XAPIArguments { get; private set; }

        //[DoNotSerialize]
        //public List<ValueInput> CustomObjects { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput InputTrigger { get; private set; }
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput OutputTrigger { get; private set; }

        private GameObject gameObject;

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);

            gameObject = instance.gameObject;
        }

        protected override void Definition()
        {
            InputTrigger = ControlInput(nameof(InputTrigger), (f) =>
            {
                //var customObjects = CustomObjects.Select((x) =>
                //{
                //    return f.GetConvertedValue(x) as CustomType;
                //});

                Type type = IAnalyticsSystem.VerbsDTOs[Verb];

                if (type != null)
                {
                    var typeInstance = type.Instantiate();

                    foreach (var argument in Arguments)
                    {
                        if (argument.hasValidConnection || argument.hasDefaultValue)
                        {
                            var value = f.GetConvertedValue(argument);
                            if (value != null)
                            {
                                //Set field value
                                type.GetRuntimeFields().FirstOrDefault(x => x.Name.Equals(argument.key))?.SetValue(typeInstance, value);
                            }
                        }
                    }
                    AnalyticDTO AnalyticDTO = typeInstance as AnalyticDTO;
                    if (sendToXAPI)
                    {
                        var xapiVerb = f.GetConvertedValue(XAPIArguments[0]) as XAPIVerb;
                        var xapiObject = f.GetConvertedValue(XAPIArguments[1]) as XAPIObject;
                        AnalyticDTO.XApiVerb = xapiVerb;
                        AnalyticDTO.XApiObject = xapiObject;
                    }
                    try
                    {
                        SM.GetSystem<IAnalyticsSystem>().SendAnalytic(Verb, AnalyticDTO);
                    }
                    catch (Exception exception)
                    {
                        string message = $"Error during execution of \"{UNIT_TITLE}\" on gameObject {gameObject}: {exception.Message} ";
                        if (IAnalyticsSystem.VerbsTypes[EAnalyticType.Experience].Contains(Verb))
                        {
                            message = message +
                            $"Remember to call the node {AnalyticGenerateExperienceIDUnit.UNIT_TITLE} to generate the ExperienceID before trying to send Analytics data!";
                        }
                        Debug.LogError(message, gameObject);
                    }
                }
                else
                {
                    Debug.LogError("There are no DTOs for the selected VERB");
                }

                return OutputTrigger;
            });

            OutputTrigger = ControlOutput(nameof(OutputTrigger));

            Arguments = new List<ValueInput>();

            Type type = IAnalyticsSystem.VerbsDTOs[Verb];

            if (type != null)
            {
                foreach (var field in type.GetRuntimeFields())
                {
                    var attr = field.GetCustomAttribute<SettableFieldAttribute>();
                    if (attr != null)
                    {
                        ValueInput argument;
                        if (attr.entryType != null)
                        {
                            argument = ValueInput(attr.entryType, field.Name);
                        }
                        else
                        {
                            argument = ValueInput(field.FieldType, field.Name);
                        }
                        //if ((!field.FieldType.IsValueType && !field.FieldType.IsPrimitive && !field.FieldType.IsClass)
                        //|| (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        //|| field.FieldType.IsEnum)

                        if (field.FieldType.IsNullable() && !field.FieldType.IsClass)
                        {
                            argument.unit.defaultValues[field.Name] = null;
                        }
                        else
                        {
                            argument.SetDefaultValue(field.FieldType.Default());
                        }


                        Arguments.Add(argument);
                        if (attr.isRequired)
                        {
                            Requirement(argument, InputTrigger);
                        }
                    }
                }
            }

            if (sendToXAPI)
            {
                XAPIArguments = new List<ValueInput>();
                ValueInput xapiVerb = ValueInput(typeof(XAPIVerb), "XAPIVerb");
                XAPIArguments.Add(xapiVerb);
                Requirement(xapiVerb, InputTrigger);
                ValueInput xapiObject = ValueInput(typeof(XAPIObject), "XAPIObject");
                XAPIArguments.Add(xapiObject);
                Requirement(xapiObject, InputTrigger);
            }

            //CustomObjects = new List<ValueInput>();

            //for (var i = 0; i < CustomEntriesCount; i++)
            //{
            //    var customProperty = ValueInput<CustomType>("Custom_Object_" + i);
            //    CustomObjects.Add(customProperty);
            //    Requirement(customProperty, InputTrigger);
            //}

            Succession(InputTrigger, OutputTrigger);
        }
    }
}
