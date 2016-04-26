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
using DevExpress.Xpf.Core;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : BaseUserControl
    {
        public RegisterView(object ParentViewModel, RegisterEditMessage registerEditMessage)
        {
            InitializeComponent();
            var vm = new RegisterViewModel(ParentViewModel, registerEditMessage);
            this.DataContext = vm;
            vm.Init();
        }
    }
}
