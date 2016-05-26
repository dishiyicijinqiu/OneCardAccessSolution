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
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Events;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;

namespace FengSharp.OneCardAccess.Client.PC.UI
{
    /// <summary>
    /// Interaction logic for BaseRibbonWindow.xaml
    /// </summary>
    public partial class BaseRibbonWindow : DXRibbonWindow
    {
        public BaseRibbonWindow()
        {
            InitializeComponent();
            var vm = new BaseRibbonWindowVM();
            this.DataContext = vm;
            vm.CloseEventToken = DefaultEventAggregator.Current.GetEvent<CloseEvent>().Subscribe(OnInterClose);
        }

        private void OnInterClose(SubscriptionToken sender, CloseEventArgs args)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                var vm = this.DataContext as BaseRibbonWindowVM;
                if (sender != vm.CloseEventToken)
                    return;
                this.Close();
            }));
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
    }
    public class BaseRibbonWindowVM : NotificationObject
    {
        public SubscriptionToken CloseEventToken { get; set; }
        public ICommand CloseCommand { get; private set; }
        public BaseRibbonWindowVM()
        {
            CloseCommand = new DelegateCommand(Close);
        }
        public void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken);
        }
    }
}
