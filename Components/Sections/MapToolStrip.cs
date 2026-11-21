using Hades_Map_Editor.Data;
using Hades_Map_Editor.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Hades_Map_Editor
{
    public class MapToolStrip : ToolStrip, IComponent
    {
        //protected ProjectData data;
        private MapCanvas canvas;
        private ToolStripMenuItem zoomIn, zoomOut;

        public MapToolStrip(MapCanvas canvas)
        {
            this.canvas = canvas;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            // Add items to the ToolStrip.
            Items.Add("Create New Obstacle");
            Items.Add("Flip");
            Items.Add("Resize");

            // Add items to the ToolStrip.
            zoomOut = new ToolStripMenuItem();
            zoomOut.Text = "+";
            zoomOut.Click += (s, e) => MapPanel_ZoomOut_Click(s, e);

            zoomIn = new ToolStripMenuItem();
            zoomIn.Text = "-";
            zoomIn.Click += (s, e) => MapPanel_ZoomIn_Click(s, e);

            Items.Add(zoomIn);
            Items.Add(zoomOut);
            // Add the ToolStrip to the top panel of the ToolStripContainer.
        }

        public void Populate(){ }
        private void MapPanel_ZoomIn_Click(object sender, EventArgs e)
        {
            canvas.ZoomIn();
            zoomIn.Enabled = canvas.CanZoomIn();
            zoomOut.Enabled = canvas.CanZoomOut();
            Console.WriteLine("Zoom In Clicked");
        }
        private void MapPanel_ZoomOut_Click(object sender, EventArgs e)
        {
            canvas.ZoomOut();
            zoomIn.Enabled = canvas.CanZoomIn();
            zoomOut.Enabled = canvas.CanZoomOut();
            Console.WriteLine("Zoom Out Clicked");
        }
    }
}
