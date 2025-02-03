namespace Reflectis.SDK.Core.Diagnostics
{
    public class ExperienceJoinDTO : ExperienceDiagnosticDTO
    {
        [SettableField(isRequired = true)]
        public string context;

    }
}
