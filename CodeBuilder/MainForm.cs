using FengSharp.OneCardAccess.CodeBuilder.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FengSharp.OneCardAccess.CodeBuilder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
        }

        private void btnBuildCode_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.comboBox1.Text)
                {
                    default:
                        break;
                    case "RuntimeBusinessEntity":
                        GeneRuntimeBusinessEntity();
                        break;
                    case "RuntimeTableEntity":
                        GeneRuntimeTableEntity();
                        break;
                }
                MessageBox.Show("Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GeneRuntimeBusinessEntity()
        {
            var lines = this.textBox1.Lines;
            foreach (var line in lines)
            {
                var datas = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (datas.Length != 3)
                    continue;
                string _namespace = datas[0];
                string tablename = datas[1];
                string buildpath = datas[2];
                string dir = Path.GetDirectoryName(buildpath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                RuntimeBusinessEntity.GeneCodeToPath(buildpath, tablename, _namespace);
            }
        }

        private void GeneRuntimeTableEntity()
        {
            var lines = this.textBox1.Lines;
            foreach (var line in lines)
            {
                var datas = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (datas.Length != 3)
                    continue;
                string _namespace = datas[0];
                string tablename = datas[1];
                string buildpath = datas[2];
                string dir = Path.GetDirectoryName(buildpath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                RuntimeTableEntity.GeneCodeToPath(buildpath, tablename, _namespace);
            }
        }
    }
}
