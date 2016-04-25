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

namespace FengSharp.OneCardAccess.Client.PC.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<MainViewModel>>().Subscribe(OnException, ThreadOption.UIThread);
                DefaultEventAggregator.Current.GetEvent<LoginEvent>().Subscribe(OnLogin);
                DefaultEventAggregator.Current.GetEvent<LoginSuccessEvent>().Subscribe(OnLoginSuccess);
                DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().Subscribe(OnShowDocument);
                this.Unloaded += MainView_Unloaded;
                this.Loaded += MainView_Loaded;
                this.DataContext = new MainViewModel();
            }
        }

        private void OnShowDocument(MainViewModel sender, ShowDocumentEventArgs args)
        {
            if (sender == this.DataContext)
            {
                var docInfo = args.DocumentInfo;
                var doc = new DocumentPanel();
                doc.AllowDrag = false;
                doc.IsActive = true;
                doc.FloatOnDoubleClick = false;
                doc.Caption = docInfo.DocumentTitle;
                var type = System.Type.GetType(docInfo.DocumentType);
                doc.Content = Activator.CreateInstance(type);
                mdiContainer.Add(doc);
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
        private void MainView_Unloaded(object sender, RoutedEventArgs e)
        {
            DefaultEventAggregator.Current.GetEvent<ExceptionEvent<MainViewModel>>().Unsubscribe(OnException);
            DefaultEventAggregator.Current.GetEvent<LoginEvent>().Unsubscribe(OnLogin);
        }
        #endregion
        #region EventAggregator Events
        private void OnLoginSuccess(NullEventArgs args)
        {
            var mainwindow = Window.GetWindow(this);
            mainwindow.ShowInTaskbar = true;
            mainwindow.Opacity = 100;
        }
        public void OnException(MainViewModel sender, ExceptionEventArgs args)
        {
            if (sender == this.DataContext)
            {
                args.Exception.HandleException(this);
            }
        }
        public void OnLogin(LoginEventArgs args)
        {
            var window = new UI.BaseWindow();
            window.Title = Properties.Resources.View_LoginView_Title;
            window.Style = FindResource("LoginWindowStyle") as Style;
            window.Content = new LoginView();
            window.Owner = Window.GetWindow(this);
            window.ShowDialog();
        }
        #endregion
    }
}
