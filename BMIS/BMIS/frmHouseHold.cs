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
    public partial class frmHouseHold : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        frmResident f;
        public frmHouseHold(frmResident f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
            this.f = f;
        }

        public void LoadRecords()
        {
            try
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tblresident where (lname + ', ' + fname + ' ' + mname) like '%" + txtSearch.Text + "%' and category like 'HEAD OF THE FAMILY'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["id"].ToString(), dr["house"].ToString(), dr["lname"].ToString() + ", " +  dr["fname"].ToString() + " " + dr["mname"].ToString(), dr["address"].ToString());
                }
                dr.Close();
                cn.Close();
                dataGridView1.ClearSelection();
                lblCount.Text = "Record count ( " + dataGridView1.RowCount + " )";
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname == "btnSelect")
            {
                f.txtHouse.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtHead.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }
    }

   
}
