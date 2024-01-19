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
    public partial class frmPrintBusiness : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        frmDocument f;
        public frmPrintBusiness(frmDocument f)
        {
            InitializeComponent();
            this.f = f;
            cn = new SqlConnection(dbconstring.connection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmPrintBusiness_Load(object sender, EventArgs e)
        {

        }
    }
}
