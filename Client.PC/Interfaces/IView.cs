using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IView
    {
    }
    public interface IWindowView : IView
    {
    }
    public interface IDocumentView : IView
    {
    }
    public interface IWindowAndDocumentView : IWindowView, IDocumentView
    {

    }
}
