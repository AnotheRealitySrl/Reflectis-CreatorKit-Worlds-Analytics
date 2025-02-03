using Reflectis.SDK.Core.Utilities;

using System.Collections.Generic;

namespace Reflectis.SDK.Core.Diagnostics
{
    public abstract class DisplayableContentBase
    {
        public abstract void CheckValidity();

        public abstract void AssignValues(List<Field> args);
    }
}
