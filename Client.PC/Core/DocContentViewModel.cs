using System.ComponentModel;
using System;

namespace FengSharp.OneCardAccess.Core
{
    public class DocContentViewModel : IDocContent
    {
        public virtual IDocOwner DocumentOwner { get; set; }

        public virtual object Title { get; }

        public virtual void OnClose(CancelEventArgs e)
        {
        }
        public virtual void OnDestroy()
        {
        }

        public virtual void Close()
        {
            this.DocumentOwner.Close(this);
        }
    }
    public class DefaultViewModel : IViewModel
    {
        public virtual IView View { get; private set; }
        public DefaultViewModel(IView view)
        {
            this.View = view;
        }
    }
}