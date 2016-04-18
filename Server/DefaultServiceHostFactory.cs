using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;

namespace FengSharp.OneCardAccess.Server
{
    public class DefaultServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            try
            {
                var host = new ServiceHost(serviceType, baseAddresses);
                //foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
                //{
                //    foreach (OperationDescription operation in endpoint.Contract.Operations)
                //    {
                //        FaultDescription faultDescription = new FaultDescription(ServiceExceptionDetail.FaultAction);
                //        faultDescription.DetailType = typeof(ServiceExceptionDetail);
                //        operation.Faults.Add(faultDescription);
                //    }
                //}
                host.Faulted += host_Faulted;
                return host;
            }
            catch (Exception ex)
            {
                //Logger.Writer.Write(ex, TraceEventType.Critical);
                throw ex;
            }
        }
        void host_Faulted(object sender, EventArgs e)
        {
            //var host = sender as ServiceHost;
            //if (host != null)
            //    Logger.Writer.Write(string.Format("类型{0}宿主遇到错误", host.Description.ServiceType), TraceEventType.Critical);
            //else
            //    Logger.Writer.Write("未知类型宿主遇到错误", TraceEventType.Critical);
        }
    }
}