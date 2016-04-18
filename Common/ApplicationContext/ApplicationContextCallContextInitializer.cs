using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class ApplicationContextCallContextInitializer : ICallContextInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationContextCallContextInitializer()
        {
        }

        public void AfterInvoke(object correlationState)
        {
            ApplicationContext context = correlationState as ApplicationContext;
            if (context == null)
            {
                return;
            }
            MessageHeader<ApplicationContext> contextHeader = new MessageHeader<ApplicationContext>(context);
            var header = contextHeader.GetUntypedHeader(ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextHeaderNamespace);
            OperationContext.Current.OutgoingMessageHeaders.Add(header);
            ApplicationContext.Current = null;
        }

        public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
        {
            ApplicationContext context = message.Headers.GetHeader<ApplicationContext>(ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextHeaderNamespace);
            if (context == null) return null;
            ApplicationContext.Current = context;
            return ApplicationContext.Current;
        }
    }
}
