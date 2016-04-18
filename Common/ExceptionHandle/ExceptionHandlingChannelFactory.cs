using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace FengSharp.OneCardAccess.Common
{
    public class ExceptionHandlingChannelFactory<TChannel>:ChannelFactory<TChannel>
    {
        public ExceptionHandlingChannelFactory(string endpointConfigurationName)
            : base(endpointConfigurationName)
        { }

        protected override ServiceEndpoint CreateDescription()
        {  
            ServiceEndpoint serviceEndpoint = base.CreateDescription();
            foreach (OperationDescription operation in serviceEndpoint.Contract.Operations)
            {
                FaultDescription faultDescription = new FaultDescription(ServiceExceptionDetail.FaultAction);
                faultDescription.DetailType = typeof(ServiceExceptionDetail);
                operation.Faults.Add(faultDescription);
            }

            return serviceEndpoint;
        }
    }
}
