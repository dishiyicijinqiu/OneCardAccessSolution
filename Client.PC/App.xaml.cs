using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.Helpers;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System.Windows;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace FengSharp.OneCardAccess.Client.PC
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        static System.Threading.Mutex RunMutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            bool isNotRun = false;
            RunMutex = new System.Threading.Mutex(true, "OneCardAccess.Client.PC", out isNotRun);
            if (!isNotRun)
            {
                Shutdown();
            };
            ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.Office2010BlueName;
            Helpers.ExceptionHelper.Initialize();
            //Test1();
            //RegisterServices();
            //RealProxyCallHandlerManager.AddCallHandler(new Core.LoadingCallHandler());
            if (DXSplashScreen.IsActive)
                DXSplashScreen.Close();
            DXSplashScreen.Show<SplashScreenView>();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-cn");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-cn");
            base.OnStartup(e);
        }
        #region test1
      //  public static void RegisterServices()
      //  {
      //      var type = typeof(IServiceRealProxy<IConnectService>);
      //      var instance = new ServiceRealProxy<IConnectService>("ConnectService");
      //      ServiceLoader.Container.RegisterInstance(type, instance)

      //      //Container.RegisterInstance<IConnectService>(new MyConnectionService())
      //         .AddNewExtension<Interception>().Configure<Interception>()
      //         .SetDefaultInterceptorFor<IServiceRealProxy<IConnectService>>(new TransparentProxyInterceptor())
      //         .AddPolicy("loadingpolicy")
      //         .AddMatchingRule(new TypeMatchingRule(type))
      //      //   .AddMatchingRule(new MethodSignatureMatchingRule("Invoke", new System.Collections.Generic.List<string>()
      //      //{
      //      //    "System.Runtime.Remoting.Messaging.IMessage,mscorlib"
      //      //}))
      //         .AddCallHandler(typeof(LoadingCallHandler));
      //      var irealproxy = ServiceLoader.Container.Resolve<IServiceRealProxy<IConnectService>>();
      //      if (irealproxy == null)
      //      {
      //      }
      //      else
      //      {
      //      }
      //      var test = irealproxy as System.Runtime.Remoting.Proxies.RealProxy;
      //      if (test == null)
      //      {

      //      }

      //      var ycproxy = irealproxy as IConnectService;
      //      if (ycproxy == null)
      //      {

      //      }
      //      ycproxy.Login("", "");
      //  }

      //  static void Test1()
      //  {
      //      var Container = new UnityContainer();
      //      Container.RegisterType<IMyConnectService, MyConnectService>()
      //.AddNewExtension<Interception>().Configure<Interception>()
      //.SetDefaultInterceptorFor<IMyConnectService>(new TransparentProxyInterceptor())
      //.AddPolicy("loadingpolicy")
      //.AddMatchingRule(new TypeMatchingRule(typeof(IMyConnectService)))
      //.AddCallHandler(typeof(LoadingCallHandler));
      //      var connectService = Container.Resolve<IMyConnectService>();
      //      connectService.Login(string.Empty, string.Empty);
      //  }
        #endregion
    }

    #region test1
    //public interface IMyConnectService
    //{
    //    bool Login(string username, string password);
    //}
    //public class MyConnectService : IMyConnectService
    //{
    //    public bool Login(string username, string password)
    //    {
    //        System.Threading.Thread.Sleep(3000);
    //        return true;
    //    }
    //}

    #endregion
}
