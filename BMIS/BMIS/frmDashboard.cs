using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMIS
{
    public partial class frmDashboard : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public frmDashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string CountRecords(string sql)
        {
            cn.Open();
            cm = new SqlCommand(sql, cn);
            string _count = cm.ExecuteScalar().ToString();
            cn.Close();
            return _count;
        }
        public void Loadrecords()
        {
            lblCount.Text = CountRecords("select count(*) from tblresident");
            lblHousehold.Text = CountRecords("select count(*) from tblresident where category like 'HEAD OF THE FAMILY'");
            lblMember.Text = CountRecords("select count(*) from tblresident where category like 'MEMBER'");
            lblVoters.Text = CountRecords("select count(*) from tblresident where voters like 'YES'");
            lblFemale.Text = CountRecords("select count(*) from tblresident where gender like 'FEMALE'");
            lblMale.Text = CountRecords("select count(*) from tblresident where gender like 'MALE'");
            lblVaccination.Text = CountRecords("select count(*) from tblArchive");

            //2nd row 
            lblBlotter.Text = CountRecords("SELECT count(*) FROM tblBlotter");
            lblTotalCase.Text = CountRecords("SELECT count(*) FROM tblBlotterSettled");

            //3rd row
            lblTotalUnemployed.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE employment = 'UNEMPLOYED'");
            lblTotalEmployed.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE employment = 'EMPLOYED'");
            lblTotalGovernment.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE employment = 'GOVERNMENT'");
            lblTotalSelfEmployed.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE employment = 'SELF-EMPLOYED'");
            lblTotalVoters.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE voters = 'YES'");
            lblTotalSenior.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE age > 59");
            lblTotalPwd.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE disability = 'YES'");

            //4th row
            lblTotalElementary.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE schoolYouth = 'ELEMENTARY'");
            lblTotalJuniorHigh.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE schoolYouth = 'JUNIOR HIGH SCHOOL'");
            lblTotalSeniorHigh.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE schoolYouth = 'SENIOR HIGH SCHOOL'");
            lblTotalCollege.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE schoolYouth = 'COLLEGE'");
            lblTotalWidow.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE civilstatus = 'WIDOW'");
            lblTotalSeprated.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE civilstatus = 'SEPARATED'");
            lblOutSchool.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE education = 'OUT OF SCHOOL YOUTH'");

            //5th row
            lblTotalSingle.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE civilstatus = 'SINGLE'");
            lblTotal4Ps.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE [4pMember] = 'YES'");
            lblTotalMarried.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE civilstatus = 'MARRIED'");
            lblSoloParent.Text = CountRecords("SELECT COUNT(*) FROM tblResident WHERE civilstatus = 'SOLO PARENT'");

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
