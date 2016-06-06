using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Routing;
using System.Web;

namespace FengSharp.OneCardAccess.Router
{
    public class DefaultServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);
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

        void host_Faulted(object sender, EventArgs e)
        {
          
        }
    }
}