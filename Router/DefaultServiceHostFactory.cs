using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Routing;
using System.Web;

namespace FengSharp.OneCardAccess.Router
{
    public class DefaultServiceHostFactory : ServiceHostFactory
    {
        public DefaultServiceHostFactory() : base()
        {

        }
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new BaseServiceHost(serviceType, baseAddresses);
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

        void host_Faulted(object sender, EventArgs e)
        {
            //var router = new RoutingService;
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
        protected override void ApplyConfiguration()
        {
            base.ApplyConfiguration();
        }
        public override ReadOnlyCollection<ServiceEndpoint> AddDefaultEndpoints()
        {
            return base.AddDefaultEndpoints();
        }
        protected override ServiceDescription CreateDescription(out IDictionary<string, ContractDescription> implementedContracts)
        {
            return base.CreateDescription(out implementedContracts);
        }
    }
}