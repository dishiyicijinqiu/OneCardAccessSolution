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
            var host = base.CreateServiceHost(serviceType, baseAddresses);
            //if (!host.Description.Behaviors.Contains(typeof(AspNetCompatibilityRequirementsAttribute)))
            //    host.Description.Behaviors.Add(new AspNetCompatibilityRequirementsAttribute() { RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed });
            host.Faulted += host_Faulted;
            host.Opened += Host_Opened;
            host.UnknownMessageReceived += Host_UnknownMessageReceived;
            return host;
        }

        private void Host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
        }

        private void Host_Opened(object sender, EventArgs e)
        {
        }
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            return new BaseServiceHost(Type.GetType(constructorString), baseAddresses);
        }

        void host_Faulted(object sender, EventArgs e)
        {
            var host = sender as ServiceHost;
            if (host != null)
                Logger.Writer.Write(string.Format("类型{0}宿主遇到错误", host.Description.ServiceType), TraceEventType.Critical);
            else
                Logger.Writer.Write("未知类型宿主遇到错误", TraceEventType.Critical);
        }
    }
    public class BaseServiceHost : ServiceHost
    {
        public BaseServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
        {

        }
        public override void AddServiceEndpoint(ServiceEndpoint endpoint)
        {
            base.AddServiceEndpoint(endpoint);
        }
    }
}