using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IDocManagerService
    {
        IDoc CreateDoc(string docType, object viewmodel);
    }
}
