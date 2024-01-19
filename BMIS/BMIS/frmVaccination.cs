using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BMIS
{
    public partial class frmVaccination : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        public string _id;
        frmResidentList f;
        public frmVaccination(frmResidentList f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
            this.f = f;
        }

        private void cboStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Do you want to save changes?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (CheckDuplicate("select count(*) from tblvaccine where rid like '" + _id + "'") == true)
                    {
                        cn.Open();
                        cm = new SqlCommand("update tblvaccine set vaccine =@vaccine, status =@status where rid = @rid", cn);
                        cm.Parameters.AddWithValue("@vaccine", txtVaccine.Text);
                        cm.Parameters.AddWithValue("@status", cboStatus.Text);
                        cm.Parameters.AddWithValue("@rid", _id);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }else
                    {
                        cn.Open();
                        cm = new SqlCommand("insert into tblvaccine (rid,vaccine,status) values(@rid, @vaccine,@status)", cn);
                        cm.Parameters.AddWithValue("@rid", _id);
                        cm.Parameters.AddWithValue("@vaccine", txtVaccine.Text);
                        cm.Parameters.AddWithValue("@status", cboStatus.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    MessageBox.Show("Record has been successfully saved!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadVaccination();
                    f.Loadrecords();
                    this.Dispose();
                    
                }
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool CheckDuplicate(string sql)
        {
            bool duplicate= false;
            try
            {
                cn.Open();
                cm = new SqlCommand(sql, cn);
                int count = int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();

                if (count == 0) duplicate = false; else duplicate = true;
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return duplicate;
        }
    }
}
