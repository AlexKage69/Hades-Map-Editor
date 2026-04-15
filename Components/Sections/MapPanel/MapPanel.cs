using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using Hades_Map_Editor.MapSection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Sections
{
    public class MapPanel : Panel, IComponent, Focusable
    {
        private MapCanvas canvas;
        private ProjectData data;
        private MenuStrip topMenu;
        private ToolStripMenuItem zoomIn, zoomOut;
        public MapPanel(ProjectData data)
        {
            this.data = data;
            Initialize();
            Populate();
        }


        public void Initialize()
        {
            BackColor = System.Drawing.Color.BlueViolet;
            AutoScroll = false;
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.FixedSingle;
            SetAutoScrollMargin(0, 50);

            zoomOut = new ToolStripMenuItem();
            zoomOut.Text = "+";
            zoomOut.Click += (s, e) => MapPanel_ZoomOut_Click(s, e);

            zoomIn = new ToolStripMenuItem();
            zoomIn.Text = "-";
            zoomIn.Click += (s, e) => MapPanel_ZoomIn_Click(s, e);

            topMenu = new MenuStrip();
            topMenu.Dock = DockStyle.Top;
            topMenu.Items.Add(zoomIn);
            topMenu.Items.Add(zoomOut);
            Controls.Add(topMenu);

            canvas = new MapCanvas();
            Controls.Add(canvas);

            /*canvas.MouseClick += new MouseEventHandler((o, e) => {
                MouseEventArgs me = (MouseEventArgs)e;
                System.Drawing.Point coordinates = me.Location;
                var list = mapManager.GetElementsAt(coordinates);
                if (list.Count > 0)
                {
                    //app.Select(list.First().info.Id, true);
                }
                else
                {
                    //app.Unload();
                }
            });*/
            //RefreshMap();
            /*canvasSelector = new PictureBox();
            canvasSelector.BorderStyle = BorderStyle.FixedSingle;
            canvasSelector.ClientSize = new Size(20, 20);
            canvasSelector.Visible = true;
            Controls.Add(canvasSelector);*/
            /*foreach (int id in elementManager.GetAllIds())
            {
                var info = elementManager.GetElement(id);               
                Graphics g = e.Graphics;

                var controls = this.ParentPictureBox.Controls.OfType<PictureBox>();
                foreach (var c in controls)
                {
                    g.DrawImage(c.Image, c.Location.X, c.Location.Y, c.Width, c.Height);
                }
            }*/
        }

        public void Populate()
        {
            RefreshData();
        }
        public void UnFocus()
        {
        }
        public void FocusOn(int id)
        {
            Obstacles obs = data.mapData.GetFromId(id);
            //Size size = canvas;
            //Add rectangle

            // Adjust scrollbar
            Size offset = canvas.GetOffset(obs.GetLocation());
            //Size mapOffset = canvas.GetMapOffset();

            if (obs.HasAsset())
            {
                canvas.VerticalScroll.Value = Math.Max(Math.Min(offset.Width, canvas.VerticalScroll.Maximum), 0);
                canvas.HorizontalScroll.Value = Math.Max(Math.Min(offset.Height, canvas.HorizontalScroll.Maximum), 0);
                canvas.SetSelect(obs);
                //PerformLayout();
            }
            else
            {
                canvas.UnsetSelect();
            }
            canvas.Refresh();
        }

        public void GetData()
        {
                AssetsManager assetsManager = AssetsManager.GetInstance();
                foreach (Obstacles obs in data.mapData.Obstacles)
                {
                    Asset asset;
                    if (assetsManager.GetAsset(obs.Name, out asset))
                    {
                        canvas.AddItem(obs);
                    }
                }
        }

        public void RefreshData()
        {
            GetData();
            canvas.MapRefresh();
        }
        // Actions
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
