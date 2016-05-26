using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class MainViewModel : BaseNotificationObject, IMainViewModel
    {
        public ICommand ShowDocumentCommand { get; private set; }
        public ICommand ChangePasswordCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public SubscriptionToken LoginEventSubscriptionToken { get; set; }
        public SubscriptionToken ShowDocumentEventSubscriptionToken { get; set; }
        public SubscriptionToken UICloseDocumentEventSubscriptionToken { get; set; }
        public SubscriptionToken LoginSucessEventSubscriptionToken { get; set; }
        public MainViewModel()
        {
            RecentItems = new ObservableCollection<RecentItem>();
            LoginEventSubscriptionToken = DefaultEventAggregator.Current.GetEvent<LoginEvent>().Subscribe(OnLogin);
            ShowDocumentCommand = new DelegateCommand<DocumentInfo>(ShowDocument);
            ChangePasswordCommand = new DelegateCommand(ChangePassword);
            AboutCommand = new DelegateCommand(AboutApp);
            ExitCommand = new DelegateCommand(this.Close);
        }

        private void AboutApp()
        {
            ShowMessage(Properties.Resources.CompanyRight_ComanyName);
        }

        private void ChangePassword()
        {
            try
            {
                var vm = ServiceLoader.LoadService<IChangePassWordViewModel>();
                var view = ServiceLoader.LoadService<IChangePassWordView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "ChangePasswordWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void ShowDocument(DocumentInfo docInfo)
        {
            try
            {
                var vm = ServiceLoader.LoadService<IMainViewModel>();
                DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().Publish(
                    vm.ShowDocumentEventSubscriptionToken,
                    new ShowDocumentEventArgs(docInfo));

                RecentItems.Add(new RecentItem() { DocumentInfo = docInfo, DocumentTitle = docInfo.DocumentTitle, Number = 0 });
                if (RecentItems.Count > 10)
                    RecentItems.RemoveAt(0);
                for (int i = 0; i < RecentItems.Count; i++)
                {
                    RecentItems[i].Number = i + 1;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        public void OnLogin(SubscriptionToken sender, LoginEventArgs args)
        {
            try
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
                var vm = ServiceLoader.LoadService<ILoginViewModel>(new ParameterOverride("isReLogin", isReLogin));
                var view = ServiceLoader.LoadService<ILoginView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "LoginWindowStyle",
                    WindowStartupLocation: WindowStartupLocation.CenterScreen));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }


        public ObservableCollection<RecentItem> RecentItems { get; private set; }
    }
    public class RecentItem
    {
        public int Number { get; set; }
        public string DocumentTitle { get; set; }
        public DocumentInfo DocumentInfo { get; set; }
    }
    #region Defs
    public class DocumentInfo
    {
        public string DocumentTitle { get; set; }
        public string DocumentType { get; set; }
        public string DocumentVMType { get; set; }
        public string DocumentName { get; set; }
    }
    [MarkupExtensionReturnType(typeof(System.Windows.PropertyPath))]
    public class DocumentInfoExtension : MarkupExtension
    {
        public string DocumentTitle { get; set; }
        public string DocumentType { get; set; }
        public string DocumentVMType { get; set; }
        public string DocumentName { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new DocumentInfo()
            {
                DocumentTitle = this.DocumentTitle,
                DocumentType = this.DocumentType,
                DocumentVMType = this.DocumentVMType,
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
