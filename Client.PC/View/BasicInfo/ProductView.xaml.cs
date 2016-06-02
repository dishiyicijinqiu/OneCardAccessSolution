using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// ProductView.xaml 的交互逻辑
    /// </summary>
    public partial class ProductView : BaseUserControl, IProductView
    {
        public ProductView(ProductViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public ProductView()
        {
            InitializeComponent();
        }
    }
}
