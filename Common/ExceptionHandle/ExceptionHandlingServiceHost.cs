using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Activation;

namespace FengSharp.OneCardAccess.Common
{
    public class ExceptionHandlingServiceHost : ServiceHost
    {
        public ExceptionHandlingServiceHost(Type t, params Uri[] baseAddresses)
            : base(t, baseAddresses)
        { }

        protected override void OnOpening()
        {
            base.OnOpening();
            foreach (ServiceEndpoint endpoint in this.Description.Endpoints)
            {
                foreach (OperationDescription operation in endpoint.Contract.Operations)
                { 
                    FaultDescription faultDescription = new FaultDescription(ServiceExceptionDetail.FaultAction);
                    faultDescription.DetailType = typeof(ServiceExceptionDetail);
                    operation.Faults.Add(faultDescription);
                }
            }
        }
    }
}
