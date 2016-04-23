using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IMainWindow : IWindow
    {
        IDocManagerService DocManagerService { get; }
    }
    public interface IMainView : IView
    {
        IDocManagerService DocManagerService { get; }
    }
}
