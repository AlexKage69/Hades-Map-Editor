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
        public MapPanel(ProjectData data)
        {
            this.data = data;
            Initialize();
            Populate();
        }


        public void Initialize()
        {
            BackColor = System.Drawing.Color.BlueViolet;
            AutoScroll = true;
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.FixedSingle;
            SetAutoScrollMargin(0, 50);

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
            AssetsManager assetsManager = AssetsManager.GetInstance();
            foreach (Obstacle obs in data.mapData.Obstacles)
            {
                Asset asset;
                if(assetsManager.GetAsset(obs.Name,out asset)){
                    canvas.AddItem(obs);
                }
            }
            canvas.MapRefresh();
        }
        public void UnFocus()
        {
        }
        public void FocusOn(Obstacle obs)
        {
            //Size size = canvas;
            //Add rectangle

            // Adjust scrollbar
            Size offset = canvas.GetOffset();
            VerticalScroll.Value = Math.Max(Math.Min((int)obs.Location.X - offset.Width,VerticalScroll.Maximum),0);
            HorizontalScroll.Value = Math.Max(Math.Min((int)obs.Location.Y - offset.Height, HorizontalScroll.Maximum), 0);
            PerformLayout();
        }
    }
}
