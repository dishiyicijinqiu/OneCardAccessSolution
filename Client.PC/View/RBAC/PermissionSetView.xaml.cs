using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC;
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

namespace FengSharp.OneCardAccess.Client.PC.View.RBAC
{
    /// <summary>
    /// PermissionSetView.xaml 的交互逻辑
    /// </summary>
    public partial class PermissionSetView : BaseUserControl, IPermissionSetView
    {
        public PermissionSetView(PermissionSetViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public PermissionSetView()
        {
            InitializeComponent();
        }
    }
}
