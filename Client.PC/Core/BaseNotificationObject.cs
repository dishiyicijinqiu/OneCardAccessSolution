using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Core
{
    public class BaseNotificationObject : NotificationObject
    {
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
            DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
        }
        public void CloseDocument()
        {
            DefaultEventAggregator.Current.GetEvent<CloseDocumentEvent<object>>().Publish(this);
        }
        public void CloseChild()
        {
            DefaultEventAggregator.Current.GetEvent<CloseFromParentEvent<object>>().Publish(this);
        }
        public void ChangeChildViewDataContext(object NewDataContext)
        {
            DefaultEventAggregator.Current.GetEvent<ChangeDataContextFromParentEvent<object>>().Publish(this, new ChangeDataContextFromParentEventArgs(NewDataContext));
        }
        #endregion
        private object _ParentViewModel;

        public object ParentViewModel
        {
            get { return _ParentViewModel; }
            set
            {
                _ParentViewModel = value;
                RaisePropertyChanged("ParentViewModel");
            }
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

        public MsgResult ShowMessage(MessageBoxEventArgs args)
        {
            var result = MsgResult.OK;
            args.CallBack = (MsgResult msgResult, object[] param) =>
            {
                result = msgResult;
            };
            DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, args);
            return result;
        }

        public void ShowException(Exception ex)
        {
            DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
        }
    }
    public class CrudNotificationObject<Msg, K> : BaseNotificationObject where Msg : EditMessage<K>
    {
        public CrudNotificationObject()
        {
            DefaultEventAggregator.Current.
                GetEvent<EntityEditedEvent<object, EntityEditedEventArgs<Msg, K>, Msg, K>>().
                Subscribe(OnEntityViewEdited);
        }
        protected virtual void OnEntityViewEdited(object sender, EntityEditedEventArgs<Msg, K> args)
        {
        }

        public void EntityEdited()
        {
            DefaultEventAggregator.Current.GetEvent<EntityEditedEvent<object, EntityEditedEventArgs<Msg, K>, Msg, K>>()
                .Publish(this.ParentViewModel, new EntityEditedEventArgs<Msg, K>(this.EditMessage));
        }

        private Msg _EditMessage;
        public Msg EditMessage
        {
            get { return _EditMessage; }
            set
            {
                _EditMessage = value;
                RaisePropertyChanged("EditMessage");
            }
        }
    }
}
