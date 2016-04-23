using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Configuration;

namespace FengSharp.OneCardAccess.Common
{

    public static class ServiceLoader
    {
        private const string unityconfigname = "unity";
        static IUnityContainer Container
        { get; set; }
        #region ctor
        static ServiceLoader()
        {
            try
            {
                Container = new UnityContainer();
                ConfigurationManager.GetSection(unityconfigname);
                UnityConfigurationSection unitySection = ConfigurationManager.GetSection(unityconfigname) as UnityConfigurationSection;
                if (unitySection != null)
                {
                    foreach (var container in unitySection.Containers)
                    {
                        Container.LoadConfiguration(container.Name);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region LoadService
        public static object LoadService(System.Type type, string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Container.Resolve(type);
            return Container.Resolve(type, name);
        }
        public static void RegisterInstance<TInterface>(TInterface instance, LifetimeManager lifetime = null)
        {
            if (lifetime == null)
                Container.RegisterInstance(instance);
            else
                Container.RegisterInstance(instance, lifetime);
        }

        public static T LoadService<T>()
        {
            return Container.Resolve<T>();
        }
        public static T LoadService<T>(string serviceName, params ResolverOverride[] overrides)
        {
            return Container.Resolve<T>(serviceName, overrides);
        }
        #endregion
    }
}
