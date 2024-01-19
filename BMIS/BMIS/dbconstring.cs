using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMIS
{
    class dbconstring
    {
        //public static string connection = File.ReadAllText(System.Environment.CurrentDirectory + @"\config.jer");
        public static string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bmisDB.mdf;Integrated Security=True";
    }
}
