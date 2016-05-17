using DevExpress.Xpf.Printing;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using System.Collections.Generic;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<UserInfoEntity> users = new List<UserInfoEntity>();
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                var user = new UserInfoEntity();
                user.IsLock = i % 2 == 0 ? true : false;
                user.IsSuper = i % 2 == 0 ? false : true;
                user.PassWord = "335698";
                user.Remark = "备注" + i.ToString();
                //user.UserId = i;
                user.UserName = "UserName" + i.ToString();
                user.UserNo = "UserNo" + i.ToString();
                users.Add(user);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            XtraReport5 report = new XtraReport5();
            PrintHelper.ShowRibbonPrintPreviewDialog(this, report);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            XtraReport1 report = new XtraReport1();
            report.DataAdapter = users;
            PrintHelper.ShowRibbonPrintPreviewDialog(this, report);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            DXRibbonWindow1 dxw = new DXRibbonWindow1(users);
            dxw.ShowDialog();
        }
    }
}
