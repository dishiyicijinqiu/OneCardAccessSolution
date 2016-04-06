using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.Helpers;
using FengSharp.OneCardAccess.Client.PC.UI;
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
            if (!isNotRun)
            {
                Shutdown();
            };
            //ThemeManager.ApplicationThemeName = "Office2010Blue";//111
            ExceptionHelper.Initialize();
            ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.Office2010BlueName;
            DXSplashScreen.Show<SplashScreenView>();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-cn");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-cn");
            base.OnStartup(e);
        }
    }
}
