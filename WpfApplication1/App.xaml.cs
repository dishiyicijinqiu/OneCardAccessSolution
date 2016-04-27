using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.Office2010BlueName;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-cn");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-cn");
            base.OnStartup(e);
        }
    }
}
