using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowScriptProxy proxy;
        public MainWindow()
        {
            InitializeComponent();
            proxy = new MainWindowScriptProxy(this);
            browser.ObjectForScripting = proxy;
            SuppressScriptErrors(browser, true);
            this.browser.Navigate(new Uri("http://dishiyicijinqiu.eicp.net:17515/WebSite/Devices/WPF/Index.html"));
        }
        static void SuppressScriptErrors(WebBrowser webBrowser, bool hide)
        {
            webBrowser.Navigating += (s, e) =>
            {
                var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (fiComWebBrowser == null)
                    return;

                object objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);
                if (objComWebBrowser == null)
                    return;

                objComWebBrowser.GetType().InvokeMember("Silent", System.Reflection.BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
            };
        }
    }

    [System.Runtime.InteropServices.ComVisible(true)]
    public class MainWindowScriptProxy
    {
        bool ismax = false;
        Rect rcnormal;
        MainWindow mainWindow;
        public MainWindowScriptProxy(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            mainWindow.SizeChanged += MainWindow_SizeChanged;
        }
        public void MinWindow()
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (mainWindow.ActualHeight > SystemParameters.WorkArea.Height || mainWindow.ActualWidth > SystemParameters.WorkArea.Width)
            {
                mainWindow.WindowState = System.Windows.WindowState.Normal;
                MaxWindow();
            }
        }

        public void MaxWindow()
        {
            if (ismax)
            {
                mainWindow.Left = rcnormal.Left;
                mainWindow.Top = rcnormal.Top;
                mainWindow.Width = rcnormal.Width;
                mainWindow.Height = rcnormal.Height;
            }
            else
            {
                rcnormal = new Rect(mainWindow.Left, mainWindow.Top, mainWindow.Width, mainWindow.Height);//保存下当前位置与大小
                mainWindow.Left = 0;//设置位置
                mainWindow.Top = 0;
                Rect rc = SystemParameters.WorkArea;//获取工作区大小
                mainWindow.Width = rc.Width;
                mainWindow.Height = rc.Height;
            }
            ismax = !ismax;
        }
        public void Close()
        {
            mainWindow.Close();
        }
        public void SetMovePos(double x, double y)
        {
            var wx = mainWindow.Left;
            var wy = mainWindow.Left;
            var nowp = new Point(mainWindow.Left, mainWindow.Top);
            nowp.Offset(x, y);
            mainWindow.Left = nowp.X;
            mainWindow.Top = nowp.Y;
        }
    }
}
