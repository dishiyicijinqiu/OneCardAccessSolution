using FengSharp.OneCardAccess.Client.PC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC
{
    class Program
    {
        static System.Threading.Mutex RunMutex;
        [STAThread]
        public static void Main(string[] args)
        {
            bool isNotRun = false;
            RunMutex = new System.Threading.Mutex(true, "OneCardAccess.Client.PC", out isNotRun);
            if (!isNotRun) return;
            ExceptionHelper.Initialize();


            //ThemeManager.ApplicationThemeName = "Office2010Blue";//111
            //DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.Office2010BlueName;
            //DXSplashScreen.Show<SplashScreenView>();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-cn");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-cn");
            App app = new App();
            app.Run(new MainWindow());
        }
    }
}
