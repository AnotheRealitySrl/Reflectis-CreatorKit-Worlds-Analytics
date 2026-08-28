using Virtuademy.SDK.Core.Utilities;

using System.Collections.Generic;

namespace Virtuademy.CreatorKit.Worlds.Analytics
{
    public abstract class DisplayableContentBase
    {
        public abstract void CheckValidity();

        public abstract void AssignValues(List<Field> args);
    }
}
