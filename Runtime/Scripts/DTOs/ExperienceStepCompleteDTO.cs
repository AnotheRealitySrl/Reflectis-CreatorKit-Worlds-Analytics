namespace Reflectis.CreatorKit.Worlds.Analytics
{
    public class ExperienceStepCompleteDTO : ExperienceStepDTO
    {
        [SettableField]
        public int? score;
        [SettableField]
        public int? maxScore;
        [SettableField]
        public int? passingScore;
        [SettableField]
        public EExperienceOutcome outcome;
        [SettableField]
        public EExperienceScoringType scoringType;
        [SettableField]
        public string notes;
    }
}
