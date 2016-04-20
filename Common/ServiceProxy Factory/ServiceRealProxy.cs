using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;

namespace FengSharp.OneCardAccess.Common
{
    public class ServiceRealProxy<T> : RealProxy
    {
        private string _endpointName;

        public ServiceRealProxy(string endpointName)
            : base(typeof(T))
        {
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentNullException("endpointName");
            }
            this._endpointName = endpointName;
        }

        public override IMessage Invoke(IMessage msg)
        {
            return MyInvoke(msg);
        }
        public IMessage MyInvoke(IMessage msg)
        {
            IMethodReturnMessage methodReturn = null;
            IMethodCallMessage methodCall = (IMethodCallMessage)msg;
            foreach (var item in RealProxyCallHandlerManager.list)
            {
                item.BeforeInvoke();
            }
            T channel = ChannelFactoryCreator.Create<T>(this._endpointName).CreateChannel();
            try
            {
                object[] copiedArgs = Array.CreateInstance(typeof(object), methodCall.Args.Length) as object[];
                methodCall.Args.CopyTo(copiedArgs, 0);
                object returnValue = methodCall.MethodBase.Invoke(channel, copiedArgs);
                methodReturn = new ReturnMessage(returnValue,
                                                copiedArgs,
                                                copiedArgs.Length,
                                                methodCall.LogicalCallContext,
                                                methodCall);
                (channel as ICommunicationObject).Close();
            }
            catch (CommunicationException ex)
            {
                (channel as ICommunicationObject).Abort();
                methodReturn = new ReturnMessage(ex, methodCall);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    methodReturn = new ReturnMessage(ex.InnerException, methodCall);
                }
                else
                {
                    methodReturn = new ReturnMessage(ex, methodCall);
                }
            }
            foreach (var item in RealProxyCallHandlerManager.list)
            {
                item.AfterInvoke();
            }
            return methodReturn;
        }
    }
    public class RealProxyCallHandlerManager
    {
        internal static List<IRealProxyCallHandler> list = new List<IRealProxyCallHandler>();
        public static void AddCallHandler(IRealProxyCallHandler handler)
        {
            list.Add(handler);
        }
        public static void RemoveCallHandler(IRealProxyCallHandler handler)
        {
            if (list.Contains(handler))
            {
                list.Remove(handler);
            }
        }
    }

    public interface IRealProxyCallHandler
    {
        void BeforeInvoke();
        void AfterInvoke();
    }
}
