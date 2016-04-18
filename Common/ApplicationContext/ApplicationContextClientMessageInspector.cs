using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class ApplicationContextClientMessageInspector : IClientMessageInspector
    {
        public bool IsBidirectional
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isBidirectional">是否为双向传递</param>
        public ApplicationContextClientMessageInspector(bool isBidirectional = false)
        {
            IsBidirectional = isBidirectional;
        }
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (!this.IsBidirectional)
            {
                return;
            }
            if (reply.Headers.FindHeader(ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextHeaderNamespace) < 0)
            {
                return;
            }
            ApplicationContext context = reply.Headers.GetHeader<ApplicationContext>(ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextHeaderNamespace);
            if (context == null)
            {
                return;
            }
            ApplicationContext.Current = context;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            MessageHeader<ApplicationContext> contextHeader = new MessageHeader<ApplicationContext>(ApplicationContext.Current);
            request.Headers.Add(contextHeader.GetUntypedHeader(ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextHeaderNamespace));
            return null;
        }
    }
}
