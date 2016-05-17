using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Core;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// P_CRTempView.xaml 的交互逻辑
    /// </summary>
    public partial class P_CRTempView : BaseUserControl, IView
    {
        public P_CRTempView(P_CRTempViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public P_CRTempView()
        {
            InitializeComponent();
        }
    }
}
