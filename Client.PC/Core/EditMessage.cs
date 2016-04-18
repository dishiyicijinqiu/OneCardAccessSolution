using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.Core
{
    public abstract class EditMessage<T>
    {
        public bool IsContinue { get; set; }
        public EntityEditMode EntityEditMode { get; protected set; }
        public T Key { get; set; }
    }
}
