using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Core
{
    public class DefaultViewModelBase : ViewModelBase
    {
        public DefaultViewModelBase()
        {
            this.PropertyChanged += DefaultViewModelBase_PropertyChanged;
        }
        protected bool HasPropertyChanged { get; set; }
        private void DefaultViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasPropertyChanged = true;
        }
    }
}
