using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using System;

namespace FengSharp.OneCardAccess.Core
{
    [POCOViewModel]
    public class DefaultViewModel : ViewModelBase, IDocumentContent
    {
        public DefaultViewModel()
        {
            this.PropertyChanged += DefaultViewModelBase_PropertyChanged;
        }
        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
        }
        protected override void OnInitializeInDesignMode()
        {
            base.OnInitializeInDesignMode();
        }
        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();
        }
        protected override void OnParentViewModelChanged(object parentViewModel)
        {
            base.OnParentViewModelChanged(parentViewModel);
        }

        protected bool HasPropertyChanged { get; set; }

        private void DefaultViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasPropertyChanged = true;
        }
        public virtual IDocumentOwner DocumentOwner { get; set; }

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

        #region Services
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        #endregion
    }
}