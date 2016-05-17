using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System.Windows;

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
            if (!isNotRun) return;
            ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.Office2010BlueName;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-cn");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-cn");
            ExceptionHelper.Initialize();
            DefaultEventAggregator.Current.GetEvent<ShutDownEvent>().Subscribe(ShutDown);
            //RealProxyCallHandlerManager.AddCallHandler(new Core.LoadingCallHandler());
            if (DXSplashScreen.IsActive)
                DXSplashScreen.Close();
            //DXSplashScreen.Show<SplashScreenView>();
            base.OnStartup(e);
        }
        void ShutDown(ShutDownEventArgs args)
        {
            this.Shutdown();
        }
    }
}
