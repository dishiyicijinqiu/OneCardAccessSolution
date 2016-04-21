using DevExpress.Xpf.Core;
using System;
using System.Windows;

namespace FengSharp.OneCardAccess.Client.PC.UI
{
    /// <summary>
    /// LoadingView.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingView : Window, ISplashScreen
    {
        public LoadingView()
        {
            InitializeComponent();
        }

        public void CloseSplashScreen()
        {
            this.Close();
        }

        public void Progress(double value)
        {
        }

        public void SetProgressState(bool isIndeterminate)
        {
        }
    }
}
