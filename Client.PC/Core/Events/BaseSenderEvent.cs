using Microsoft.Practices.Prism.Events;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace FengSharp.OneCardAccess.Core
{
    public class SenderEvent<Args> : BaseSenderEvent<SubscriptionToken, Args>
    {
    }
    public class BaseSenderEvent<Sender, Args> : EventBase
    {
        private ISenderDispatcherFacade uiDispatcher;
        private ISenderDispatcherFacade UIDispatcher
        {
            get
            {
                if (uiDispatcher == null)
                {
                    this.uiDispatcher = new DefaultSenderDispatcher();
                }

                return uiDispatcher;
            }
        }
        public SubscriptionToken Subscribe(Action<Sender, Args> action)
        {
            return Subscribe(action, ThreadOption.PublisherThread);
        }
        public SubscriptionToken Subscribe(Action<Sender, Args> action, ThreadOption threadOption)
        {
            return Subscribe(action, threadOption, false);
        }
        public SubscriptionToken Subscribe(Action<Sender, Args> action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);
        }
        public SubscriptionToken Subscribe(Action<Sender, Args> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, threadOption, keepSubscriberReferenceAlive, null);
        }
        public virtual SubscriptionToken Subscribe(Action<Sender, Args> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Func<Sender, Args, bool> filter)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
            IDelegateReference filterReference;
            if (filter != null)
            {
                filterReference = new DelegateReference(filter, keepSubscriberReferenceAlive);
            }
            else
            {
                filterReference = new DelegateReference(new Func<Sender, Args, bool>(delegate { return true; }), true);
            }
            SenderEventSubscription<Sender, Args> subscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    subscription = new SenderEventSubscription<Sender, Args>(actionReference, filterReference);
                    break;
                case ThreadOption.BackgroundThread:
                    subscription = new SenderBackgroundEventSubscription<Sender, Args>(actionReference, filterReference);
                    break;
                case ThreadOption.UIThread:
                    subscription = new SenderDispatcherEventSubscription<Sender, Args>(actionReference, filterReference, UIDispatcher);
                    //subscription = new DispatcherEventSubscription<Args>(actionReference, filterReference, UIDispatcher);
                    break;
                default:
                    subscription = new SenderEventSubscription<Sender, Args>(actionReference, filterReference);
                    break;
            }


            return base.InternalSubscribe(subscription);
        }
        public virtual void Publish(Sender sender, Args args)
        {
            base.InternalPublish(sender, args);
        }
        public virtual void Publish(Sender sender)
        {
            base.InternalPublish(sender, null);
        }
        public virtual void Unsubscribe(Action<Sender, Args> subscriber)
        {
            lock (Subscriptions)
            {
                IEventSubscription eventSubscription = Subscriptions.Cast<SenderEventSubscription<Sender, Args>>().FirstOrDefault(evt => evt.Action == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }
        public virtual bool Contains(Action<Sender, Args> subscriber)
        {
            IEventSubscription eventSubscription;
            lock (Subscriptions)
            {
                eventSubscription = Subscriptions.Cast<SenderEventSubscription<Sender, Args>>().FirstOrDefault(evt => evt.Action == subscriber);
            }
            return eventSubscription != null;
        }
    }
    public class SenderEventSubscription<Sender, Args> : IEventSubscription
    {
        private readonly IDelegateReference _actionReference;
        private readonly IDelegateReference _filterReference;
        public SenderEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            if (actionReference == null)
                throw new ArgumentNullException("actionReference");
            if (!(actionReference.Target is Action<Sender, Args>))
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "InvalidDelegateRerefenceTypeException", typeof(Action<Sender, Args>).FullName), "actionReference");

            if (filterReference == null)
                throw new ArgumentNullException("filterReference");
            if (!(filterReference.Target is Func<Sender, Args, bool>))
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "InvalidDelegateRerefenceTypeException", typeof(Func<Sender, Args, bool>).FullName), "filterReference");

            _actionReference = actionReference;
            _filterReference = filterReference;
        }
        public Action<Sender, Args> Action
        {
            get { return (Action<Sender, Args>)_actionReference.Target; }
        }
        public Func<Sender, Args, bool> Filter
        {
            get { return (Func<Sender, Args, bool>)_filterReference.Target; }
        }
        public SubscriptionToken SubscriptionToken { get; set; }
        public virtual Action<object[]> GetExecutionStrategy()
        {
            Action<Sender, Args> action = this.Action;
            Func<Sender, Args, bool> filter = this.Filter;
            if (action != null && filter != null)
            {
                return arguments =>
                {
                    Sender sender = default(Sender);
                    if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                    {
                        sender = (Sender)arguments[0];
                    }
                    Args argument = default(Args);
                    if (arguments != null && arguments.Length > 1 && arguments[1] != null)
                    {
                        argument = (Args)arguments[1];
                    }
                    if (filter(sender, argument))
                    {
                        InvokeAction(action, sender, argument);
                    }
                };
            }
            return null;
        }
        public virtual void InvokeAction(Action<Sender, Args> action, Sender sender, Args argument)
        {
            if (action == null) throw new System.ArgumentNullException("action");

            action(sender, argument);
        }
    }
    public class SenderBackgroundEventSubscription<Sender, Args> : SenderEventSubscription<Sender, Args>
    {
        public SenderBackgroundEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference) :
            base(actionReference, filterReference)
        {
        }
        public override void InvokeAction(Action<Sender, Args> action, Sender sender, Args argument)
        {
            Thread thread = new Thread(() =>
            {
                action(sender, argument);
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
    public class SenderDispatcherEventSubscription<Sender, Args> : SenderEventSubscription<Sender, Args>
    {
        private readonly ISenderDispatcherFacade dispatcher;
        public SenderDispatcherEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference, ISenderDispatcherFacade dispatcher)
            : base(actionReference, filterReference)
        {
            this.dispatcher = dispatcher;
        }
        public override void InvokeAction(Action<Sender, Args> action, Sender sender, Args argument)
        {
            dispatcher.BeginInvoke(action, sender, argument);
        }
    }
    public interface ISenderDispatcherFacade
    {
        void BeginInvoke(Delegate method, object sender, object arg);
    }
    public class DefaultSenderDispatcher : ISenderDispatcherFacade
    {
        public void BeginInvoke(Delegate method, object sender, object arg)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, method, sender, arg);
            }
        }
    }
}
