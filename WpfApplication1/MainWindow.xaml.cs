using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebBrowserOverlay wbo;
        private System.Windows.Forms.WebBrowser fwb;
        MainWindowScriptProxy proxy;
        public MainWindow()
        {
            InitializeComponent();
            proxy = new MainWindowScriptProxy(this);

            // 在此点下面插入创建对象所需的代码。  
            wbo = new WebBrowserOverlay(this.grid);
            fwb = wbo.WebBrowser;

            fwb.ObjectForScripting = proxy;
            fwb.ScriptErrorsSuppressed = true;
            fwb.Navigate(new Uri("http://dishiyicijinqiu.eicp.net:17515/WebSite/Devices/WPF/Index.html"));
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
