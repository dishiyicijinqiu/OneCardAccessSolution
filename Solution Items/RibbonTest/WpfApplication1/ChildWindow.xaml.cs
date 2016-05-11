using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// ChildWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChildWindow : Window
    {
        public ChildWindow()
        {
            InitializeComponent();
            this.Loaded += ChildWindow_Loaded;
        }

        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                this.DataContext = new ChildWindowViewModel();
            }
        }
    }
    public class ChildWindowViewModel : INotifyPropertyChanged
    {
        public ChildWindowViewModel()
        {
            try
            {
                //throw new Exception("wrong");
                this.CurrentUser = new UserInfo() { UserId = 5, UserName = "zs" };
                Items = new ObservableCollection<UserInfo>() {
                    new UserInfo() {  UserId=3, UserName="张三"}
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ObservableCollection<UserInfo> Items { get; private set; }
        private UserInfo _CurrentUser;

        public UserInfo CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
