using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class SessionCallContextInitializer : ICallContextInitializer
    {
        public void AfterInvoke(object correlationState)
        {
            Session context = correlationState as Session;
            MessageHeader<Session> contextHeader = new MessageHeader<Session>(context);
            var header = contextHeader.GetUntypedHeader(Session.ContextHeaderLocalName, Session.ContextHeaderNamespace);
            OperationContext.Current.OutgoingMessageHeaders.Add(header);
        }

        public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
        {
            Session context = message.Headers.GetHeader<Session>(Session.ContextHeaderLocalName, Session.ContextHeaderNamespace);
            if (context == null)
                throw new BusinessException("无效的会话");
            if (string.IsNullOrWhiteSpace(context.Ticket))
                throw new BusinessException("无效的会话");
            var sessionInfo = SessionState.GetSession(context.Ticket);
            if (sessionInfo == null)
                throw new LoginTimeOutException();
            return sessionInfo;
        }
    }
}
