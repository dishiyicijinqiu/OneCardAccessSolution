using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace FengSharp.OneCardAccess.Server
{
    public class DefaultServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new ServiceHost(serviceType, baseAddresses);
            host.Closed += host_Closed;
            host.Faulted += host_Faulted;
            host.Opened += host_Opened;
            return host;
        }

        void host_Opened(object sender, EventArgs e)
        {
            //var host = sender as ServiceHost;
            //if (host != null)
            //    Logger.Write(string.Format("{0}宿主启动", host.Description.ServiceType), "ServiceHostLog", -1, -1, System.Diagnostics.TraceEventType.Information, "宿主启动");
        }

        void host_Faulted(object sender, EventArgs e)
        {
            var host = sender as ServiceHost;
            //if (host != null)
            //    Logger.Write(string.Format("{0}宿主遇到错误", host.Description.ServiceType), "ServiceHostLog", 2, -1, System.Diagnostics.TraceEventType.Error, "宿主遇到错误");
        }

        void host_Closed(object sender, EventArgs e)
        {
            var host = sender as ServiceHost;
            //if (host != null)
            //    Logger.Write(string.Format("{0}宿主关闭", host.Description.ServiceType), "ServiceHostLog", -1, -1, System.Diagnostics.TraceEventType.Information, "宿主关闭");
        }
    }
}