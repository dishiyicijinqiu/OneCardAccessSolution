using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// P_CRTempView.xaml 的交互逻辑
    /// </summary>
    public partial class P_CRTempView : BaseUserControl
    {
        public P_CRTempView(object ParentViewModel, P_CRTempEditMessage p_crTempEditMessage)
        {
            InitializeComponent();
            var vm = new P_CRTempViewModel(ParentViewModel, p_crTempEditMessage);
            this.DataContext = vm;
            vm.Init();
        }
    }
}
