using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMIS
{
    public partial class frmDocument : Form
    {
        public frmDocument()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openDoc("indigency-english");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            openDoc("indigency-tagalog");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openDoc("barangay-clearance");
        }

        void openDoc(String doc)
        {
            string filePath = $@"Docs\{doc}.docx";

            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    Process.Start("WINWORD.EXE", filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening Word file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File not found: " + filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openDoc("residency");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            openDoc("RESIDENCY-CHILD");
        }

        private void button7_Click(object sender, EventArgs e)
        {

            openDoc("senior-citizen-indigency");
        }

        private void button8_Click(object sender, EventArgs e)
        {

            openDoc("solo-parent");
        }
    }
}
