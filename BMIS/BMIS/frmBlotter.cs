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
    public partial class frmBlotter : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        frmIssue f;
        public string _id;
        public frmBlotter(frmIssue f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
            this.f = f;
        }
        public string GetFileNO() 
        {
            string fileno = "CASE-";
            Random rnd = new Random();
            for (int x = 0; x < 6; x++)
            {
                fileno += rnd.Next(1, 9).ToString();
            }
            try
            {
                cn.Open();  
                cm = new SqlCommand("Select top 1 fileno from tblBlotter where fileno like'" + fileno + "%' order by id desc", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblFile.Text = GetFileNO();
                }
                dr.Close();
                cn.Close();

            }
            catch (Exception ex)
            { 
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return fileno;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public static bool isSettled = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                if(MessageBox.Show("Do you want to save this blotter?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    cn.Open();
                    if(isSettled)
                        cm = new SqlCommand("insert into tblblotterSettled (fileno, baranggay, purok, incident, place, idate, itime, complainant, witness1, witness2, narrative)values(@fileno, @barangay, @purok, @incident, @place, @idate, @itime, @complainant, @witness1, @witness2, @narrative)", cn);
                    else
                        cm = new SqlCommand("insert into tblblotter (fileno, baranggay, purok, incident, place, idate, itime, complainant, witness1, witness2, narrative)values(@fileno, @barangay, @purok, @incident, @place, @idate, @itime, @complainant, @witness1, @witness2, @narrative)",cn);
                    cm.Parameters.AddWithValue("@fileno", lblFile.Text);
                    cm.Parameters.AddWithValue("@barangay", txtBrgy.Text);
                    cm.Parameters.AddWithValue("@purok", txtPurok.Text);
                    cm.Parameters.AddWithValue("@incident", txtIncident.Text);
                    cm.Parameters.AddWithValue("@place", txtPlace.Text);
                    cm.Parameters.AddWithValue("@idate", DateTime.Parse(dtDate.Value.ToLongDateString()));
                    cm.Parameters.AddWithValue("@itime", txtTime.Text);
                    cm.Parameters.AddWithValue("@complainant", txtComplainant.Text);
                    cm.Parameters.AddWithValue("@witness1", txtWitness1.Text);
                    cm.Parameters.AddWithValue("@witness2", txtWitness2.Text);
                    cm.Parameters.AddWithValue("@narrative", txtNarrative.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadRecords();
                    lblFile.Text = GetFileNO();
                    Clear();
                    
                }
            
            }catch(Exception ex) 
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        public void Clear() 
        {
            txtBrgy.Clear();
            txtPurok.Clear();
            txtIncident.Clear();
            txtPlace.Clear();
            txtTime.Clear();
            txtComplainant.Clear();
            txtWitness1.Clear();
            txtWitness2.Clear();
            txtNarrative.Clear();
            dtDate.Value = DateTime.Now;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtBrgy.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void frmBlotter_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update this blotter?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    if(isSettled)
                        cm = new SqlCommand("update tblBlotterSettled set baranggay=@barangay, purok=@purok, incident=@incident, place=@place, idate=@idate, itime=@itime, complainant=@complainant, witness1=@witness1, witness2=@witness2, narrative=@narrative where id = @id", cn);
                    else
                        cm = new SqlCommand("update tblBlotter set baranggay=@barangay, purok=@purok, incident=@incident, place=@place, idate=@idate, itime=@itime, complainant=@complainant, witness1=@witness1, witness2=@witness2, narrative=@narrative where id = @id", cn);
                    cm.Parameters.AddWithValue("@barangay", txtBrgy.Text);
                    cm.Parameters.AddWithValue("@purok", txtPurok.Text);
                    cm.Parameters.AddWithValue("@incident", txtIncident.Text);
                    cm.Parameters.AddWithValue("@place", txtPlace.Text);
                    cm.Parameters.AddWithValue("@idate", DateTime.Parse(dtDate.Value.ToLongDateString()));
                    cm.Parameters.AddWithValue("@itime", txtTime.Text);
                    cm.Parameters.AddWithValue("@complainant", txtComplainant.Text);
                    cm.Parameters.AddWithValue("@witness1", txtWitness1.Text);
                    cm.Parameters.AddWithValue("@witness2", txtWitness2.Text);
                    cm.Parameters.AddWithValue("@narrative", txtNarrative.Text);
                    cm.Parameters.AddWithValue("@id", _id);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully updated!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadRecords();
                    this.Dispose();

                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
