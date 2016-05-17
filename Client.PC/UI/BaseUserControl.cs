using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Events;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FengSharp.OneCardAccess.Client.PC.UI
{
    /// <summary>
    /// BaseUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class BaseUserControl : UserControl, IView
    {
        public BaseUserControl()
        {

        }
        public BaseUserControl(BaseNotificationObject VM)
        {
            this.DataContext = VM;
            var win = Window.GetWindow(this);
            if (win != null)
            {
                win.Closed += Win_Closed;
            }
            Init();
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            UnInit();
        }
        protected virtual void Init()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = this.DataContext as BaseNotificationObject;

                vm.ChangeDataContextEventToken = DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().Subscribe(OnInterChangeDataContext);
                vm.ExceptionEventToken = DefaultEventAggregator.Current.GetEvent<ExceptionEvent>().Subscribe(OnInterException);
                vm.CloseEventToken = DefaultEventAggregator.Current.GetEvent<CloseEvent>().Subscribe(OnInterClose);
                vm.MessageBoxEventToken = DefaultEventAggregator.Current.GetEvent<MessageBoxEvent>().Subscribe(OnInterMessage);
                vm.CreateViewEventToken = DefaultEventAggregator.Current.GetEvent<CreateViewEvent>().Subscribe(OnInterCreateView);
            }
        }

        private void OnInterChangeDataContext(SubscriptionToken sender, ChangeDataContextEventArgs args)
        {
            var vm = this.DataContext as BaseNotificationObject;
            if (sender == vm.CloseEventToken)
            {
                OnChangeDataContext(sender, args);
            }
        }
        protected virtual void OnChangeDataContext(SubscriptionToken sender, ChangeDataContextEventArgs args)
        {
            this.DataContext = args.NewDataContext;
        }

        protected virtual void UnInit()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = this.DataContext as BaseNotificationObject;
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent>().Unsubscribe(vm.ExceptionEventToken);
                DefaultEventAggregator.Current.GetEvent<CloseEvent>().Unsubscribe(vm.CloseEventToken);
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent>().Unsubscribe(vm.MessageBoxEventToken);
                DefaultEventAggregator.Current.GetEvent<CreateViewEvent>().Unsubscribe(vm.CreateViewEventToken);
            }
        }
        private void OnInterException(SubscriptionToken sender, ExceptionEventArgs args)
        {
            var vm = this.DataContext as BaseNotificationObject;
            if (sender == vm.ExceptionEventToken)
            {
                OnException(sender, args);
            }
        }
        protected virtual void OnException(SubscriptionToken sender, ExceptionEventArgs args)
        {
            args.Exception.HandleException(this);
        }
        private void OnInterClose(SubscriptionToken sender, CloseEventArgs args)
        {
            var vm = this.DataContext as BaseNotificationObject;
            if (sender == vm.CloseEventToken)
            {
                OnClose(sender, args);
            }
        }
        protected virtual void OnClose(SubscriptionToken sender, CloseEventArgs args)
        {
            var vm = this.DataContext as BaseNotificationObject;
            switch (args.CloseStyle)
            {
                case CloseStyle.NullClose:
                    Window.GetWindow(this).Close();
                    break;
                case CloseStyle.CancelClose:
                    {
                        var window = Window.GetWindow(this);
                        window.DialogResult = false;
                        window.Close();
                    }
                    break;
                case CloseStyle.OKClose:
                    {
                        var window = Window.GetWindow(this);
                        window.DialogResult = true;
                        window.Close();
                    }
                    break;
                case CloseStyle.DocumentClose:
                    {
                        var uicloseargs = new UICloseDocumentEventArgs(this);
                        uicloseargs.CallBack = this.UnInit;
                        DefaultEventAggregator.Current.GetEvent<UICloseDocumentEvent>().Publish(vm.CloseEventToken, new UICloseDocumentEventArgs(this));
                    }
                    break;
                default:
                    break;
            }
        }
        private void OnInterMessage(SubscriptionToken sender, MessageBoxEventArgs args)
        {
            var vm = this.DataContext as BaseNotificationObject;
            if (sender == vm.MessageBoxEventToken)
            {
                OnMessage(sender, args);
            }
        }
        protected virtual void OnMessage(SubscriptionToken sender, MessageBoxEventArgs args)
        {
            var diaresult = DXMessageBox.Show(this, args.MessageText,
                args.Caption ?? Properties.Resources.Info_Title,
                (MessageBoxButton)args.MsgButton, (MessageBoxImage)args.MsgImage
                );
            if (args.CallBack != null)
                args.CallBack((MsgResult)diaresult, args.Paras);
        }
        private void OnInterCreateView(SubscriptionToken sender, CreateViewEventArgs args)
        {
            var vm = this.DataContext as BaseNotificationObject;
            if (sender == vm.CreateViewEventToken)
            {
                OnCreateView(sender, args);
            }
        }
        protected virtual void OnCreateView(SubscriptionToken sender, CreateViewEventArgs args)
        {
            var window = new BaseRibbonWindow();
            window.Title = args.Title;
            if (args.ViewStyle == ViewStyle.Dialog)
                window.Style = FindResource("DialogWindowStyle") as Style;
            window.Content = args.View;
            window.Owner = Window.GetWindow(this);
            var diaresult = window.ShowDialog();
            if (args.CallBack != null)
                args.CallBack(diaresult);
        }
    }
}
