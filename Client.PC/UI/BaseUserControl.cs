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
        public object ParentDataContext { get; set; }
        public object Parameter { get; set; }
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
                DefaultEventAggregator.Current.GetEvent<ChangeDataContextFromParentEvent<object>>().Subscribe(OnParentChangeDataContext);
                DefaultEventAggregator.Current.GetEvent<CloseFromParentEvent<object>>().Subscribe(OnCloseFromParentEvent);
                DefaultEventAggregator.Current.GetEvent<CloseDocumentFromParentEvent<object>>().Subscribe(OnCloseDocumentFromParentEvent);

                //DefaultEventAggregator.Current.GetEvent<CreateViewEvent<object, CreateViewEventArgs<P_CRTempEditMessage, string>, P_CRTempEditMessage, string>>().Subscribe(OnCreateP_CRTempView);
                DefaultEventAggregator.Current.GetEvent<CreateViewEvent<object>>().Subscribe(OnCreateView);
            }
        }

        private void OnCreateView(object sender, CreateViewEventArgs obj)
        {
            if (sender == this.DataContext)
            {
                var window = new BaseRibbonWindow();
                window.Title = obj.Title;
                if (obj.ViewStyle == ViewStyle.Dialog)
                    window.Style = FindResource("DialogWindowStyle") as Style;
                window.Content = obj.View;
                window.Owner = Window.GetWindow(this);
                window.ShowDialog();
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
                DefaultEventAggregator.Current.GetEvent<ChangeDataContextFromParentEvent<object>>().Unsubscribe(OnParentChangeDataContext);
                DefaultEventAggregator.Current.GetEvent<CloseFromParentEvent<object>>().Unsubscribe(OnCloseFromParentEvent);
                DefaultEventAggregator.Current.GetEvent<CloseDocumentFromParentEvent<object>>().Unsubscribe(OnCloseDocumentFromParentEvent);
            }
        }

        private void OnCloseDocumentFromParentEvent(object sender, NullEventArgs args)
        {
            if (sender == null) return;
            if (sender == this.ParentDataContext)
            {
                InterCloseDocument();
            }
        }

        private void OnCloseFromParentEvent(object sender, NullEventArgs args)
        {
            if (sender == null) return;
            if (sender == this.ParentDataContext)
            {
                InterClose();
            }
        }
        private void OnParentChangeDataContext(object sender, ChangeDataContextFromParentEventArgs args)
        {
            if (sender == null) return;
            if (sender == this.ParentDataContext)
            {
                this.DataContext = args.NewDataContext;
            }
        }

        protected virtual void OnMessage(object sender, MessageBoxEventArgs args)
        {
            if (sender == null) return;
            if (sender == this.DataContext)
            {
                var diaresult = DXMessageBox.Show(this, args.MessageText,
                    args.Caption ?? Properties.Resources.Info_Title,
                    (MessageBoxButton)args.MsgButton, (MessageBoxImage)args.MsgImage
                    );
                if (args.CallBack != null)
                    args.CallBack((MsgResult)diaresult, args.Paras);
                //args.CallBack?.Invoke((MsgResult)diaresult, args.Paras);
            }
        }

        protected virtual void OnClose(object sender, NullEventArgs args)
        {
            if (sender == null) return;
            if (sender == this.DataContext)
            {
                InterClose();
            }
        }
        protected void InterClose()
        {
            Window.GetWindow(this).Close();
            UnInit();
        }

        protected virtual void OnCloseDocument(object sender, NullEventArgs args)
        {
            if (sender == null) return;
            if (sender == this.DataContext)
            {
                InterCloseDocument();
            }
        }
        protected void InterCloseDocument()
        {
            DefaultEventAggregator.Current.GetEvent<UICloseDocumentEvent>().Publish(new UICloseDocumentEventArgs(this));
            UnInit();
        }
        protected virtual void OnException(object sender, ExceptionEventArgs args)
        {
            if (sender == null) return;
            if (sender == this.DataContext)
            {
                args.Exception.HandleException(this);
            }
        }

    }
}
