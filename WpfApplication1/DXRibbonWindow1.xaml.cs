using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using DevExpress.XtraReports.UI;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for DXRibbonWindow1.xaml
    /// </summary>
    public partial class DXRibbonWindow1 : DXRibbonWindow
    {
        public DXRibbonWindow1()
        {
            InitializeComponent();
        }
        public DXRibbonWindow1(List<UserInfoEntity> users)
        {
            InitializeComponent();
            var report = new XtraReport();
            report.DataSource = users;
            reportDesigner.OpenDocument(report);
            //reportDesigner.FileStorage = new ReportFileStorage();
        }
    }
}
