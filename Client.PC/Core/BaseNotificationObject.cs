using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Core
{
    public class BaseNotificationObject<P> : NotificationObject
    {
        private object _ParentViewModel;

        public object ParentViewModel
        {
            get { return _ParentViewModel; }
            set
            {
                _ParentViewModel = value;
                RaisePropertyChanged("ParentViewModel");
            }
        }

        private P _Parameter;
        public P Parameter
        {
            get { return _Parameter; }
            set
            {
                _Parameter = value;
                RaisePropertyChanged("Parameter");
            }
        }
    }
}
