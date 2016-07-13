using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// CommodityCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class CommodityCollectionView : BaseUserControl, ICommodityCollectionView
    {
        public CommodityCollectionView(CommodityCollectionViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public CommodityCollectionView()
        {
            InitializeComponent();
        }
    }
}
