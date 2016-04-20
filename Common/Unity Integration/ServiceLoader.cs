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
        public static IUnityContainer Container
        { get; set; }
        #region ctor
        static ServiceLoader()
        {
            try
            {
                Container = new UnityContainer();
                //ConfigurationManager.GetSection(unityconfigname);
                //UnityConfigurationSection unitySection = ConfigurationManager.GetSection(unityconfigname) as UnityConfigurationSection;
                //if (unitySection != null)
                //{
                //    foreach (var container in unitySection.Containers)
                //    {
                //        Container.LoadConfiguration(container.Name);
                //    }
                //}
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
        public static T LoadService<T>()
        {
            return Container.Resolve<T>();
        }
        public static T LoadService<T>(string serviceName, params ResolverOverride[] overrides)
        {
            return Container.Resolve<T>(serviceName, overrides);
        }
        #endregion

        //public static void RegisterServices()
        //{
        //    var service = ServiceProxyFactory.Create<IConnectService>();

        //    Container.RegisterInstance<IConnectService>(service)

        //    //Container.RegisterInstance<IConnectService>(new MyConnectionService())
        //       .AddNewExtension<Interception>().Configure<Interception>()
        //       .SetDefaultInterceptorFor<IConnectService>(new TransparentProxyInterceptor())
        //       .AddPolicy("loadingpolicy")
        //       .AddMatchingRule(new TypeMatchingRule(typeof(System.Runtime.Remoting.Proxies.RealProxy)))
        //       .AddMatchingRule(new MethodSignatureMatchingRule("Invoke", new System.Collections.Generic.List<string>()
        //    {
        //        "System.Runtime.Remoting.Messaging.IMessage,mscorlib"
        //    }))
        //       .AddCallHandler(typeof(LoadingCallHandler));
        //    var connectService = Container.Resolve<IConnectService>();
        //    connectService.Login(string.Empty, string.Empty);
        //}
    }
}
