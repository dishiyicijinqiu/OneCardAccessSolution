using DevExpress.Xpf.Core;
using DevExpress.Xpf.Reports.UserDesigner;
using DevExpress.Xpf.Reports.UserDesigner.Native;
using DevExpress.XtraReports.UI;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using System;
using System.Collections.Generic;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for DXWindow1.xaml
    /// </summary>
    public partial class DXWindow1 : DXWindow
    {
        public DXWindow1()
        {
            InitializeComponent();
        }
        public DXWindow1(List<UserInfoEntity> users)
        {
            InitializeComponent();
            var report = new XtraReport();
            report.DataSource = users;
            reportDesigner.OpenDocument(report);
            //reportDesigner.FileStorage = new ReportFileStorage();
        }
    }
    public class ReportFileStorage : IReportFileStorage
    {
        public string GetErrorMessage(Exception exception)
        {
            return exception.Message + "我的";
        }

        public XtraReport Load(string filePath)
        {
            var report = new XtraReport();
            report.LoadLayout(filePath);
            return report;
        }

        public void Save(string filePath, XtraReport report)
        {
            report.SaveLayout(filePath);
        }

        public string ShowOpenDialog(IReportDesignerUI designer)
        {
            return string.Empty;
        }

        public string ShowSaveAsDialog(string filePath, string reportTitle, IReportDesignerUI designer)
        {
            return string.Empty;
        }
    }
}
