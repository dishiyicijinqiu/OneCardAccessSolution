using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Common
{
    public class DefaultEventAggregator : EventAggregator
    {
        static object lockobj = new object();
        static DefaultEventAggregator _Current;
        public static DefaultEventAggregator Current
        {
            get
            {
                if (_Current == null)
                {
                    lock (lockobj)
                    {
                        if (_Current == null)
                        {
                            _Current = new DefaultEventAggregator();
                        }
                    }
                }
                return _Current;
            }
        }
    }
}
