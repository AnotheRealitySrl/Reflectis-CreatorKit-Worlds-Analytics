namespace Reflectis.SDK.Core.Diagnostics
{
    public class ExperienceTranscriptDTO : ExperienceStepDTO
    {
        [SettableField(isRequired = true)]
        public string description;
    }
}
