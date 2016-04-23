using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.Events;
using System;
using System.Globalization;

namespace FengSharp.OneCardAccess.Core
{
    public class ShutDownEvent : CompositePresentationEvent<ShutDownEventArgs> { }
    public class ShutDownEventArgs { }
    public class LoginTimeOutEvent : CompositePresentationEvent<LoginTimeOutEventArgs> { }
    public class LoginTimeOutEventArgs
    {
        public LoginTimeOutEventArgs(LoginTimeOutException loginTimeOutException)
        {
            this.LoginTimeOutException = loginTimeOutException;
        }
        public LoginTimeOutException LoginTimeOutException { get; set; }
    }
    public class ExceptionEvent<Sender> : BaseSenderEvent<Sender, ExceptionEventArgs> { }
    public class ExceptionEventArgs
    {
        public ExceptionEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
        public Exception Exception { get; set; }
    }
    public class MessageBoxEvent : CompositePresentationEvent<MessageBoxEventArgs> { }
    public class MessageBoxEvent<Sender> : BaseSenderEvent<Sender, MessageBoxEventArgs> { }
    public class MessageBoxEventArgs
    {
        public MessageBoxEventArgs(string messageText, string caption)
        {
            this.MessageText = messageText;
            this.Caption = caption;
        }

        public string MessageText { get; set; }
        public string Caption { get; set; }
    }

    public class NullEvent : CompositePresentationEvent<NullEventArgs> { }
    public class NullEventArgs { }
}
