using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class UnityBehaviorAttribute : Attribute, IServiceBehavior
    {
        public string ContainerName
        { get; private set; }

        public UnityBehaviorAttribute(string containerName)
        {
            this.ContainerName = containerName;
        }

        #region IServiceBehavior Members
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    string contractName = endpointDispatcher.ContractName;
                    if (contractName != "IMetadataExchange" &&
                        contractName != "IHttpGetHelpPageAndMetadataContract")
                    {
                        endpointDispatcher.DispatchRuntime.InstanceProvider = new UnityInstanceProvider(GetContractType(serviceHostBase, endpointDispatcher), this.ContainerName);
                    }
                }
            }
        }
        private Type GetContractType(ServiceHostBase serviceHostBase, EndpointDispatcher endpointDispatcher)
        {
            var endpoint = serviceHostBase.Description.Endpoints.Where(item => item.Contract.Name == endpointDispatcher.ContractName &&
                item.Contract.Namespace == endpointDispatcher.ContractNamespace).ToArray<ServiceEndpoint>()[0];
            return endpoint.Contract.ContractType;
        }
        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }
        #endregion
    }
}
