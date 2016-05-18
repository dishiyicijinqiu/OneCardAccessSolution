using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.Events;
using System;
using System.Globalization;

namespace FengSharp.OneCardAccess.Core
{
    public class CreateViewEvent : SenderEvent<CreateViewEventArgs>
    {

    }
    public class CreateViewEventArgs : NullEventArgs
    {
        public CreateViewEventArgs(Client.PC.Interfaces.IView View, string Style = null, bool IsDialog = true, WindowStartupLocation WindowStartupLocation = WindowStartupLocation.CenterOwner)
        {
            this.View = View;
            this.WindowStartupLocation = WindowStartupLocation;
            this.Style = Style;
            this.IsDialog = IsDialog;
        }
        public Client.PC.Interfaces.IView View { get; set; }
        public WindowStartupLocation WindowStartupLocation { get; set; }
        public string Style { get; set; }
        public bool IsDialog { get; set; }
        public CreateViewCallBack CallBack { get; set; }
    }
    public enum WindowStartupLocation
    {
        Manual = 0,
        CenterScreen = 1,
        CenterOwner = 2
    }

    public delegate void CreateViewCallBack(bool? result);

    public class EntityEditedEvent<Sender, Args, Msg, Key> : BaseSenderEvent<Sender, Args> where Args : EntityEditedEventArgs<Msg, Key> where Msg : EditMessage<Key>
    {

    }
    public class EntityEditedEventArgs<T, Key> : NullEventArgs where T : EditMessage<Key>
    {
        public EntityEditedEventArgs(T EditMessage)
        {
            this.EditMessage = EditMessage;
        }
        public T EditMessage { get; set; }
    }


    public class ShutDownEvent : CompositePresentationEvent<ShutDownEventArgs> { }
    public class ShutDownEventArgs : NullEventArgs { }
    public class LoginTimeOutEvent : CompositePresentationEvent<LoginTimeOutEventArgs> { }
    public class LoginTimeOutEventArgs : NullEventArgs
    {
        public LoginTimeOutEventArgs(LoginTimeOutException loginTimeOutException)
        {
            this.LoginTimeOutException = loginTimeOutException;
        }
        public LoginTimeOutException LoginTimeOutException { get; set; }
    }
    public class ExceptionEvent : SenderEvent<ExceptionEventArgs> { }
    public class ExceptionEventArgs : NullEventArgs
    {
        public ExceptionEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
        public Exception Exception { get; set; }
    }
    public class CloseEvent : SenderEvent<CloseEventArgs> { }
    public class CloseEventArgs : NullEventArgs
    {
        public CloseEventArgs(CloseStyle CloseStyle)
        {
            this.CloseStyle = CloseStyle;
        }
        public CloseStyle CloseStyle { get; set; }
    }
    public enum CloseStyle
    {
        NullClose = 0,
        CancelClose = 1,
        OKClose = 2,
        DocumentClose = 3
    }
    public class ChangeDataContextFromParentEvent<Sender> : BaseSenderEvent<Sender, ChangeDataContextFromParentEventArgs> { }
    public class ChangeDataContextFromParentEventArgs : NullEventArgs
    {
        public ChangeDataContextFromParentEventArgs(object NewDataContext)
        {
            this.NewDataContext = NewDataContext;
        }
        public object NewDataContext { get; set; }
    }

    public class ChangeDataContextEvent : SenderEvent<ChangeDataContextEventArgs>
    {

    }
    public class ChangeDataContextEventArgs : NullEventArgs
    {
        public ChangeDataContextEventArgs(BaseNotificationObject NewDataContext)
        {
            this.NewDataContext = NewDataContext;
        }
        public BaseNotificationObject NewDataContext { get; set; }
    }
    public class MessageBoxEvent : SenderEvent<MessageBoxEventArgs> { }
    public class MessageBoxEventArgs : NullEventArgs
    {
        public MessageBoxEventArgs(string messageText, string caption = null,
            MsgButton msgButton = MsgButton.OK, MsgImage msgImage = MsgImage.Information, MessageBoxCallBack callBack = null, params object[] paras)
        {
            this.MessageText = messageText;
            this.Caption = caption;
            this.MsgButton = msgButton;
            this.MsgImage = msgImage;
            this.CallBack = callBack;
            this.Paras = paras;
        }
        public object[] Paras { get; set; }
        public string MessageText { get; set; }
        public string Caption { get; set; }
        public MsgButton MsgButton { get; set; }
        public MsgImage MsgImage { get; set; }
        public MessageBoxCallBack CallBack { get; set; }
    }

    public delegate void MessageBoxCallBack(MsgResult msgResult, params object[] param);

    #region MessageBox
    //
    // 摘要:
    //     指定显示在消息框上的按钮。用作 Overload:System.Windows.MessageBox.Show 方法的参数。
    public enum MsgButton
    {
        //
        // 摘要:
        //     消息框显示“确定”按钮。
        OK = 0,
        //
        // 摘要:
        //     消息框显示“确定”和“取消”按钮。
        OKCancel = 1,
        //
        // 摘要:
        //     消息框显示“是”、“否”和“取消”按钮。
        YesNoCancel = 3,
        //
        // 摘要:
        //     消息框显示“是”和“否”按钮。
        YesNo = 4
    }
    //
    // 摘要:
    //     指定用户单击的消息框按钮。System.Windows.MessageBoxResult 由 Overload:System.Windows.MessageBox.Show
    //     方法返回。
    public enum MsgResult
    {
        //
        // 摘要:
        //     消息框未返回值。
        None = 0,
        //
        // 摘要:
        //     消息框的结果值为“确定”。
        OK = 1,
        //
        // 摘要:
        //     消息框的结果值为“取消”。
        Cancel = 2,
        //
        // 摘要:
        //     消息框的结果值为“是”。
        Yes = 6,
        //
        // 摘要:
        //     消息框的结果值为“否”。
        No = 7
    }
    //
    // 摘要:
    //     指定消息框所显示的图标。
    public enum MsgImage
    {
        //
        // 摘要:
        //     不显示图标。
        None = 0,
        //
        // 摘要:
        //     消息框显示一个“手形”图标。
        Hand = 16,
        //
        // 摘要:
        //     消息框显示一个“停止”图标。
        Stop = 16,
        //
        // 摘要:
        //     消息框显示一个“错误”图标。
        Error = 16,
        //
        // 摘要:
        //     消息框显示一个“问号”图标。
        Question = 32,
        //
        // 摘要:
        //     消息框显示一个“感叹号”图标。
        Exclamation = 48,
        //
        // 摘要:
        //     消息框显示一个“警告”图标。
        Warning = 48,
        //
        // 摘要:
        //     消息框显示一个“星号”图标。
        Asterisk = 64,
        //
        // 摘要:
        //     消息框显示一个“信息”图标。
        Information = 64
    }
    #endregion
    public class NullEvent : SenderEvent<NullEventArgs> { }
    public class NullEventArgs { }
}
