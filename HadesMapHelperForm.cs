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
        AssetManager assetManager;
        PanelManager panelManager;
        MapManager elementManager;

        AssetPanel assetPanel;
        MapPanel mapPanel;
        ToolbarPanel toolbarPanel;
        ElementPanel elementPanel;
        PropertyPanel propertyPanel;
        public HadesMapHelperForm()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            // Managers
            assetManager = new AssetManager();
            elementManager = new MapManager(assetManager);
            panelManager = new PanelManager(this);

            //
            assetPanel = new AssetPanel(panelManager, assetManager);
            mapPanel = new MapPanel(panelManager, assetManager, elementManager);
            toolbarPanel = new ToolbarPanel(panelManager);
            elementPanel = new ElementPanel(panelManager, elementManager);
            propertyPanel = new PropertyPanel(panelManager, elementManager);
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
        public void Select(int id, bool sourceMap = false)
        {
            Console.WriteLine("Select:"+id);
            elementPanel.Select(id);
            propertyPanel.Select(id);
            mapPanel.Select(id, sourceMap);
        }
        public void Unload()
        {
            elementPanel.Unload();
            propertyPanel.Unload();
            mapPanel.Unload();
        }
    }
}
