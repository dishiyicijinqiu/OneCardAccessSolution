using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;

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
