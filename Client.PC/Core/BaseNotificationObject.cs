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
        public SubscriptionToken CloseEventToken { get; set; }
        public SubscriptionToken MessageBoxEventToken { get; set; }
        public SubscriptionToken CreateViewEventToken { get; set; }
        public SubscriptionToken ChangeDataContextEventToken { get; set; }

        public ICommand CloseCommand { get; private set; }
        public ICommand CloseDocumentCommand { get; private set; }
        public BaseNotificationObject()
        {
            CloseCommand = new DelegateCommand(Close);
            CloseDocumentCommand = new DelegateCommand(CloseDocument);
        }
        #region commandmethods
        public void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken, new CloseEventArgs(CloseStyle.NullClose));
        }
        public void CloseDocument()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken, new CloseEventArgs(CloseStyle.DocumentClose));
        }
        #endregion
        public void OKClose()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken, new CloseEventArgs(CloseStyle.OKClose));
        }

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

        public MsgResult ShowMessage(string message)
        {
            var args = new MessageBoxEventArgs(message);
            return ShowMessage(args);
        }
        public MsgResult ShowError(string message)
        {
            var args = new MessageBoxEventArgs(message);
            return ShowMessage(args);
        }

        public MsgResult ShowMessage(MessageBoxEventArgs args)
        {
            var result = MsgResult.OK;
            args.CallBack = (MsgResult msgResult, object[] param) =>
            {
                result = msgResult;
            };
            DefaultEventAggregator.Current.GetEvent<MessageBoxEvent>().Publish(this.MessageBoxEventToken, args);
            return result;
        }

        public void ShowException(Exception ex)
        {
            DefaultEventAggregator.Current.GetEvent<ExceptionEvent>().Publish(this.ExceptionEventToken, new ExceptionEventArgs(ex));
        }
        public bool? CreateView(CreateViewEventArgs args)
        {
            bool? result = null;
            if (args.ViewStyle == ViewStyle.Dialog)
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
    //public class CrudNotificationObject<Msg, K> : BaseNotificationObject where Msg : EditMessage<K>
    //{
    //    public CrudNotificationObject()
    //    {
    //        DefaultEventAggregator.Current.
    //            GetEvent<EntityEditedEvent<object, EntityEditedEventArgs<Msg, K>, Msg, K>>().
    //            Subscribe(OnEntityViewEdited);
    //    }
    //    protected virtual void OnEntityViewEdited(object sender, EntityEditedEventArgs<Msg, K> args)
    //    {
    //    }

    //    public void EntityEdited()
    //    {
    //        DefaultEventAggregator.Current.GetEvent<EntityEditedEvent<object, EntityEditedEventArgs<Msg, K>, Msg, K>>()
    //            .Publish(this.ParentViewModel, new EntityEditedEventArgs<Msg, K>(this.EditMessage));
    //    }

    //    private Msg _EditMessage;
    //    public Msg EditMessage
    //    {
    //        get { return _EditMessage; }
    //        set
    //        {
    //            _EditMessage = value;
    //            RaisePropertyChanged("EditMessage");
    //        }
    //    }
    //}
}
