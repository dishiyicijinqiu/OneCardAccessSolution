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


namespace FengSharp.OneCardAccess.Client.PC.UI
{
    /// <summary>
    /// Interaction logic for BaseReportDesignerWindow.xaml
    /// </summary>
    public partial class BaseReportDesignerWindow : DXRibbonWindow
    {
        public BaseReportDesignerWindow(DevExpress.XtraReports.UI.XtraReport report)
        {
            InitializeComponent();
            this.reportDesigner.OpenDocument(report);
            
        }
    }
}
