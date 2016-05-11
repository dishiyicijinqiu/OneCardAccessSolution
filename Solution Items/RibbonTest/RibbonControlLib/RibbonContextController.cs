using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNBSoft.WPF.RibbonControl
{
    public class RibbonContextController
    {
        private RibbonController controller = null;
        private Dictionary<IContextObject, List<RibbonBar>> contexts = new Dictionary<IContextObject, List<RibbonBar>>();
        private IContextObject currentContext = null;

        internal RibbonContextController(RibbonController controller)
        {
            this.controller = controller;
        }

        public Dictionary<IContextObject, List<RibbonBar>> Contexts
        {
            get
            {
                return contexts;
            }
        }

        public IContextObject CurrentContext
        {
            get
            {
                return currentContext;
            }
            set
            {
                #region remove old context
                if (currentContext != null)
                {
                    List<RibbonBar> bars = Contexts[currentContext];
                    foreach(RibbonBar bar in bars)
                    {
                        controller.Ribbons.Remove(bar);
                    }
                }
                #endregion

                #region add new context
                if (value == null)
                {
                    currentContext = value;
                }
                else if (!this.Contexts.ContainsKey(value))
                {
                    currentContext = null;
                    throw new Exception("Context not registered");
                }
                else
                {
                    currentContext = value;
                    List<RibbonBar> bars = Contexts[currentContext];
                    foreach (RibbonBar bar in bars)
                    {
                        controller.Ribbons.Add(bar);
                    }
                }
                #endregion
            }
        }
    }
}
