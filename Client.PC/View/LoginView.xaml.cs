using System.Windows.Controls;

namespace FengSharp.OneCardAccess.Client.PC.View
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            this.Loaded += LoginView_Loaded;
        }

        private void LoginView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            App.Current.MainWindow.Topmost = true;
            App.Current.MainWindow.Topmost = false;
            this.tbUserName.Focus();
        }
    }
}
