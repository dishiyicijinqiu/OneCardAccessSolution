using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace FengSharp.OneCardAccess.Client.PC.ReportTool
{
    public partial class RibbonrEportDesignerForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RibbonrEportDesignerForm(string reportfilePath)
        {
            InitializeComponent();
            //reportDesigner1.DesignPanelLoaded += ReportDesigner1_DesignPanelLoaded;
            this.reportDesigner1.OpenReport(reportfilePath);
            //commandBarItem32.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFile;
            //reportDesigner1.OpenReport();
            //reportDesigner1.ActiveDesignPanel.SaveReport()
        }
    }
}