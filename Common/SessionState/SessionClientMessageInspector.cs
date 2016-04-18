using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class SessionClientMessageInspector : IClientMessageInspector
    {
        public SessionClientMessageInspector()
        {
        }
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (reply.Headers.FindHeader(Session.ContextHeaderLocalName, Session.ContextHeaderNamespace) < 0)
            {
                Session.Current = null;
                return;
            }
            Session context = reply.Headers.GetHeader<Session>(Session.ContextHeaderLocalName, Session.ContextHeaderNamespace);
            if (context == null)
            {
                Session.Current = null;
                return;
            }
            Session.Current = context;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            MessageHeader<Session> contextHeader = new MessageHeader<Session>(Session.Current);
            request.Headers.Add(contextHeader.GetUntypedHeader(Session.ContextHeaderLocalName, Session.ContextHeaderNamespace));
            return null;
        }
    }
}
