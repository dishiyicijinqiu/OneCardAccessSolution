using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// CommodityView.xaml 的交互逻辑
    /// </summary>
    public partial class CommodityView : BaseUserControl, ICommodityView
    {
        public CommodityView(CommodityViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public CommodityView()
        {
            InitializeComponent();
        }
    }
}
