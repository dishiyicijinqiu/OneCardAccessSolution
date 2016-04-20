using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Core
{
    public interface ILoadDataExceptionSupport
    {
        bool IsLoadDateException { get; }
        void LoadData();
    }
}
