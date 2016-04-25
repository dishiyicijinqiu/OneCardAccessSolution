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
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using System.ComponentModel;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.Client.PC.UI;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView(RegisterEditMessage registerEditMessage)
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                //DefaultEventAggregator.Current.GetEvent<ExceptionEvent<RegisterViewModel>>().Subscribe(OnException);
                //DefaultEventAggregator.Current.GetEvent<CloseEvent<RegisterViewModel>>().Subscribe(OnClose);
                //DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<RegisterViewModel>>().Subscribe(OnMessage);
                //this.DataContext = new RegisterViewModel(registerEditMessage);
            }
        }
    }
}
