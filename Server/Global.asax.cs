using FengSharp.OneCardAccess.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace FengSharp.OneCardAccess.Server
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //System.Diagnostics.Debugger.Break();
            //var serviceactivations = ((ServiceHostingEnvironmentSection)(ConfigurationManager.GetSection("system.serviceModel/serviceHostingEnvironment"))).ServiceActivations;
            //foreach (ServiceActivationElement item in serviceactivations)
            //{
            //    RouteTable.Routes.Add(
            //    new ServiceRoute(item.RelativeAddress,
            //    Activator.CreateInstance(Type.GetType(item.Factory)) as ServiceHostFactoryBase,
            //    Type.GetType(item.Service)));
            //}
            var types = new List<Type> { typeof(ConnectService), typeof(BasicInfoService), typeof(RBACService) };
            foreach (Type type in types)
            {
                RouteTable.Routes.Add(
                    new ServiceRoute(
                        type.Name,
                        new DefaultServiceHostFactory(),
                        type)
                );
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}