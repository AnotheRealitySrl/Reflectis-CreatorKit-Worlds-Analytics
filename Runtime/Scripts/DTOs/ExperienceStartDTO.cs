namespace Reflectis.SDK.Core.Diagnostics
{
    public class ExperienceStartDTO : ExperienceDiagnosticDTO
    {
        [SettableField(isRequired = true)]
        public string context;
    }
}
