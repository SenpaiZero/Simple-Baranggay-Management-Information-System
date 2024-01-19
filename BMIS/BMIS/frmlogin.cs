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

    public partial class frmlogin : Form
    {

        public frmlogin()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
                String username, user_password;

                username = txt_username.Text;
                user_password = txt_password.Text;

                try
                {
                    String querry = "SELECT * FROM login_new WHERE username ='" + txt_username.Text + "'AND password = '" + txt_password.Text + "' "; 

                    using (SqlConnection con = new SqlConnection(dbconstring.connection))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(querry, con))
                        {
                            username = txt_username.Text;
                            user_password = txt_password.Text;

                            SqlDataReader dr = cmd.ExecuteReader();
                            if(dr.Read())
                            {
                                globalVariable.username = username;
                                globalVariable.position = comboBox1.Text.ToString();
                                Form1 f = new Form1();
                                f.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txt_username.Clear();
                                txt_password.Clear();


                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
            
        }

        private void frmlogin_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
            this.Dispose();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
