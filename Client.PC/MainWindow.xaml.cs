using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using FengSharp.OneCardAccess.Core;
using System;
using System.ComponentModel;
using System.Windows;

namespace FengSharp.OneCardAccess.Client.PC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXRibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += MainWindow_SourceInitialized;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            System.IntPtr handle = (new System.Windows.Interop.WindowInteropHelper(this)).Handle;
            System.Windows.Interop.HwndSource.FromHwnd(handle).AddHook(new System.Windows.Interop.HwndSourceHook(new MaximizedNoCoverTaskInfoHelper()
            {
                MinWidth = (int)this.MinWidth,
                MinHeight = (int)this.MinHeight
            }.WindowProc));
        }
    }
}
