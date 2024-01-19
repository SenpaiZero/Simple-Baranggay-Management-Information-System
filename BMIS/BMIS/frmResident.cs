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
using System.Text.RegularExpressions;

namespace BMIS
{
    public partial class frmResident : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        frmResidentList f;
        public string _id;
        Boolean isArchieve;
        public frmResident(frmResidentList f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
            isArchieve = false;
            this.f = f;
            LoadPurok();
        }
        public frmResident(frmResidentList f, bool isArchive)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
            this.f = f;
            this.isArchieve = isArchive;
            LoadPurok();
        }
        public void LoadPurok()
        {
            String[] arr = new string[] { "Pag-asa I", "Pag-asa II", "Maligaya", "Pinagpala", "Embargo", "Halina", "Mariposa"};
            try
            {
                cboPurok.Items.Clear();

                for (int i = 0; i < arr.Length; i++)
                {
                    cboPurok.Items.Add(arr[i]);
                }

                cn.Open();
                cm = new SqlCommand("select * from tblpurok", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboPurok.Items.Add(dr["purok"].ToString());
                }
                dr.Close();
                cn.Close();
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {
            lblName.Text = txtFname.Text + " " + txtMI.Text + " " + txtLname.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Image Files (*.png)|*.png|(*.jpeg)|*.jpeg";
               
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    picImage.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        Boolean checkIsEmpty()
        {
            String title = "input error";
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("The Residential ID is empty", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtLname.Text))
            {
                MessageBox.Show("The Last Name is empty", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtFname.Text))
            {
                MessageBox.Show("The First Name is empty", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtPlace.Text))
            {
                MessageBox.Show("The Birthplace is empty", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtContact.Text))
            {
                MessageBox.Show("The Contact is empty", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("The Address is empty", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        Boolean validation()
        {
            String title = "input error";
            if (!Regex.IsMatch(txtLname.Text, "^[a-zA-Z]+(\\s[a-zA-Z]+)*$"))
            {
                MessageBox.Show("Last Name only allows letters", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Regex.IsMatch(txtFname.Text, "^[a-zA-Z]+(\\s[a-zA-Z]+)*$"))
            {
                MessageBox.Show("First Name only allows letters", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(!Regex.IsMatch(txtMI.Text, "^[a-zA-Z]+(\\s[a-zA-Z]+)*$"))
            {
                MessageBox.Show("Middle Name only allows letters", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(!Regex.IsMatch(txtPlace.Text, @"^(?!.*[,.-]{2})(?!.*\s{2})[a-zA-Z0-9,. -]+$"))
            {
                MessageBox.Show("Invalid location on birthplace", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Regex.IsMatch(txtAge.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Age only allows numbers", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Regex.IsMatch(txtReligion.Text, "^[a-zA-Z]+(\\s[a-zA-Z]+)*$"))
            {
                MessageBox.Show("Religion only allows letters", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                MessageBox.Show("Invalid email", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Regex.IsMatch(txtContact.Text, @"^09\d{9}$"))
            {
                MessageBox.Show("Contact only allows numbers", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Regex.IsMatch(txtOccupation.Text, "^[a-zA-Z]+(\\s[a-zA-Z]+)*$"))
            {
                MessageBox.Show("Occupation only allows letters", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Regex.IsMatch(txtAddress.Text, @"^(?!.*[,.-]{2})(?!.*\s{2})[a-zA-Z0-9,. -]+$"))
            {
                MessageBox.Show("Invalid location on address", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Regex.IsMatch(txtHouse.Text, "^[0-9]+$"))
            {
                MessageBox.Show("House Number only allows numbers", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!validation())
                return;
            if (!checkIsEmpty())
                return;
                try
            {
                

                if (MessageBox.Show("Do you want to save this record?",var._title,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    MemoryStream ms = new MemoryStream();
                    picImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = ms.GetBuffer();
                    cn.Open();
                    if(isArchieve == false)
                        cm = new SqlCommand("insert into tblResident (nid, lname, fname, mname, bdate, bplace, age, civilstatus, gender, religion, email,contact, voters, purok, educational, occupation, address, category, house, head, disability, status, pic,employment, education, citizenship, [4pMember], outOfSchoolYouth, relationHead, income, schoolYouth)values(@nid, @lname, @fname, @mname, @bdate, @bplace, @age, @civilstatus, @gender, @religion, @email,@contact, @voters, @purok, @educational, @occupation, @address, @category, @house, @head, @disability, @status,@pic, @employ, @educ, @citizenship, @4pMember, @schoolYouth, @relationHead, @income, @schoolYouth2)", cn);
                    else

                        cm = new SqlCommand("insert into tblArchive (nid, lname, fname, mname, bdate, bplace, age, civilstatus, gender, religion, email,contact, voters, purok, educational, occupation, address, category, house, head, disability, status, pic,employment, education, citizenship, [4pMember], outOfSchoolYouth, relationHead, income, schoolYouth)values(@nid, @lname, @fname, @mname, @bdate, @bplace, @age, @civilstatus, @gender, @religion, @email,@contact, @voters, @purok, @educational, @occupation, @address, @category, @house, @head, @disability, @status,@pic, @employ, @educ, @citizenship, @4pMember, @schoolYouth, @relationHead, @income, @schoolYouth2)", cn);
                    cm.Parameters.AddWithValue("@nid", txtID.Text);
                    cm.Parameters.AddWithValue("@lname", txtLname.Text);
                    cm.Parameters.AddWithValue("@fname", txtFname.Text);
                    cm.Parameters.AddWithValue("@mname", txtMI.Text);
                    cm.Parameters.AddWithValue("@bdate", dtBdate.Value);
                    cm.Parameters.AddWithValue("@bplace", txtPlace.Text);
                    cm.Parameters.AddWithValue("@age", txtAge.Text);
                    cm.Parameters.AddWithValue("@civilstatus", cboCivil.Text);
                    cm.Parameters.AddWithValue("@gender", cboGender.Text);
                    cm.Parameters.AddWithValue("@religion", txtReligion.Text);
                    cm.Parameters.AddWithValue("@email", txtEmail.Text);
                    cm.Parameters.AddWithValue("@contact", txtContact.Text);
                    cm.Parameters.AddWithValue("@voters", cboVoters.Text);
                    cm.Parameters.AddWithValue("@purok", cboPurok.Text);
                    cm.Parameters.AddWithValue("@educational", txtEducational.Text);
                    cm.Parameters.AddWithValue("@occupation", txtOccupation.Text.ToUpper());
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@category", cboCategory.Text);
                    cm.Parameters.AddWithValue("@house", txtHouse.Text);
                    cm.Parameters.AddWithValue("@head", txtHead.Text);
                    cm.Parameters.AddWithValue("@disability", cboDisability.Text);
                    cm.Parameters.AddWithValue("@status", cboStatus.Text);
                    cm.Parameters.AddWithValue("@employ", cboEmployment.Text);
                    cm.Parameters.AddWithValue("@educ", txtEducational.Text);
                    cm.Parameters.AddWithValue("@citizenship", cboCitizenship.Text);
                    cm.Parameters.AddWithValue("4pMember", cbo4p.Text);
                    cm.Parameters.AddWithValue("@pic", arrImage);
                    cm.Parameters.AddWithValue("@schoolYouth", cboSchoolYouth.Text);
                    cm.Parameters.AddWithValue("@relationHead", cboRelationHead.Text);
                    cm.Parameters.AddWithValue("@schoolYouth2", cboInSchoolYouth.Text);
                    cm.Parameters.AddWithValue("@income", cboIncome.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.Loadrecords();
                    f.LoadHead();
                    f.LoadArchive();
                }

            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.Text == "HEAD OF THE FAMILY")
            {
                txtHouse.Enabled = true;
                btnBrowse.Visible = false;
                cboRelationHead.Enabled = false;
            }
            else
            {
                cboRelationHead.Enabled = true;
                cboRelationHead.Text = string.Empty;
                txtHouse.Enabled = false;
                btnBrowse.Visible = true;
            }
        }

        private void cboVoters_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (MessageBox.Show("Do you want to update this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    MemoryStream ms = new MemoryStream();
                    picImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = ms.GetBuffer();
                    cn.Open();
                    cm = new SqlCommand("update tblresident set nid=@nid, lname=@lname, fname=@fname, mname=@mname, bdate=@bdate, bplace=@bplace, age=@age, civilstatus=@civilstatus, gender=@gender, religion=@religion, email=@email,contact=@contact, voters=@voters, purok=@purok, educational=@educational, occupation=@occupation, address=@address, category=@category, house=@house, head=@head, disability=@disability, status=@status, pic=@pic, education=@educ, employment=@employ, citizenship=@citizenship, [4pMember]=@4pMember, outOfSchoolYouth=@schoolYouth, relationHead=@relationHead, income=@income, schoolYouth=@schoolYouth2 where id =@id", cn);
                    cm.Parameters.AddWithValue("@nid", txtID.Text);
                    cm.Parameters.AddWithValue("@lname", txtLname.Text);
                    cm.Parameters.AddWithValue("@fname", txtFname.Text);
                    cm.Parameters.AddWithValue("@mname", txtMI.Text);
                    cm.Parameters.AddWithValue("@bdate", dtBdate.Value);
                    cm.Parameters.AddWithValue("@bplace", txtPlace.Text);
                    cm.Parameters.AddWithValue("@age", txtAge.Text);
                    cm.Parameters.AddWithValue("@civilstatus", cboCivil.Text);
                    cm.Parameters.AddWithValue("@gender", cboGender.Text);
                    cm.Parameters.AddWithValue("@religion", txtReligion.Text);
                    cm.Parameters.AddWithValue("@email", txtEmail.Text);
                    cm.Parameters.AddWithValue("@contact", txtContact.Text);
                    cm.Parameters.AddWithValue("@voters", cboVoters.Text);
                    cm.Parameters.AddWithValue("@purok", cboPurok.Text);
                    cm.Parameters.AddWithValue("@educational", txtEducational.Text);
                    cm.Parameters.AddWithValue("@occupation", txtOccupation.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@category", cboCategory.Text);
                    cm.Parameters.AddWithValue("@house", txtHouse.Text);
                    cm.Parameters.AddWithValue("@head", txtHead.Text);
                    cm.Parameters.AddWithValue("@disability", cboDisability.Text);
                    cm.Parameters.AddWithValue("@status", cboStatus.Text);
                    cm.Parameters.AddWithValue("@employ", cboEmployment.Text);
                    cm.Parameters.AddWithValue("@educ", txtEducational.Text);
                    cm.Parameters.AddWithValue("@citizenship", cboCitizenship.Text);
                    cm.Parameters.AddWithValue("@4pMember", cbo4p.Text);
                    cm.Parameters.AddWithValue("@schoolYouth", cboSchoolYouth.Text);
                    if(cboCategory.Text == "MEMBER")
                        cm.Parameters.AddWithValue("@relationHead", cboRelationHead.Text);
                    else
                        cm.Parameters.AddWithValue("@relationHead", "");
                    cm.Parameters.AddWithValue("@income", cboIncome.Text);
                    if(cboInSchoolYouth.Text == "YES")
                        cm.Parameters.AddWithValue("@schoolYouth2", cboInSchoolYouth.Text);
                    else
                        cm.Parameters.AddWithValue("@schoolYouth2", "");
                    cm.Parameters.AddWithValue("@pic", arrImage);
                    cm.Parameters.AddWithValue("@id", _id);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully updated!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.Loadrecords();
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Clear()
        {
            picImage.BackgroundImage = Image.FromFile(Application.StartupPath + @"\man1.png");
            txtAddress.Clear();
            txtAge.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            txtFname.Clear();
            txtHead.Clear();
            txtHouse.Clear();
            txtID.Clear();
            txtLname.Clear();
            txtMI.Clear();
            txtOccupation.Clear();
            txtPlace.Clear();
            txtReligion.Clear();
            cboCategory.Text = "";
            cboCivil.Text = "";
            cboDisability.Text = "";
            cboGender.Text = "";
            cboPurok.Text = "";
            cboStatus.Text = "";
            cboVoters.Text = "";
            dtBdate.Value = DateTime.Now;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtID.Focus();
        }

        private void dtBdate_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dtBdate.Value.Year;
            txtAge.Text = age.ToString();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            frmHouseHold f = new frmHouseHold(this);
            f.LoadRecords();
            f.ShowDialog();
        }

        private void txtHead_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHouse_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void picImage_Click(object sender, EventArgs e)
        {

        }

        private void generageRidBtn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            long randID = rand.Next(1000000, 10000000);
            txtID.Text = "1000" + randID.ToString();

        }

        private void frmResident_Load(object sender, EventArgs e)
        {
            cboCivil.SelectedIndex = 0;
            cboGender.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            cboDisability.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;
            cboPurok.SelectedIndex = 0;
            txtEducational.SelectedIndex = 0;
            cboVoters.SelectedIndex = 0;
            cboEmployment.SelectedIndex = 0;
            cbo4p.SelectedIndex = 0;
            cboSchoolYouth.SelectedIndex = 0;
            cboInSchoolYouth.SelectedIndex = 0;
            cboIncome.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboSchoolYouth.Text != "NO")
            {
                cboInSchoolYouth.Enabled = false;
                cboInSchoolYouth.Text = string.Empty;
            }
            else
            {
                cboInSchoolYouth.Enabled = true;
                cboInSchoolYouth.SelectedIndex = 0;
            }
        }
    }
}
