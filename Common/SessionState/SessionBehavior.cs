using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
namespace FengSharp.OneCardAccess.Common
{
    public class SessionBehavior : IEndpointBehavior
    {
        public bool IsBidirectional
        { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isBidirectional">是否为双向传递</param>
        public SessionBehavior(bool isBidirectional = true)
        {
            IsBidirectional = isBidirectional; 
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new SessionClientMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            foreach (var operation in endpointDispatcher.DispatchRuntime.Operations)
            {
                operation.CallContextInitializers.Add(new SessionCallContextInitializer());
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
