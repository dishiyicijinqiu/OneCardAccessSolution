using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Events;
using System.Windows;
using System.Windows.Controls;
using System;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.Interfaces;

namespace FengSharp.OneCardAccess.Client.PC.View
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : BaseUserControl, ILoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }
        public LoginView(LoginViewModel VM) : base(VM)
        {
            InitializeComponent();
            this.Loaded += LoginView_Loaded;
        }
        //protected override void OnClose(SubscriptionToken sender, CloseEventArgs args)
        //{
        //    base.OnClose(sender, args);
        //    if (args.CloseStyle != CloseStyle.OKClose)
        //        Application.Current.Shutdown();
        //}
        protected override void UnInit()
        {

        }
        protected override void OnWindowClosed()
        {
            if (Common.Session.Current == null)
            {
                Application.Current.Shutdown();
            }
        }


        private void LoginView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (DXSplashScreen.IsActive)
                    DXSplashScreen.Close();
                Window loginWindow = Window.GetWindow(this);
                System.Windows.Input.FocusManager.SetFocusedElement(loginWindow, this.tbUserNo);
                loginWindow.Activate();
            }
            catch (Exception ex)
            {
                ex.HandleException(this);
            }
        }
    }
}
