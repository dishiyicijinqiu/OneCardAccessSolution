using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC;


namespace FengSharp.OneCardAccess.Client.PC.View.RBAC
{
    /// <summary>
    /// UserView.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : BaseUserControl, IUserView
    {
        public UserView(UserViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public UserView()
        {
            InitializeComponent();
        }
    }
}
