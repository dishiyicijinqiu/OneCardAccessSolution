using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class ApplicationContextBehaviorAttribute : Attribute, IOperationBehavior
    {
        public bool IsBidirectional
        { get; set; }
        public ApplicationContextBehaviorAttribute(bool isBidirectional = true)
        {
            this.IsBidirectional = isBidirectional;
        }

        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.Parent.MessageInspectors.Add(new ApplicationContextClientMessageInspector(this.IsBidirectional));
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.CallContextInitializers.Add(new ApplicationContextCallContextInitializer());
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        #endregion
    }
}
