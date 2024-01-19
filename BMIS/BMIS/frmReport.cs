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
using Microsoft.Reporting.WinForms;

namespace BMIS
{
    public partial class frmReport : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        public frmReport()
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
        }

        private void frmReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        public void PreviewBlotter(string sql)
        {
            try
            {
                ReportDataSource rptDs;
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\rptBlotter.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                using (cn)
                {
                    cn.Open();
                    da.SelectCommand = new SqlCommand(sql, cn);
                    da.Fill(ds.Tables["dtBlotter"]);
                }

                rptDs = new ReportDataSource("DataSet1", ds.Tables["dtBlotter"]);
                reportViewer1.LocalReport.DataSources.Add(rptDs);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 150;
                System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();
                pageSettings.Landscape = true;
                reportViewer1.SetPageSettings(pageSettings);
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                // Log exception details for debugging
                Console.WriteLine("Exception: " + ex.Message);
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
