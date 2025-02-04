
using Reflectis.SDK.Core.SystemFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reflectis.CreatorKit.Worlds.Analytics
{
    public interface

        IAnalyticsSystem : ISystem
    {
        public static Dictionary<EAnalyticType, List<EAnalyticVerb>> VerbsTypes =
            new Dictionary<EAnalyticType, List<EAnalyticVerb>>
            {
                {
                    EAnalyticType.Experience,
                    new List<EAnalyticVerb>
                    {
                        EAnalyticVerb.ExpJoin,
                        EAnalyticVerb.ExpStart,
                        EAnalyticVerb.ExpComplete,
                        EAnalyticVerb.StepStart,
                        EAnalyticVerb.StepComplete,
                        EAnalyticVerb.ExpTranscript
                    }
                }
            };

        public static Dictionary<EAnalyticVerb, Type> VerbsDTOs =
            new Dictionary<EAnalyticVerb, Type>
            {
                {
                    EAnalyticVerb.ExpJoin,
                    typeof(ExperienceJoinDTO)
                },
                {
                    EAnalyticVerb.ExpStart,
                    typeof(ExperienceStartDTO)
                },
                {
                    EAnalyticVerb.ExpComplete,
                    typeof(ExperienceCompleteDTO)
                },
                {
                    EAnalyticVerb.StepStart,
                    typeof(ExperienceStepStartDTO)
                },
                {
                    EAnalyticVerb.StepComplete,
                    typeof(ExperienceStepCompleteDTO)
                },
                {
                    EAnalyticVerb.ExpTranscript,
                    typeof(ExperienceTranscriptDTO)
                },
            };

        public static Dictionary<EAnalyticsDisplayableType, Type> DisplayableDataTypes =
            new Dictionary<EAnalyticsDisplayableType, Type>
            {
                {
                    EAnalyticsDisplayableType.Dynamic,
                    typeof(DynamicDisplayableContent)
                }
            };

        Task GenerateExperienceGUID(string key);

        void SendAnalytic(EAnalyticVerb verb, AnalyticDTO AnalyticDTO);
    }
}
