using Hades_Map_Helper.Managers;
using Hades_Map_Helper.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    class MapPanel
    {
        private PanelManager panelManager;
        private MapManager mapManager;
        private AssetManager assetManager;
        private System.Windows.Forms.Panel panel;
        private PictureBox canvas;
        private PictureBox canvasSelector;
        private Size mapSize;
        public MapPanel(PanelManager panelManager, AssetManager assetManager, MapManager mapManager)
        {
            this.panelManager = panelManager;
            this.assetManager = assetManager;
            this.mapManager = mapManager;
            mapSize = new Size(1, 1);
            CreatePanel(0.80f);
            LoadElements();
        }
        private void CreatePanel(float sizePercent)
        {
            panel = new System.Windows.Forms.Panel();
            panel.Location = panelManager.GetLocation(PanelNames.MapPanel);
            panel.Size = panelManager.GetSize(PanelNames.MapPanel);
            panel.BackColor = System.Drawing.Color.LightGray;
            panel.Parent = panelManager.GetForm();
            panel.AutoScroll = true;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.SetAutoScrollMargin(0, 50);
        }
        private void LoadElements()
        {
            int assetSize = (panel.Size.Width) / 3;
            canvas = new PictureBox();
            canvas.Name = "Canvas";
            //picBox.Image = assetManager.GetImage(elementManager.GetElement(id).Name);
            canvas.ClientSize = mapSize;
            canvas.BackColor = System.Drawing.Color.Transparent;
            canvas.SizeMode = PictureBoxSizeMode.AutoSize;
            //picBox.Location = new System.Drawing.Point((int)info.Location.X, (int)info.Location.Y);
            canvas.Parent = panel;
            canvas.MouseClick += new MouseEventHandler((o, e) => {
                MouseEventArgs me = (MouseEventArgs)e;
                System.Drawing.Point coordinates = me.Location;
                var list = mapManager.GetElementsAt(coordinates);
                if(list.Count > 0)
                {
                    panelManager.GetForm().Select(list.First().info.Id, true);
                }
                else
                {
                    panelManager.GetForm().Unload();
                }
            });
            RefreshMap();
            canvasSelector = new PictureBox();
            canvasSelector.BorderStyle = BorderStyle.FixedSingle;
            canvasSelector.ClientSize = new Size(20, 20);
            canvasSelector.Visible = false;
            canvasSelector.Parent = canvas;
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
        public void Select(int id, bool sourceMap)
        {
            var element = mapManager.GetElement(id);
            SelectionFocus(element);
            if (!sourceMap)
            {
                MoveScrollbar(element);
            }
        }
        public void Unload()
        {
            canvasSelector.Visible = false;
        }
        private void MoveScrollbar(MapElement element)
        {
            var origin = mapManager.GetMapOriginator();
            var y = Math.Min(element.GetYCenter() - origin.Height, panel.VerticalScroll.Maximum);// - (panel.Height / 2) + (element.area.Height * AssetManager.OVERALL_SCALE) / 2, panel.VerticalScroll.Maximum);
            var x = Math.Min(element.GetXCenter() - origin.Width, panel.HorizontalScroll.Maximum);// - (panel.Width / 2) + (element.area.Width * AssetManager.OVERALL_SCALE) / 2, panel.HorizontalScroll.Maximum);
            panel.VerticalScroll.Value = y;
            panel.HorizontalScroll.Value = x;

            panel.PerformLayout();
        }
        private void SelectionFocus(MapElement element)
        {
            canvasSelector.Visible = true;
            canvasSelector.ClientSize = new Size(element.area.Width, element.area.Height);
            canvasSelector.Location = new System.Drawing.Point(element.area.X, element.area.Y);
        }
        public void RefreshMap()
        {
            canvas.Refresh();
            mapSize = mapManager.GetMapSize();
            var origin = mapManager.GetMapOriginator();
            Bitmap result = new Bitmap(mapSize.Width, mapSize.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                //var temp = 0;
                foreach (int id in mapManager.GetAllIds())
                {
                    var element = mapManager.GetElement(id);
                    if(!element.info.Active){
                        continue;
                    }
                    System.Drawing.Image image = assetManager.GetImage(element.info.Name);
                    if (element.info.FlipHorizontal)
                    {
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }//  * info.Scale
                    double scale = element.info.Scale;
                    //Rectangle area = new Rectangle((int)info.Location.X, (int)info.Location.Y, (int)(image.Width), (int)(image.Height));
                    element.area = new Rectangle((int)((element.info.Location.X- origin.Width) * AssetManager.OVERALL_SCALE), (int)((element.info.Location.Y - origin.Height) * AssetManager.OVERALL_SCALE), (int)(image.Width* scale), (int)(image.Height* scale));
                    g.DrawImage(image, element.area);
                    //Back to normal
                    if (element.info.FlipHorizontal)
                    {
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                    //temp += 100;
                }
            }
            canvas.Image = result;
        }
    }
}
