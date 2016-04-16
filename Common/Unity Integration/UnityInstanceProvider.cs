using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public static class Extension
    {
        public static IList<UnityTypeMapping> Copy(this RegisterElementCollection unityTypeElements)
        {
            IList<UnityTypeMapping> mappings = new List<UnityTypeMapping>();
            foreach (RegisterElement type in unityTypeElements)
            {
                mappings.Add(new UnityTypeMapping { Type = System.Type.GetType(type.TypeName), MapTo = System.Type.GetType(type.MapToName), Name = type.Name });
            }

            return mappings;
        }
    }

    public class UnityTypeMapping
    {
        public Type Type
        { get; set; }

        public Type MapTo
        { get; set; }

        public string Name
        { get; set; }
    }


    public class UnityInstanceProvider : IInstanceProvider
    {
        private static object syncHelper = new object();
        private Type _contractType;

        public static IDictionary<string, IUnityContainer> Containers
        { get; private set; }

        private IUnityContainer Container
        { get; set; }

        static UnityInstanceProvider()
        {
            Containers = new Dictionary<string, IUnityContainer>();
        }

        public UnityInstanceProvider(Type contractType, string containerName)
        {
            if (contractType == null)
            {
                throw new ArgumentNullException("contractType");

            }
            this._contractType = contractType;

            string key = containerName ?? "";
            if (Containers.ContainsKey(key))
            {
                this.Container = Containers[key];
                return;
            }
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration(containerName);
            lock (syncHelper)
            {
                if (!Containers.ContainsKey(key))
                {
                    Containers[key] = container;
                }
            }

            Container = container;
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.Container.Resolve(this._contractType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return this.GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            IDisposable disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        #endregion
    }
}