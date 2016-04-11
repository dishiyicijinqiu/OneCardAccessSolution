using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;

namespace FengSharp.OneCardAccess.Client.PC.Core
{
    public class DefaultViewModel : ViewModelBase
    {
        public DefaultViewModel()
        {
            this.PropertyChanged += DefaultViewModelBase_PropertyChanged;
        }
        protected bool HasPropertyChanged { get; set; }
        private void DefaultViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasPropertyChanged = true;
        }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IWindowService DialogWindowService { get { return null; } }
    }
    public class BaseDocumentContent : BindableBase, ISupportParameter, IDocumentContent
    {
        public BaseDocumentContent()
        {
            this.PropertyChanged += DefaultViewModelBase_PropertyChanged;
        }
        protected bool HasPropertyChanged { get; set; }
        private void DefaultViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasPropertyChanged = true;
        }
        private object _Parameter;
        public virtual object Parameter
        {
            get
            {
                return _Parameter;
            }
            set
            {
                _Parameter = value;
                OnParameterChanged(value);
            }
        }
        public virtual IDocumentOwner DocumentOwner { get; set; }

        public virtual object Title { get; }

        public virtual void OnClose(CancelEventArgs e)
        {
        }
        public virtual void OnDestroy()
        {
        }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IWindowService DialogWindowService { get { return null; } }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } }
        

        public virtual void OnParameterChanged(object parameter)
        {

        }
    }
}