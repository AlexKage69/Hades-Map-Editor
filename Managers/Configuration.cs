using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    public static class Configuration
    {
        //public static double OVERALL_SCALE = 1.0;//0.125; 1 doesn't work because too high
        public static string DATA_PATH = AppDomain.CurrentDomain.BaseDirectory + @"..\..\test_data\";
        public static string RAW_PATH = DATA_PATH + @"atlas\";
        public static string APPDATA()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        //public static string COMPILED_PATH = DATA_PATH + @"compiled\";
        //public static string EXPORT_PATH = DATA_PATH + @"export\";
        //public static string APPDATA_PATH = DATA_PATH + @"AppData\";
    }

}
