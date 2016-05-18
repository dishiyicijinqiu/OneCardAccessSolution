using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Core
{
    public class BaseNotificationObject : NotificationObject, IViewModel
    {
        public SubscriptionToken ExceptionEventToken { get; set; }
        public SubscriptionToken MessageBoxEventToken { get; set; }
        public SubscriptionToken CreateViewEventToken { get; set; }
        public SubscriptionToken ChangeDataContextEventToken { get; set; }
        public SubscriptionToken CloseEventToken { get; set; }
        public ICommand CloseCommand { get; private set; }
        private object _Parameter;
        public object Parameter
        {
            get { return _Parameter; }
            set
            {
                _Parameter = value;
                RaisePropertyChanged("Parameter");
            }
        }
        public BaseNotificationObject()
        {
            CloseCommand = new DelegateCommand(Close);
        }

        public virtual void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken, new CloseEventArgs(CloseStyle.NullClose));
        }
        public virtual void OKClose()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken, new CloseEventArgs(CloseStyle.OKClose));
        }
        public virtual MsgResult ShowMessage(string message)
        {
            var args = new MessageBoxEventArgs(message);
            return ShowMessage(args);
        }
        public virtual MsgResult ShowError(string message)
        {
            var args = new MessageBoxEventArgs(message);
            return ShowMessage(args);
        }
        public virtual MsgResult ShowMessage(MessageBoxEventArgs args)
        {
            var result = MsgResult.OK;
            args.CallBack = (MsgResult msgResult, object[] param) =>
            {
                result = msgResult;
            };
            DefaultEventAggregator.Current.GetEvent<MessageBoxEvent>().Publish(this.MessageBoxEventToken, args);
            return result;
        }
        public virtual void ShowException(Exception ex)
        {
            DefaultEventAggregator.Current.GetEvent<ExceptionEvent>().Publish(this.ExceptionEventToken, new ExceptionEventArgs(ex));
        }
        public virtual bool? CreateView(CreateViewEventArgs args)
        {
            bool? result = null;
            if (args.IsDialog)
            {
                args.CallBack = (bool? callbackresult) =>
                {
                    result = callbackresult;
                };
            }
            DefaultEventAggregator.Current.GetEvent<CreateViewEvent>().Publish(this.CreateViewEventToken, args);
            return result;
        }
    }
}
