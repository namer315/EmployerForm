using EmployerForm.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployerForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void label1_Click(object sender, EventArgs e)
        {
            
        }


        private void groupBox3_Enter(object sender, EventArgs e)
        {
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            loadEmployeeData();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            loadEmployeeData();
        }

        //For now I only code this part
        //To display details that has entered on MsSql17 server
        private void loadEmployeeData()
        {
            NHibernate.ISession session = (NHibernate.ISession)SessionFactory.OpenSession;

            using (session)
            {
                IQuery query = session.CreateQuery("From Employee");
                IList<Model.Employee> empInfo = query.List<Model.Employee>();
                Employee.DataSource = empInfo;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            loadEmployeeData();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
