using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FengSharp.OneCardAccess.Client.PC.UI
{
    /// <summary>
    /// BaseUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class BaseUserControl : UserControl
    {
        public BaseUserControl()
        {
            Init();

        }

        protected virtual void Init()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Subscribe(OnException);
                DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Subscribe(OnClose);
                DefaultEventAggregator.Current.GetEvent<CloseDocumentEvent<object>>().Subscribe(OnCloseDocument);
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Subscribe(OnMessage);
            }
        }

        protected virtual void UnInit()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Unsubscribe(OnException);
                DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Unsubscribe(OnClose);
                DefaultEventAggregator.Current.GetEvent<CloseDocumentEvent<object>>().Unsubscribe(OnCloseDocument);
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Unsubscribe(OnMessage);
            }
        }

        protected virtual void OnMessage(object sender, MessageBoxEventArgs args)
        {
            if (sender == this.DataContext)
            {
                var diaresult = DXMessageBox.Show(this, args.MessageText,
                    args.Caption ?? Properties.Resources.Info_Title,
                    (MessageBoxButton)args.MsgButton, (MessageBoxImage)args.MsgImage
                    );
                args.CallBack?.Invoke((MsgResult)diaresult, args.Paras);
            }
        }

        protected virtual void OnClose(object sender, NullEventArgs args)
        {
            if (sender == this.DataContext)
            {
                Window.GetWindow(this).Close();
                UnInit();
            }
        }

        protected virtual void OnCloseDocument(object sender, NullEventArgs args)
        {
            if (sender == this.DataContext)
            {
                DefaultEventAggregator.Current.GetEvent<UICloseDocumentEvent>().Publish(new UICloseDocumentEventArgs(this));
                UnInit();
            }
        }

        protected virtual void OnException(object sender, ExceptionEventArgs args)
        {
            if (sender == this.DataContext)
            {
                args.Exception.HandleException(this);
            }
        }
    }
}
