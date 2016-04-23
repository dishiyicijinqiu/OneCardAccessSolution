using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.Events;
using System.Windows;
using System.Windows.Controls;
using FengSharp.OneCardAccess.Core;
using DevExpress.Xpf.Core;
using System;

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
            DefaultEventAggregator.Current.GetEvent<ExceptionEvent<MainViewModel>>().Subscribe(OnException, ThreadOption.UIThread);
            DefaultEventAggregator.Current.GetEvent<LoginEvent>().Subscribe(OnLogin);
            DefaultEventAggregator.Current.GetEvent<LoginSuccessEvent>().Subscribe(OnLoginSuccess);

            this.Unloaded += MainView_Unloaded;
            this.DataContext = new MainViewModel();
        }

        private void OnLoginSuccess(NullEventArgs args)
        {
            var mainwindow = Window.GetWindow(this);
            mainwindow.ShowInTaskbar = true;
            mainwindow.Opacity = 100;
        }

        private void MainView_Unloaded(object sender, RoutedEventArgs e)
        {
            DefaultEventAggregator.Current.GetEvent<ExceptionEvent<MainViewModel>>().Unsubscribe(OnException);
            DefaultEventAggregator.Current.GetEvent<LoginEvent>().Unsubscribe(OnLogin);
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
            var window = new LoginWindow();
            window.ShowDialog();
        }
    }
}
