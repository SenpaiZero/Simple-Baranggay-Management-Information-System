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
    public partial class frmIssue : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public frmIssue()
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmBlotter f = new frmBlotter(this);
            f.lblFile.Text = f.GetFileNO(); 
            f.btnUpdate.Enabled = false;
            f.ShowDialog();
        }

        public void LoadRecords()
        {
            try 
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    cm = new SqlCommand("select * from tblBlotter", cn);
                }
                else
                {
                    cm = new SqlCommand("select * from tblBlotter WHERE fileno LIKE @file", cn);
                    cm.Parameters.AddWithValue("@file", $"%{txtSearch.Text}%");
                }
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["id"].ToString(), dr["fileno"].ToString(), dr["baranggay"].ToString(), dr["purok"].ToString(), dr["incident"].ToString(), dr["place"].ToString(), DateTime.Parse(dr["idate"].ToString()).ToShortDateString(), dr["itime"].ToString(), dr["complainant"].ToString(), dr["witness1"].ToString(), dr["witness2"].ToString(), dr["narrative"].ToString(), dr["status"].ToString());
                }
                cn.Close();
                dataGridView1.ClearSelection();
            
            }catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        public void loadSettled()
        {
            try
            {
                dataGridView3.Rows.Clear();
                cn.Open();
                if (string.IsNullOrEmpty(tbSearch.Text))
                {
                    cm = new SqlCommand("select * from tblBlotterSettled", cn);
                }
                else
                {
                    cm = new SqlCommand("select * from tblBlotterSettled WHERE fileno LIKE @file", cn);
                    cm.Parameters.AddWithValue("@file", $"%{tbSearch.Text}%");
                }
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView3.Rows.Add(dr["id"].ToString(), dr["fileno"].ToString(), dr["baranggay"].ToString(), dr["purok"].ToString(), dr["incident"].ToString(), dr["place"].ToString(), DateTime.Parse(dr["idate"].ToString()).ToShortDateString(), dr["itime"].ToString(), dr["complainant"].ToString(), dr["witness1"].ToString(), dr["witness2"].ToString(), dr["narrative"].ToString(), dr["status"].ToString());
                }
                cn.Close();
                dataGridView3.ClearSelection();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = dataGridView1.Columns[e.ColumnIndex].Name;
                if (colname == "btnEdit")
                {
                    frmBlotter f = new frmBlotter(this);
                    f._id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.lblFile.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.txtBrgy.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    f.txtPurok.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    f.txtIncident.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    f.txtPlace.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    f.dtDate.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                    f.txtTime.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    f.txtComplainant.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    f.txtWitness1.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    f.txtWitness2.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    f.txtComplainant.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    f.btnSave.Enabled = false;
                    f.ShowDialog();
                }
                else if (colname == "btnDelete")
                {
                    if (MessageBox.Show("Do you want to delete this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblBlotterSettled(fileno, baranggay, purok, incident, place, idate, itime, complainant, witness1, witness2,narrative,status)" +
                            " SELECT fileno, baranggay, purok, incident, place, idate, itime, complainant, witness1, witness2,narrative,status " +
                            $"FROM tblBlotter WHERE id like '{dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()}'", cn);
                        cm.ExecuteNonQuery();
                        cm = new SqlCommand("delete from tblBlotter where id like '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("record ha been successfully deleted!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecords();
                        loadSettled();
                    }
                }
                else if (colname == "btnPrint") 
                {
                    frmReport f = new frmReport();
                    f.PreviewBlotter("select * from tblblotter where id like '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'");
                    f.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            LoadRecords();
        }
    }
}
