using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System.Collections.Generic;
using System.Windows;

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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                List<FengSharp.OneCardAccess.BusinessEntity.RBAC.UserInfoEntity> list = new List<BusinessEntity.RBAC.UserInfoEntity>();
                list.Add(new FengSharp.OneCardAccess.BusinessEntity.RBAC.UserInfoEntity()
                {
                    UserNo = "UserNo",
                    UserName = "UserName",
                    UserId = 1,
                    Remark = "Remark",
                    IsLock = false,
                    IsSuper = false,
                    PassWord = "PassWord"
                });
                list.Add(new FengSharp.OneCardAccess.BusinessEntity.RBAC.UserInfoEntity()
                {
                    UserNo = "UserNo1",
                    UserName = "UserName1",
                    UserId = 2,
                    Remark = "Remark1",
                    IsLock = true,
                    IsSuper = true,
                    PassWord = "PassWord1"
                });


                var report = new P_CRTempReport();
                //report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() { Visible = false, Description = "用户编号", Name = "UserNo", Type = typeof(string), Value = "UserNo" });
                //report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() { Visible = false, Description = "用户名称", Name = "UserName", Type = typeof(string), Value = "UserName" });
                //report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() { Visible = false, Description = "备注", Name = "Remark", Type = typeof(string), Value = "Remark" });



                report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() { Visible = false, Description = "用户编号", Name = "用户编号", Type = typeof(string), Value = "UserNo" });
                report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() { Visible = false, Description = "用户名称", Name = "用户名称", Type = typeof(string), Value = "UserName" });
                report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() { Visible = false, Description = "备注", Name = "备注", Type = typeof(string), Value = "Remark" });
                report.DataSource = list;
                //report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter()
                //{
                //    MultiValue = true,
                //    Visible = false,
                //    Description = "UserInfoEntity",
                //    Name = "用户信息",
                //    Type = typeof(BusinessEntity.RBAC.UserInfoEntity),
                //    Value = userinfoentity
                //});
                //var para = new DevExpress.XtraReports.Parameters.Parameter();

                var window = new UI.BaseReportDesignerWindow(report);
                window.Title = Properties.Resources.View_P_CRTempView_Title;
                window.Owner = Window.GetWindow(this);
                window.ShowDialog();
            }
            catch (System.Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this.DataContext, new ExceptionEventArgs(ex));
            }
        }
    }
}
