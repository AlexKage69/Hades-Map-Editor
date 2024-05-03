using Hades_Map_Helper.Components;
using Hades_Map_Helper.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    public partial class HadesMapHelperForm : Form
    {
        HadesMapHelper hmh;
        public HadesMapHelperForm()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            hmh = new HadesMapHelper(this);
        }

        // Resize?
        public Size oldSize;
        private void HadesMapHelperForm_Load(object sender, EventArgs e) => oldSize = base.Size;

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            foreach (Control cnt in this.Controls)
                ResizeAll(cnt, base.Size);

            oldSize = base.Size;
        }
        private void ResizeAll(Control control, Size newSize)
        {
            int width = newSize.Width - oldSize.Width;
            control.Left += (control.Left * width) / oldSize.Width;
            control.Width += (control.Width * width) / oldSize.Width;

            int height = newSize.Height - oldSize.Height;
            control.Top += (control.Top * height) / oldSize.Height;
            control.Height += (control.Height * height) / oldSize.Height;
        }
        private void HadesMapHelperForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Console.WriteLine("Paint");
        }        
    }
}
