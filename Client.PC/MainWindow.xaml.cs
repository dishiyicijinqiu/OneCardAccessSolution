using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Unity;
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
            var mainview = ServiceLoader.LoadService<IView>("MainView", new ParameterOverride("VM", ServiceLoader.LoadService<IMainView>()));
            this.AddChild(mainview);
            this.SourceInitialized += MainWindow_SourceInitialized;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (Common.Session.Current != null)
            {
                if (DXMessageBox.Show(this, Properties.Resources.Info_ConfirmToExit, Properties.Resources.Info_Title, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                    e.Cancel = true;
            }
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
