using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public interface IClientFaultFormatter
    {
        FaultException Deserialize(MessageFault messageFault, string action);
    }

    internal class FaultFormatter : IClientFaultFormatter
    {
        private object _formatter;
        private MethodInfo _method;
        private const string formatterTypeName = "System.ServiceModel.Dispatcher.FaultFormatter,System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        public FaultFormatter(ClientRuntime clientRuntime)
        {
            Type formatterType = Type.GetType(formatterTypeName);
            this._method = formatterType.GetMethod("Deserialize", BindingFlags.Public | BindingFlags.Instance);
            SynchronizedCollection<FaultContractInfo> faultContractInfoList = new SynchronizedCollection<FaultContractInfo>();
            foreach(ClientOperation operation in clientRuntime.Operations)
            {
                foreach (FaultContractInfo faultInfo in operation.FaultContractInfos)
                {
                    if (faultContractInfoList.Contains(faultInfo))
                    {
                        continue;
                    }

                    faultContractInfoList.Add(faultInfo);
                }
            }

            ConstructorInfo constructor = formatterType.GetConstructor( BindingFlags.NonPublic| BindingFlags.Instance,null,new Type[]{typeof(SynchronizedCollection<FaultContractInfo>)},null);
            this._formatter = constructor.Invoke(new object[]{faultContractInfoList});           
        }

        #region IClientFaultFormatter Members

        public FaultException Deserialize(MessageFault messageFault, string action)
        {
            return (FaultException)this._method.Invoke(this._formatter, new object[] { messageFault, action });
        }

        #endregion
    }
}
