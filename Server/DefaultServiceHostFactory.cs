﻿using FengSharp.OneCardAccess.Common;
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
            var host = sender as ServiceHost;
            if (host != null)
                Logger.Writer.Write(string.Format("类型{0}宿主遇到错误", host.Description.ServiceType), TraceEventType.Critical);
            else
                Logger.Writer.Write("未知类型宿主遇到错误", TraceEventType.Critical);
        }
    }
}