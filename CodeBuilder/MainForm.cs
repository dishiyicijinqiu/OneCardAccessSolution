using FengSharp.OneCardAccess.CodeBuilder.Templates.RunTime;
using System;
using System.IO;
using System.Windows.Forms;

namespace FengSharp.OneCardAccess.CodeBuilder
{
    public partial class MainForm : Form
    {
        string datapath = Path.GetFullPath("data.txt");
        string historydatapath = Path.GetFullPath("historydata.txt");
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            if (File.Exists(datapath))
            {
                var lines = File.ReadAllLines(datapath);
                for (int i = 0; i < lines.Length; i++)
                {
                    this.textBox1.AppendText(lines[i]);
                    if (i != lines.Length - 1)
                        this.textBox1.AppendText(Environment.NewLine);
                }
            }

            if (File.Exists(historydatapath))
            {
                var lines = File.ReadAllLines(historydatapath);
                for (int i = 0; i < lines.Length; i++)
                {
                    this.textBox3.AppendText(lines[i]);
                    if (i != lines.Length - 1)
                        this.textBox3.AppendText(Environment.NewLine);
                }
            }
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(datapath))
            {
                File.Delete(datapath);
            }
            var lines = this.textBox1.Lines;
            File.WriteAllLines(datapath, lines);

            if (File.Exists(historydatapath))
            {
                File.Delete(historydatapath);
            }
            var historylines = this.textBox3.Lines;
            File.WriteAllLines(historydatapath, historylines);
        }

    }
}
