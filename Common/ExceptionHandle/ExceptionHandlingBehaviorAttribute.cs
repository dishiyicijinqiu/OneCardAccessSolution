﻿using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
namespace FengSharp.OneCardAccess.Common
{
    public class ExceptionHandlingBehaviorAttribute:Attribute,IOperationBehavior,IContractBehavior,IEndpointBehavior,IServiceBehavior
    {
        public string ExceptionPolicyName
        { get; private set; }

        public ExceptionHandlingBehaviorAttribute(string exceptionPolicyName)
        {
            if (string.IsNullOrEmpty(exceptionPolicyName))
            {
                throw new ArgumentNullException("exceptionPolicyName");
            }
            this.ExceptionPolicyName = exceptionPolicyName;
        }

        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {}

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.Parent.MessageInspectors.Add(new ExceptionHandlingMessageInspector());
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Parent.ChannelDispatcher.ErrorHandlers.Add(new ServiceErrorHandler(this.ExceptionPolicyName));
            dispatchOperation.Parent.ChannelDispatcher.ErrorHandlers.Add(new ServiceErrorHandler(this.ExceptionPolicyName));
        }

        public void Validate(OperationDescription operationDescription)
        {}

        #endregion

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {}

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ExceptionHandlingMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {}

        public void Validate(ServiceEndpoint endpoint)
        {}

        #endregion

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {}

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {              
                channelDispatcher.ErrorHandlers.Add(new ServiceErrorHandler(this.ExceptionPolicyName));
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {}

        #endregion

        #region IContractBehavior Members

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {}

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ExceptionHandlingMessageInspector());
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.ChannelDispatcher.ErrorHandlers.Add(new ServiceErrorHandler(this.ExceptionPolicyName));
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {}

        #endregion
    }
}