using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class MainViewModel : BaseNotificationObject, IMainView
    {
        public ICommand ShowDocumentCommand { get; private set; }
        public SubscriptionToken LoginEventSubscriptionToken { get; set; }
        public SubscriptionToken ShowDocumentEventSubscriptionToken { get; set; }
        public SubscriptionToken UICloseDocumentEventSubscriptionToken { get; set; }
        public SubscriptionToken LoginSucessEventSubscriptionToken { get; set; }
        public MainViewModel()
        {
            LoginEventSubscriptionToken = DefaultEventAggregator.Current.GetEvent<LoginEvent>().Subscribe(OnLogin);
            ShowDocumentCommand = new DelegateCommand<DocumentInfo>(ShowDocument);
        }
        public void ShowDocument(DocumentInfo docInfo)
        {
            try
            {
                var vm = ServiceLoader.LoadService<IMainView>();
                DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().Publish(
                    vm.ShowDocumentEventSubscriptionToken,
                    new ShowDocumentEventArgs(docInfo));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        public void OnLogin(SubscriptionToken sender, LoginEventArgs args)
        {
            if (sender != LoginEventSubscriptionToken)
                return;
            bool isReLogin = false;
            switch (args.LoginState)
            {
                case LoginState.ReLogin:
                case LoginState.TimeOutLogin:
                    isReLogin = true;
                    break;
            }
            var vm = ServiceLoader.LoadService<ILoginView>(new ParameterOverride("isReLogin", isReLogin));
            var view = ServiceLoader.LoadService<IView>("LoginView", new ParameterOverride("VM", vm));
            this.CreateView(new CreateViewEventArgs(view, Properties.Resources.View_P_CRTempView_Title));
        }
    }
    #region Defs
    public class DocumentInfo
    {
        public string DocumentTitle { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
    }
    [MarkupExtensionReturnType(typeof(System.Windows.PropertyPath))]
    public class DocumentInfoExtension : MarkupExtension
    {
        public string DocumentTitle { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new DocumentInfo()
            {
                DocumentTitle = this.DocumentTitle,
                DocumentType = this.DocumentType,
                DocumentName = this.DocumentName
            };
        }
    }

    public class ShowDocumentEvent : SenderEvent<ShowDocumentEventArgs>
    {
    }
    public class ShowDocumentEventArgs : NullEventArgs
    {
        public ShowDocumentEventArgs(DocumentInfo documentInfo)
        {
            this.DocumentInfo = documentInfo;
        }
        public DocumentInfo DocumentInfo { get; set; }
    }
    public class UICloseDocumentEvent : SenderEvent<UICloseDocumentEventArgs>
    {

    }

    public class UICloseDocumentEventArgs : NullEventArgs
    {
        public UICloseDocumentEventArgs(object panelContent)
        {
            this.PanelContent = panelContent;
        }
        public object PanelContent { get; set; }

        public UICloseDocumentCallBack CallBack { get; set; }
    }

    public delegate void UICloseDocumentCallBack();

    #endregion
}
