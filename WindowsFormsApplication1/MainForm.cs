using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            webBrowser1.ObjectForScripting = this;
            webBrowser1.ScriptErrorsSuppressed = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://192.168.18.2/WebSite/Devices/WPF/Index.html");
        }
        public void MinWindow()
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public void MaxWindow()
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }
        public void SetMovePos(int x, int y)
        {
            var nowp = Location;
            nowp.Offset(x, y);
            Location = nowp;
        }
    }
}
