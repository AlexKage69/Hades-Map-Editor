using Hades_Map_Editor.Data.Project;
using Hades_Map_Editor.Data.Map;
using Hades_Map_Editor.Data.Asset;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Hades_Map_Editor.MapSection
{
    public class MapPanel : Panel, IComponent, Focusable
    {
        private MapCanvas canvas;
        private MapToolStrip mts;
        private ProjectPage parent;
        public MapPanel(ProjectPage projectPage)
        {
            parent = projectPage;
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

            canvas = new MapCanvas(this);
            Controls.Add(canvas);

            mts = new MapToolStrip(canvas);
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
            foreach (Obstacle obs in GetData().mapData.GetAllObstacles())
            {
                if (obs.HasAsset() && obs.Active)
                {
                    canvas.AddItem(obs);
                }
            }
            canvas.MapRefresh();
        }
        public void UnFocus()
        {
        }
        public void FocusOn(int id)
        {
            
            Obstacle obs = parent.GetData().mapData.GetFromId(id);
            //Size size = canvas;
            //Add rectangle

            // Adjust scrollbar
            Size offset = canvas.GetOffset(obs.GetLocation());
            //Size mapOffset = canvas.GetMapOffset();

            if (obs.HasAsset())
            {
                obs.GetAsset(out AssetData asset);
                var currentScale = canvas.GetCurrentScale();
                // If you want to focus on Map, Uncomment below.
                canvas.VerticalScroll.Value = (int)Math.Max(Math.Min((offset.Height * currentScale) - (Height/2) +(asset.GetRect().Height/ 2 * currentScale), canvas.VerticalScroll.Maximum), 0);
                canvas.HorizontalScroll.Value = (int)Math.Max(Math.Min((offset.Width* currentScale) - (Width/2) + (asset.GetRect().Width / 2 * currentScale), canvas.HorizontalScroll.Maximum), 0);
                canvas.SetSelect(obs);
                //PerformLayout();
            }
            else
            {
                canvas.UnsetSelect();
            }
            canvas.Refresh();
        }

        public ProjectPage GetProjectPage()
        {
            return parent;
        }
        public ProjectData GetData()
        {
            return GetProjectPage().GetData();
        }
        public MapToolStrip GetMapToolStrip() { return mts; }
        public MapCanvas GetCanvas() { return canvas; }
        // Actions
    }
}
