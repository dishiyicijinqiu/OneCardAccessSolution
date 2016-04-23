using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Events;
using System.Windows;
using System.Windows.Controls;
using System;

namespace FengSharp.OneCardAccess.Client.PC.View
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }
        void Init()
        {
            DefaultEventAggregator.Current.GetEvent<ExceptionEvent<LoginViewModel>>().Subscribe(OnException);
            DefaultEventAggregator.Current.GetEvent<LoginedEvent>().Subscribe(OnLogined);
            this.DataContext = new LoginViewModel();
            this.Loaded += LoginView_Loaded;
        }

        private void OnException(LoginViewModel sender, ExceptionEventArgs args)
        {
            if (sender == this.DataContext)
            {
                args.Exception.HandleException(this);
            }
        }
        public void OnLogined(LoginedEventArgs args)
        {
            switch (args.LoginResult)
            {
                case BusinessEntity.RBAC.LoginResult.Success:
                    DefaultEventAggregator.Current.GetEvent<LoginSuccessEvent>().Publish(null);
                    Window.GetWindow(this).Close();
                    break;
                case BusinessEntity.RBAC.LoginResult.UserNotExist:
                    DXMessageBox.Show(this, Client.PC.Properties.Resources.Error_UserNotExist, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                case BusinessEntity.RBAC.LoginResult.UserIsLocked:
                    DXMessageBox.Show(this, Client.PC.Properties.Resources.Error_UserIsLocked, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                case BusinessEntity.RBAC.LoginResult.ErrorPassWord:
                    DXMessageBox.Show(this, Client.PC.Properties.Resources.Error_PassWordIsError, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                case BusinessEntity.RBAC.LoginResult.UserIsEmpty:
                    DXMessageBox.Show(this, Client.PC.Properties.Resources.Error_UserIsEmpty, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }
        }

        private void LoginView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DXSplashScreen.IsActive)
                DXSplashScreen.Close();
            Window loginWindow = Window.GetWindow(this);
            System.Windows.Input.FocusManager.SetFocusedElement(loginWindow, this.tbUserNo);
            loginWindow.Activate();
        }
    }
}
