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
    public partial class frmResidentList : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public frmResidentList()
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
             frmResident f = new frmResident(this);
            f.btnUpdate.Enabled = false;
            f.Clear();
            f.ShowDialog();
        }

        public void Loadrecords()
        {
            try
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                if(string.IsNullOrEmpty(txtSearch.Text))
                    cm = new SqlCommand("select * from tblResident", cn);
                else
                    cm = new SqlCommand("select * from tblResident where nid like '%" + txtSearch.Text + "%'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["id"].ToString(), dr["nid"].ToString(), dr["lname"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["alias"].ToString(), dr["address"].ToString(), dr["house"].ToString(), dr["category"].ToString(), DateTime.Parse(dr["bdate"].ToString()).ToShortDateString(), dr["age"].ToString(), dr["gender"].ToString(), dr["civilstatus"].ToString());
                }
                dr.Close();
                cn.Close();
                dataGridView1.ClearSelection();
                
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadArchive()
        {
            try
            {
                dataGridView4.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tblArchive where lname like '%" + txtSearch.Text + "%' or fname like '%" + txtSearch.Text + "%'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView4.Rows.Add(dr["id"].ToString(), dr["nid"].ToString(), dr["lname"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["alias"].ToString(), dr["address"].ToString(), dr["house"].ToString(), dr["category"].ToString(), DateTime.Parse(dr["bdate"].ToString()).ToShortDateString(), dr["age"].ToString(), dr["gender"].ToString(), dr["civilstatus"].ToString());
                }
                dr.Close();
                cn.Close();
                dataGridView4.ClearSelection();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadHead()
        {
            try
            {
                dataGridView2.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tblResident where (lname like '%" + txtSearch1.Text + "%' or fname like '%" + txtSearch.Text + "%') and category like 'HEAD OF THE FAMILY'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr["id"].ToString(), dr["nid"].ToString(), dr["lname"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["alias"].ToString(), dr["address"].ToString(), dr["house"].ToString(), dr["category"].ToString(), DateTime.Parse(dr["bdate"].ToString()).ToShortDateString(), dr["age"].ToString(), dr["gender"].ToString(), dr["civilstatus"].ToString());
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

        public void LoadVaccination()
        {
            try
            {
                dataGridView3.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from vaccination where (lname like '%" + metroTextBox1.Text + "%' or fname like '%" + metroTextBox1.Text + "%') and status like '" + cboStatus.Text + "'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView3.Rows.Add(dr["id"].ToString(), dr["lname"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["vaccine"].ToString(), dr["status"].ToString());
                }
                dr.Close();
                cn.Close();
                dataGridView3.ClearSelection();

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
                string colname = dataGridView1.Columns[e.ColumnIndex].Name;
                if(colname == "btnEdit")
                {
                    frmResident f = new frmResident(this);
                    f.LoadPurok();
                    cn.Open();
                    cm = new SqlCommand("select pic as picture, * from tblResident where id like '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        long len = dr.GetBytes(0, 0, null, 0, 0);
                        byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                        dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));

                        f._id = dr["id"].ToString();
                        f.txtID.Text = dr["nid"].ToString();
                        f.txtLname.Text = dr["lname"].ToString();
                        f.txtFname.Text = dr["fname"].ToString();
                        f.txtMI.Text = dr["mname"].ToString();
                        f.txtAddress.Text = dr["address"].ToString();
                        f.txtAge.Text = dr["age"].ToString();
                        f.txtContact.Text = dr["contact"].ToString();
                        f.txtEducational.Text = dr["educational"].ToString();
                        f.txtEmail.Text = dr["email"].ToString();
                        f.txtHead.Text = dr["head"].ToString();
                        f.txtHouse.Text = dr["house"].ToString();
                        f.txtOccupation.Text = dr["occupation"].ToString();
                        f.txtPlace.Text = dr["bplace"].ToString();
                        f.txtReligion.Text = dr["religion"].ToString();
                        f.cboCategory.Text = dr["category"].ToString();
                        f.cboCivil.Text  = dr["civilstatus"].ToString();
                        f.cboDisability.Text = dr["disability"].ToString();
                        f.cboGender.Text = dr["gender"].ToString();
                        f.cboPurok.Text = dr["purok"].ToString();
                        f.cboStatus.Text = dr["status"].ToString();
                        f.cboVoters.Text = dr["voters"].ToString();
                        f.dtBdate.Value = DateTime.Parse(dr["bdate"].ToString());
                        MemoryStream ms = new MemoryStream(array);
                        Bitmap bitmap = new Bitmap(ms);
                        f.picImage.BackgroundImage = bitmap;
                        f.btnSave.Enabled = false;
                    }
                    dr.Close();
                    cn.Close();
                    f.ShowDialog();
                }else if (colname == "btnDelete")
                {
                    if (MessageBox.Show("Do you want to delte this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                    {
                        cn.Open();

                        cm = new SqlCommand("INSERT INTO tblArchive (nid, lname, fname, mname, alias, bdate, bplace, age, civilstatus, gender, religion, email, contact, voters, precinct, purok, educational, occupation, address, category, house, head, disability, status, pic) " +
                            "SELECT nid, lname, fname, mname, alias, bdate, bplace, age, civilstatus, gender, religion, email, contact, voters, precinct, purok, educational, occupation, address, category, house, head, disability, status, pic FROM tblResident " +
                            $"WHERE Id = '{dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()}'", cn);
                        cm.ExecuteNonQuery();

                        cm = new SqlCommand("delete from tblResident where id = '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been successfully deleted!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Loadrecords();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Loadrecords();
        }

        private void txtSearch1_TextChanged(object sender, EventArgs e)
        {
            LoadHead();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           try
            {
                string colName = dataGridView2.Columns[e.ColumnIndex].Name;
                if (colName == "btnView")
                {
                    string _id = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    frmViewHouseHold f = new frmViewHouseHold(this);
                    cn.Open();
                    cm = new SqlCommand("select (lname + ', ' + fname + ' ' + mname) as fullname, house from tblResident where id =@id", cn);
                    cm.Parameters.AddWithValue("@id", _id);
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        f.lblHouseNo.Text = dr["house"].ToString();
                        f.lblname.Text = dr["fullname"].ToString();
                        f.LoadRecord(dr["house"].ToString());
                    }
                    dr.Close();
                    cn.Close();
                    f.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title,MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView3.Columns[e.ColumnIndex].Name;
            if (colname == "colEditVaccination")
            {
                frmVaccination f = new frmVaccination(this);
                f._id = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
                f.lblname.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString() + ", " + dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString() + " " + dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString() ;
                f.txtVaccine.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.cboStatus.Text = dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.ShowDialog();
            }
        }

        private void cboStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVaccination();
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            LoadVaccination();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblVoters_Click(object sender, EventArgs e)
        {

        }

        private void lblVaccination_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = dataGridView4.Columns[e.ColumnIndex].Name;
                if (colname == "btnEditA")
                {
                    frmResident f = new frmResident(this, true);
                    f.LoadPurok();
                    cn.Open();
                    cm = new SqlCommand("select pic as picture, * from tblArchive where id like '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        long len = dr.GetBytes(0, 0, null, 0, 0);
                        byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                        dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));

                        f._id = dr["id"].ToString();
                        f.txtID.Text = dr["nid"].ToString();
                        f.txtLname.Text = dr["lname"].ToString();
                        f.txtFname.Text = dr["fname"].ToString();
                        f.txtMI.Text = dr["mname"].ToString();
                        f.txtAddress.Text = dr["address"].ToString();
                        f.txtAge.Text = dr["age"].ToString();
                        f.txtContact.Text = dr["contact"].ToString();
                        f.txtEducational.Text = dr["educational"].ToString();
                        f.txtEmail.Text = dr["email"].ToString();
                        f.txtHead.Text = dr["head"].ToString();
                        f.txtHouse.Text = dr["house"].ToString();
                        f.txtOccupation.Text = dr["occupation"].ToString();
                        f.txtPlace.Text = dr["bplace"].ToString();
                        f.txtReligion.Text = dr["religion"].ToString();
                        f.cboCategory.Text = dr["category"].ToString();
                        f.cboCivil.Text = dr["civilstatus"].ToString();
                        f.cboDisability.Text = dr["disability"].ToString();
                        f.cboGender.Text = dr["gender"].ToString();
                        f.cboPurok.Text = dr["purok"].ToString();
                        f.cboStatus.Text = dr["status"].ToString();
                        f.cboVoters.Text = dr["voters"].ToString();
                        f.dtBdate.Value = DateTime.Parse(dr["bdate"].ToString());
                        MemoryStream ms = new MemoryStream(array);
                        Bitmap bitmap = new Bitmap(ms);
                        f.picImage.BackgroundImage = bitmap;
                        f.btnSave.Enabled = false;
                    }
                    dr.Close();
                    cn.Close();
                    f.ShowDialog();
                }
                else if (colname == "btnDeleteA")
                {
                    if (MessageBox.Show("Do you want to delte this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();

                        cm = new SqlCommand("INSERT INTO tblResident (nid, lname, fname, mname, alias, bdate, bplace, age, civilstatus, gender, religion, email, contact, voters, precinct, purok, educational, occupation, address, category, house, head, disability, status, pic) " +
                            "SELECT nid, lname, fname, mname, alias, bdate, bplace, age, civilstatus, gender, religion, email, contact, voters, precinct, purok, educational, occupation, address, category, house, head, disability, status, pic FROM tblArchive " +
                            $"WHERE Id = '{dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()}'", cn);
                        cm.ExecuteNonQuery();

                        cm = new SqlCommand("delete from tblArchive where id = '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been successfully deleted!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadArchive();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            Loadrecords();
        }
    }
}
