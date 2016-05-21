using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC;

namespace FengSharp.OneCardAccess.Client.PC.View.RBAC
{
    /// <summary>
    /// UserGroupView.xaml 的交互逻辑
    /// </summary>
    public partial class UserGroupView : BaseUserControl, IUserGroupView
    {
        public UserGroupView(UserGroupViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public UserGroupView()
        {
            InitializeComponent();
        }
    }
}
