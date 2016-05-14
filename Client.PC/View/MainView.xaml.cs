using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.Events;
using System.Windows;
using System.Windows.Controls;
using FengSharp.OneCardAccess.Core;
using DevExpress.Xpf.Core;
using System;
using DevExpress.Xpf.Docking;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using FengSharp.OneCardAccess.Client.PC.UI;

namespace FengSharp.OneCardAccess.Client.PC.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : BaseUserControl
    {
        System.Collections.Generic.Dictionary<object, DocumentPanel> docs = new System.Collections.Generic.Dictionary<object, DocumentPanel>();
        public MainView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.Loaded += MainView_Loaded;
            }
        }
        protected override void Init()
        {
            base.Init();
            DefaultEventAggregator.Current.GetEvent<LoginEvent>().Subscribe(OnLogin);
            DefaultEventAggregator.Current.GetEvent<LoginSuccessEvent>().Subscribe(OnLoginSuccess);
            DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().Subscribe(OnShowDocument);
            DefaultEventAggregator.Current.GetEvent<UICloseDocumentEvent>().Subscribe(OnCloseDocument);
            this.DataContext = new MainViewModel();
        }
        protected override void UnInit()
        {
            base.UnInit();
            DefaultEventAggregator.Current.GetEvent<LoginEvent>().Unsubscribe(OnLogin);
            DefaultEventAggregator.Current.GetEvent<LoginSuccessEvent>().Unsubscribe(OnLoginSuccess);
            DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().Unsubscribe(OnShowDocument);
            DefaultEventAggregator.Current.GetEvent<UICloseDocumentEvent>().Unsubscribe(OnCloseDocument);
        }

        private void CloseDocument(DocumentPanel panel)
        {
            if (panel != null && panel.Content != null)
            {
                OnCloseDocument(new UICloseDocumentEventArgs(panel.Content));
            }
        }

        private void OnCloseDocument(UICloseDocumentEventArgs args)
        {
            if (docs.ContainsKey(args.PanelContent))
            {
                lock (docs)
                {
                    if (docs.ContainsKey(args.PanelContent))
                    {
                        var document = docs[args.PanelContent];
                        dockLayoutManager.DockController.RemovePanel(document);
                        docs.Remove(args.PanelContent);
                    }
                }
            }
        }
        private void OnShowDocument(MainViewModel sender, ShowDocumentEventArgs args)
        {
            if (sender == this.DataContext)
            {
                var doc = dockLayoutManager.DockController.AddDocumentPanel(mdiContainer);
                try
                {
                    var docInfo = args.DocumentInfo;
                    doc.AllowDrag = false;
                    doc.IsActive = true;
                    doc.FloatOnDoubleClick = false;
                    doc.Caption = docInfo.DocumentTitle;
                    var vm = ServiceLoader.LoadService(System.Type.GetType(docInfo.DocumentType));
                    var view = ServiceLoader.LoadService<Interfaces.IView>(docInfo.DocumentName,
                        new Microsoft.Practices.Unity.ResolverOverride[] {
                            new Microsoft.Practices.Unity.ParameterOverride("vm",vm)
                        });
                    doc.Content = view;
                    doc.CloseCommand = new DelegateCommand<DocumentPanel>(CloseDocument);
                    docs.Add(doc.Content, doc);
                }
                catch (Exception ex)
                {
                    dockLayoutManager.DockController.RemovePanel(doc);
                    throw ex;
                }
            }
        }
        bool isfirstlogin = true;
        #region Events
        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isfirstlogin)
                return;
            if (isfirstlogin)
            {
                DefaultEventAggregator.Current.GetEvent<LoginEvent>().Publish(new LoginEventArgs(LoginState.NewLogin));
                isfirstlogin = false;
            }
        }
        #endregion
        #region EventAggregator Events
        private void OnLoginSuccess(NullEventArgs args)
        {
            var mainwindow = Window.GetWindow(this);
            mainwindow.ShowInTaskbar = true;
            mainwindow.Opacity = 100;
        }
        public void OnLogin(LoginEventArgs args)
        {
            var window = new UI.BaseWindow();
            window.Title = Properties.Resources.View_LoginView_Title;
            window.Style = FindResource("LoginWindowStyle") as Style;
            bool isReLogin = false;
            switch (args.LoginState)
            {
                case LoginState.NewLogin:
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    isReLogin = false;
                    break;
                case LoginState.ReLogin:
                    isReLogin = true;
                    break;
                case LoginState.TimeOutLogin:
                    isReLogin = true;
                    break;
                default:
                    break;
            }
            window.ShowInTaskbar = true;
            window.Content = new LoginView(isReLogin);
            window.Owner = Window.GetWindow(this);
            window.ShowDialog();
        }
        #endregion
    }
}
