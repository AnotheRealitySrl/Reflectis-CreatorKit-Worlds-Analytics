namespace Reflectis.SDK.Core.Diagnostics
{
    public abstract class ExperienceDiagnosticDTO : DiagnosticDTO
    {
        [SettableField(isRequired = true)]
        protected string key;

        public string uniqueId;

        public string GetKey()
        {
            return key;
        }
    }
}
