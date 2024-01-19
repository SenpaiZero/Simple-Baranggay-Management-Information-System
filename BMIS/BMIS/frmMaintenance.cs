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
using System.IO;
namespace BMIS
{
    public partial class frmMaintenance : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public frmMaintenance()
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmOfficials f = new frmOfficials(this);
            f.btnUpdate.Enabled = false;
            f.ShowDialog();
        }

        public void LoadRecord()
        {
            try
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tblofficial", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["id"].ToString(), dr["name"].ToString(), dr["chairmanship"].ToString(), dr["position"].ToString(), DateTime.Parse(dr["termstart"].ToString()).ToShortDateString(), DateTime.Parse(dr["termend"].ToString()).ToShortDateString(), dr["status"].ToString());
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

        public void LoadPurok()
        {
            try
            {
                dataGridView2.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tblpurok", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr["purok"].ToString(), dr["chairman"].ToString());
                }
                dr.Close();
                cn.Close();
                dataGridView2.ClearSelection();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;
                if (colName == "btnEdit1")
                {
                    frmOfficials f = new frmOfficials(this);
                    f.btnSave.Enabled = false;
                    f._id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.cboChairmanship.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    f.cboPosition.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    f.dtStart.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    f.dtEnd.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    f.cboStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    f.ShowDialog();
                }else if(colName == "btnDelete1")
                {
                    if(MessageBox.Show("Do you want to delete this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("delete from tblofficial where id like '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been successfully deleted!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecord();
                    }
                }
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView2.Columns[e.ColumnIndex].Name;
                if (colName == "btnEdit2")
                {
                    frmPurok f = new frmPurok(this);
                    f.btnSave.Enabled = false;
                    f._purok = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.txtPurok.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.txtChairman.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.ShowDialog();
                }
                else if (colName == "btnDelete2")
                {
                    if (MessageBox.Show("Do you want to delete this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("delete from tblpurok where purok like '" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been successfully deleted!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPurok();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPurok f = new frmPurok(this);
            f.btnUpdate.Enabled = false;
            f.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string user = globalVariable.username;

            using (SqlConnection cn = new SqlConnection(dbconstring.connection))
            {
                cn.Open();
                String query = $"SELECT password FROM login_new WHERE username = '{user}'";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        if(currentPass.Text == dr[0].ToString())
                        {
                            dr.Close();
                            if(newPass.Text == reNewPass.Text && !string.IsNullOrEmpty(newPass.Text))
                            {
                                String query2 = $"UPDATE login_new SET password = '{newPass.Text}' WHERE username = '{user}'";
                                using (SqlCommand cmd2 = new SqlCommand(query2, cn))
                                {
                                    cmd2.ExecuteNonQuery();
                                }

                                currentPass.Text = String.Empty;
                                newPass.Text = String.Empty;
                                reNewPass.Text = String.Empty;
                                MessageBox.Show("You've successfully updated the password", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Password are not the same", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Current Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                cn.Close();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(dbconstring.connection))
            {
                cn.Open();
                Boolean isUnique = true;
                String query = $"SELECT username FROM login_new";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[0].ToString() == username.Text)
                        {
                            MessageBox.Show("Username already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            isUnique = false;
                            break;
                        }
                    }
                    dr.Close();
                }

                if(isUnique)
                {
                    if(password.Text != rePassword.Text)
                    {
                        MessageBox.Show("Password are not the same", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    String query2 = $"INSERT INTO login_new (username, password) VALUES ('{username.Text}', '{password.Text}')";
                    using (SqlCommand cm2 = new SqlCommand(query2, cn))
                    {
                        cm2.ExecuteNonQuery();
                        username.Text = string.Empty; 
                        password.Text = string.Empty; 
                        rePassword.Text = string.Empty;
                        MessageBox.Show("You've successfully added a new account", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
