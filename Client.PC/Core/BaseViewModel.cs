using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using FengSharp.OneCardAccess.Client.PC.Core;
using System.ComponentModel;
using System;

namespace FengSharp.OneCardAccess.Core
{
    public class DefaultViewModel : ViewModelBase, ILoadDataExceptionSupport
    {
        public DefaultViewModel() : this(null)
        {
        }
        public DefaultViewModel(object parameter)
        {
            try
            {
                this.Parameter = parameter;
                LoadData();
            }
            catch (Exception ex)
            {
                IsLoadDateException = true;
                MessageBoxService.HandleException(ex);
            }
            this.PropertyChanged += DefaultViewModelBase_PropertyChanged;
        }
        protected bool HasPropertyChanged { get; set; }
        private void DefaultViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasPropertyChanged = true;
        }

        #region Services
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IWindowService DialogWindowService { get { return null; } }
        protected virtual ICurrentWindowService CurrentWindowService { get { return null; } }
        #endregion
        public virtual void LoadData()
        {
            IsLoadData = true;
        }

        public bool IsLoadDateException { get; private set; }
        public bool IsLoadData { get; private set; }
        public virtual void OnLoaded()
        {
            if (IsLoadDateException)
                CurrentWindowService.Close();
        }


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
        public virtual void Close()
        {
            if (DocumentManagerService != null)
            {
                IDocument document = DocumentManagerService.FindDocument(this);
                if (document != null)
                    document.Close();
            };
        }
        public bool IsLoaded { get; protected set; }
    }
}