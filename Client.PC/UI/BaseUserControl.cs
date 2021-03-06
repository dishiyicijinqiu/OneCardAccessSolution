﻿using DevExpress.Xpf.Core;
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
            this.Loaded += BaseUserControl_Loaded;
            this.InputBindings.Add(new System.Windows.Input.KeyBinding(VM.CloseCommand, System.Windows.Input.Key.Escape, System.Windows.Input.ModifierKeys.None));
            Init();
        }
        bool isloaded = false;
        private void BaseUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (!isloaded)
                {
                    var win = Window.GetWindow(this);
                    if (win != null)
                    {
                        win.Closed += Win_Closed;
                    }
                    isloaded = true;
                }
            }
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            UnInit();
            OnWindowClosed();
        }
        protected virtual void OnWindowClosed()
        {

        }
        protected virtual void Init()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = this.DataContext as BaseNotificationObject;
                vm.CloseEventToken = DefaultEventAggregator.Current.GetEvent<CloseEvent>().Subscribe(OnInterClose);
                vm.ChangeDataContextEventToken = DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().Subscribe(OnInterChangeDataContext);
                vm.ExceptionEventToken = DefaultEventAggregator.Current.GetEvent<ExceptionEvent>().Subscribe(OnInterException);
                vm.MessageBoxEventToken = DefaultEventAggregator.Current.GetEvent<MessageBoxEvent>().Subscribe(OnInterMessage);
                vm.CreateViewEventToken = DefaultEventAggregator.Current.GetEvent<CreateViewEvent>().Subscribe(OnInterCreateView);
            }
        }

        protected virtual void UnInit()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var vm = this.DataContext as BaseNotificationObject;
                DefaultEventAggregator.Current.GetEvent<CloseEvent>().Unsubscribe(vm.CloseEventToken);
                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().Unsubscribe(vm.ChangeDataContextEventToken);
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent>().Unsubscribe(vm.ExceptionEventToken);
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent>().Unsubscribe(vm.MessageBoxEventToken);
                DefaultEventAggregator.Current.GetEvent<CreateViewEvent>().Unsubscribe(vm.CreateViewEventToken);
            }
        }

        private void OnInterClose(SubscriptionToken sender, CloseEventArgs args)
        {
            if (sender == null) return;
            this.Dispatcher.Invoke(new Action(() =>
            {
                var vm = this.DataContext as BaseNotificationObject;
                if (sender == vm.CloseEventToken)
                {
                    OnClose(sender, args);
                }
            }));
        }
        protected virtual void OnClose(SubscriptionToken sender, CloseEventArgs args)
        {
            switch (args.CloseStyle)
            {
                case CloseStyle.DocumentClose:
                    {
                        var uicloseargs = new UICloseDocumentEventArgs(this);
                        uicloseargs.CallBack = this.UnInit;
                        var doctoken = ServiceLoader.LoadService<IMainViewModel>().UICloseDocumentEventSubscriptionToken;
                        DefaultEventAggregator.Current.GetEvent<UICloseDocumentEvent>().Publish(doctoken, uicloseargs);
                    }
                    break;
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
            }
        }
        private void OnInterChangeDataContext(SubscriptionToken sender, ChangeDataContextEventArgs args)
        {
            if (sender == null) return;
            this.Dispatcher.Invoke(new Action(() =>
            {
                var vm = this.DataContext as BaseNotificationObject;
                if (sender == vm.ChangeDataContextEventToken)
                {
                    OnChangeDataContext(sender, args);
                }
            }));
        }
        protected virtual void OnChangeDataContext(SubscriptionToken sender, ChangeDataContextEventArgs args)
        {
            UnInit();
            this.DataContext = args.NewDataContext;
            Init();
        }
        private void OnInterException(SubscriptionToken sender, ExceptionEventArgs args)
        {
            if (sender == null) return;
            this.Dispatcher.Invoke(new Action(() =>
            {
                var vm = this.DataContext as BaseNotificationObject;
                if (sender == vm.ExceptionEventToken)
                {
                    OnException(sender, args);
                }
            }));
        }
        protected virtual void OnException(SubscriptionToken sender, ExceptionEventArgs args)
        {
            args.Exception.HandleException(this);
        }
        private void OnInterMessage(SubscriptionToken sender, MessageBoxEventArgs args)
        {
            if (sender == null) return;
            this.Dispatcher.Invoke(new Action(() =>
            {
                var vm = this.DataContext as BaseNotificationObject;
                if (sender == vm.MessageBoxEventToken)
                {
                    OnMessage(sender, args);
                }
            }));
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
            if (sender == null) return;
            this.Dispatcher.Invoke(new Action(() =>
            {
                var vm = this.DataContext as BaseNotificationObject;
                if (sender == vm.CreateViewEventToken)
                {
                    OnCreateView(sender, args);
                }
            }));
        }
        protected virtual void OnCreateView(SubscriptionToken sender, CreateViewEventArgs args)
        {
            var window = new BaseRibbonWindow();
            if (!string.IsNullOrWhiteSpace(args.Style))
                window.Style = FindResource(args.Style) as Style;
            if (!string.IsNullOrWhiteSpace(args.TitleFormatString))
                window.Title = string.Format(args.TitleFormatString, window.Title);
            window.Content = args.View;
            window.Owner = Window.GetWindow(this);
            window.WindowStartupLocation = (System.Windows.WindowStartupLocation)args.WindowStartupLocation;
            if (args.IsDialog)
            {
                var diaresult = window.ShowDialog();
                args.CallBack?.Invoke(diaresult);
            }
            else
                window.Show();
        }


        protected virtual void Close(CloseStyle closestyle = CloseStyle.NullClose)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                BaseNotificationObject VM = this.DataContext as BaseNotificationObject;
                DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(VM.CloseEventToken, new CloseEventArgs(closestyle));
            }));
        }
    }
}
