using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class SessionBehaviorAttribute : Attribute, IOperationBehavior
    {
        public bool SessionCheck
        { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionCheck">是否检测会话的有效性</param>
        public SessionBehaviorAttribute(bool sessionCheck = true)
        {
            SessionCheck = sessionCheck;
        }
        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.Parent.MessageInspectors.Add(new SessionClientMessageInspector());
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.CallContextInitializers.Add(new SessionCallContextInitializer());
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        #endregion
    }
}
