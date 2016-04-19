using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
namespace FengSharp.OneCardAccess.Common
{
    public class SessionBehavior : IEndpointBehavior
    {
        public bool SessionCheck
        { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionCheck">是否检测会话</param>
        public SessionBehavior(bool sessionCheck = true)
        {
            SessionCheck = sessionCheck; 
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
                operation.CallContextInitializers.Add(new SessionCallContextInitializer(this.SessionCheck));
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
