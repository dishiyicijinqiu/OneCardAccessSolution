using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ColumnsManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var employee = new EmployeesData();
                employee.Add(new Employee() { UserName = "zs", Id = 1, Settings = SettingsType.Default });
                employee.Add(new Employee() { UserName = "ls", Id = 2, Settings = SettingsType.Date });
                this.SaveEmployeesData(employee);
                //var result = GetEmployeesData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public EmployeesData GetEmployeesData()
        {
            using (FileStream fs = new FileStream(@"D:\1.xml", FileMode.Open))
            {
                XmlSerializer s = new XmlSerializer(typeof(EmployeesData));
                return (EmployeesData)s.Deserialize(fs);
            }
        }

        public void SaveEmployeesData(EmployeesData data)
        {
            using (FileStream fs = new FileStream(@"D:\1.xml", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    XmlSerializer s = new XmlSerializer(typeof(EmployeesData));
                    s.Serialize(sw, data);
                }
            }
        }
    }
    [Serializable]
    [XmlRoot(ElementName = "EmployeesData")]
    public class EmployeesData : List<Employee>
    {

    }
    [Serializable]
    //[XmlRoot(ElementName = "员工")]
    public class Employee
    {
        //[XmlElement(ElementName = "Id")]
        public int Id { get; set; }
        //[XmlElement(ElementName = "姓名")]
        public string UserName { get; set; }
        [XmlElement(ElementName = "类型")]
        public SettingsType Settings { get; set; }
    }
    public enum SettingsType { Default, CheckEdit, Date, Combobox, Image }
}
