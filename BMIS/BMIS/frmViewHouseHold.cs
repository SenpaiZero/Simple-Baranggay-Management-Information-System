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
    public partial class frmViewHouseHold : Form
    {
        frmResidentList f;
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public frmViewHouseHold(frmResidentList f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
            this.f = f;
        }
        public frmViewHouseHold()
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecord(string houseNum)
        {
            try
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                cm = new SqlCommand($"select * from tblResident where house = @house and category = 'MEMBER'", cn);
                cm.Parameters.AddWithValue("house", houseNum);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {

                    dataGridView1.Rows.Add(dr["id"].ToString(), dr["house"].ToString(), dr["lname"].ToString() + ", " + dr["fname"].ToString() + " " + dr["mname"].ToString(), dr["income"].ToString(), dr["relationHead"].ToString(),
                        dr["gender"].ToString(), dr["civilstatus"].ToString(), dr["education"].ToString(), dr["bplace"].ToString(), dr["schoolYouth"].ToString(), dr["outOfSchoolYouth"].ToString(),
                        dr["employment"].ToString(), dr["occupation"].ToString());

                }
                dr.Close();
                cn.Close();
                dataGridView1.ClearSelection();
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
