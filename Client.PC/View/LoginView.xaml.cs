using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Events;
using System.Windows;
using System.Windows.Controls;
using System;
using FengSharp.OneCardAccess.Client.PC.UI;

namespace FengSharp.OneCardAccess.Client.PC.View
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : BaseUserControl
    {
        public LoginView(bool isReLogin)
        {
            InitializeComponent();
            var vm = new LoginViewModel(isReLogin);
            this.DataContext = vm;
            vm.Init();
            this.Loaded += LoginView_Loaded;
        }
        protected override void Init()
        {
            base.Init();
            DefaultEventAggregator.Current.GetEvent<LoginedEvent>().Subscribe(OnLogined);
        }
        protected override void UnInit()
        {
            base.UnInit();
            DefaultEventAggregator.Current.GetEvent<LoginedEvent>().Unsubscribe(OnLogined);
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
