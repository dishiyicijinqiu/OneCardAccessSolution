﻿using System;
using System.Configuration;
using System.ServiceModel.Activation;
using System.ServiceModel.Configuration;
using System.Web.Routing;

namespace FengSharp.OneCardAccess.Router
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.Add(
            //    new ServiceRoute(
            //        "RoutingService",
            //        new ServiceHostFactory(),
            //        typeof(System.ServiceModel.Routing.RoutingService)
            //    )
            //);
            //RouteTable.Routes.Add(
            //    new ServiceRoute(
            //        "RoutingService",
            //        new ServiceHostFactory(),
            //        typeof(System.ServiceModel.Routing.RoutingService)
            //    )
            //);
            //RouteTable.Routes.MapPageRoute("Connect", "RoutingService/{Service}", "~/Connect");
            // RouteTable.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            //RouteTable.Routes.MapPageRoute("", "", "~/RoutingService.svc");

            RouteTable.Routes.MapPageRoute("WebFormRoute",
                      "test",
                      "~/WebForm1.aspx");
            RouteTable.Routes.MapPageRoute("bWebFormRoute",
             "atest",
             "~/RoutingService.svc");


            // RouteTable.Routes.MapPageRoute("WebFormRoute",
            //"test",
            //"~/RoutingService.svc");



            //RouteTable.Routes.MapPageRoute("default", "test", "~/RoutingService.svc", false);



            //var defaults = new RouteValueDictionary { { "areacode", "010" }, { "days", 2 } };
            //var constaints = new RouteValueDictionary { { "areacode", @"0\d{2,3}" }, { "days", @"[1-3]{1}" } };
            //var dataTokens = new RouteValueDictionary { { "defaultCity", "BeiJing" }, { "defaultDays", 2 } };
            //RouteTable.Routes.MapPageRoute("default", "{areacode}/{days}", "~/weather.aspx", false, defaults, constaints, dataTokens);






            //var serviceactivations = ((ServiceHostingEnvironmentSection)(ConfigurationManager.GetSection("system.serviceModel/serviceHostingEnvironment"))).ServiceActivations;
            //foreach (ServiceActivationElement item in serviceactivations)
            //{
            //    //RouteTable.Routes.Add(
            //    //new ServiceRoute(item.RelativeAddress,
            //    //Activator.CreateInstance(Type.GetType(item.Factory)) as ServiceHostFactoryBase,
            //    //Type.GetType(item.Service))
            //    //);
            //    //RouteTable.Routes.MapPageRoute("", "", "");
            //}
            // RouteTable.Routes.Add(
            //new ServiceRoute("RoutingService",
            //Activator.CreateInstance(Type.GetType("FengSharp.OneCardAccess.Router.DefaultServiceHostFactory")) as ServiceHostFactoryBase,
            //Type.GetType("System.ServiceModel.Routing.RoutingService,System.ServiceModel.Routing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")));

            //var services = ((ServicesSection)(ConfigurationManager.GetSection("system.serviceModel/services"))).Services;
            //foreach (ServiceElement item in services)
            //{
            //    //var type = Type.GetType("System.ServiceModel.Routing.RoutingService,System.ServiceModel.Routing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            //    //RouteTable.Routes.Add(
            //    //new ServiceRoute(type.Name,
            //    //new DefaultServiceHostFactory(),
            //    //type)
            //    //);
            //}
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