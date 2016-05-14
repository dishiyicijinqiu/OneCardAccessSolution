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
        //public P_CRTempView(object ParentViewModel, P_CRTempEditMessage EditMessage)
        //{
        //    InitializeComponent();
        //    this.ParentDataContext = ParentViewModel;
        //    this.Loaded += (sender, e) =>
        //    {
        //        try
        //        {
        //            this.DataContext = new P_CRTempViewModel(this.ParentDataContext, EditMessage);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ex.HandleException(this);
        //            InterCloseDocument();
        //        }
        //    };
        //}
        public P_CRTempView(P_CRTempViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
