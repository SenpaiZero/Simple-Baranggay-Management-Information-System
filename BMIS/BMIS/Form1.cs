using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParams = base.CreateParams;
                handleParams.ExStyle |= 0x02000000;
                return handleParams;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = globalVariable.username;
            label4.Text = globalVariable.position;
            this.Left = 0;
            this.Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            if(globalVariable.position.ToLower() == "admin")
            {
                //all access avail
            }
            else if(globalVariable.position.ToLower() == "standard user")
            {
                btnDocument.Enabled = false;
                btnIssue.Enabled = false;
                btnMaintenance.Enabled = false;
            }
            else if(globalVariable.position.ToLower() == "guest user")
            {
                btnResident.Enabled = false;
                btnDocument.Enabled = false;
                btnIssue.Enabled = false;
                btnMaintenance.Enabled = false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {/*
            int y = Screen.PrimaryScreen.Bounds.Height;
            int x = Screen.PrimaryScreen.Bounds.Width;
            this.Height = y - 30;
            this.Width = x;
            this.Left = 0;
            this.Top = 0;*/

        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            frmDocument f = new frmDocument();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Show();
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            frmMaintenance f = new frmMaintenance();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.LoadRecord();
            f.LoadPurok();
            f.BringToFront();
            f.Show();
        }

        private void btnResident_Click(object sender, EventArgs e)
        {
            frmResidentList f = new frmResidentList();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Loadrecords();
            f.LoadVaccination();
            f.LoadArchive();
            f.LoadHead();
            f.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmIssue f = new frmIssue();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.LoadRecords();
            f.BringToFront();
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmlogin login = new frmlogin();
            this.Hide();
            login.ShowDialog();
            this.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {

            frmDashboard f = new frmDashboard();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Loadrecords();
            f.Show();
        }
    }
}
