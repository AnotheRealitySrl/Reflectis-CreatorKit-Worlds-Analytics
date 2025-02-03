namespace Reflectis.SDK.Core.Diagnostics
{
    public class ExperienceStepStartDTO : ExperienceStepDTO
    {
        [SettableField(isRequired = true)]
        public string description;
    }
}
