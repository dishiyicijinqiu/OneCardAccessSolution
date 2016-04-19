using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class SessionCallContextInitializer : ICallContextInitializer
    {
        public bool SessionCheck
        { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionCheck">是否检测会话的有效性</param>
        public SessionCallContextInitializer(bool sessionCheck = true)
        {
            SessionCheck = sessionCheck;
        }
        public void AfterInvoke(object correlationState)
        {
            if (correlationState == null) correlationState = Session.Current;
            Session context = correlationState as Session;
            MessageHeader<Session> contextHeader = new MessageHeader<Session>(context);
            var header = contextHeader.GetUntypedHeader(Session.ContextHeaderLocalName, Session.ContextHeaderNamespace);
            OperationContext.Current.OutgoingMessageHeaders.Add(header);
        }

        public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
        {
            Session context = message.Headers.GetHeader<Session>(Session.ContextHeaderLocalName, Session.ContextHeaderNamespace);
            if (SessionCheck)
            {
                if (context == null)
                    throw new BusinessException(Properties.Resources.Error_UnableSession);
                if (string.IsNullOrWhiteSpace(context.Ticket))
                    throw new BusinessException(Properties.Resources.Error_UnableSession);
                Session.Current = SessionState.GetSession(context.Ticket);
                if (Session.Current == null)
                    throw new LoginTimeOutException();
            }
            return Session.Current;
        }
    }
}
