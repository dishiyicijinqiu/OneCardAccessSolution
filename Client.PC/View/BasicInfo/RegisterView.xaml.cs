using DevExpress.Xpf.Grid;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : BaseUserControl, Interfaces.IView
    {
        public RegisterView(RegisterViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public RegisterView()
        {
            InitializeComponent();
        }
    }
}
