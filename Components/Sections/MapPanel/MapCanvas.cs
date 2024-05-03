using Hades_Map_Helper.Data;
using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.MapSection
{
    public class MapCanvas : Panel, IComponent
    {
        List<Obstacle> listOfLoadedAssets;
        static int X = 25, Y = 25;
        double minOffsetX, maxOffsetX, minOffsetY, maxOffsetY;
        Obstacle selected;
        Image currentImage;
        public MapCanvas()
        {
            listOfLoadedAssets = new List<Obstacle>();
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            Name = "Canvas";
            BackColor = System.Drawing.Color.Red;
            Size = new Size(1,1);
            Paint += new PaintEventHandler(MapCanvas_Paint);
        }
        public void Populate()
        {
            MouseDown += new System.Windows.Forms.MouseEventHandler(MapCanvas_MouseDown);
            MapRefresh();
        }
        public void AddItem(Obstacle obs)
        {
            listOfLoadedAssets.Add(obs);
            if (obs.Location.X != 0)
            {
                if (maxOffsetX == 0)
                {
                    minOffsetX = obs.Location.X;
                    maxOffsetX = obs.Location.X;
                }
                else if(obs.Location.X > maxOffsetX)
                {
                    maxOffsetX = obs.Location.X;
                }
                else if(obs.Location.X < minOffsetX)
                {
                    minOffsetX = obs.Location.X;
                }
            }
            if (obs.Location.Y != 0)
            {
                if (maxOffsetY == 0)
                {
                    minOffsetY = obs.Location.Y;
                    maxOffsetY = obs.Location.Y;
                }
                else if (obs.Location.Y > maxOffsetY)
                {
                    maxOffsetY = obs.Location.Y;
                }
                else if (obs.Location.Y < minOffsetY)
                {
                    minOffsetY = obs.Location.Y;
                }
            }
        }
        public void SelectItem(Obstacle obstacle)
        {
            selected = obstacle;
        }
        public void MapRefresh()
        {
            Size = new Size((int)(maxOffsetX - minOffsetX + 2*X), (int)(maxOffsetY - minOffsetY + 2 * Y));
            Image finalImage = new Bitmap(Size.Width, Size.Height);
            Graphics finalGraphic = Graphics.FromImage(finalImage);
            int temp = 0;
            listOfLoadedAssets = listOfLoadedAssets.OrderByDescending((Obstacle val) => { return val.GetLayerLevel(); } ).ToList();
            foreach (Obstacle obs in listOfLoadedAssets)
            {
                Asset asset;
                if (!obs.Active || !obs.GetAsset(out asset))
                {
                    continue;
                }
                using (System.Drawing.Image image = asset.GetImage(obs.Scale))
                {
                    Utility.FlipImage(image, obs.FlipHorizontal,obs.FlipVertical);
                    finalGraphic.DrawImage(image, new PointF((float)(obs.Location.X - minOffsetX + X), (float)(obs.Location.Y - minOffsetY + Y)));
                    temp++;
                }
            }
            currentImage = finalImage;
        }
        public Size GetOffset()
        {
            return new Size((int) minOffsetX + X,(int) minOffsetY + Y);
        }
        private void MapCanvas_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;
            //AssetsManager assetsManager = AssetsManager.GetInstance();
            g.DrawImage(currentImage, new PointF(0, 0));
        }
        private void MapCanvas_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            System.Drawing.Point click = new System.Drawing.Point(e.X, e.Y);
            foreach (var obstacle in listOfLoadedAssets)
            {
                Asset asset;
                if(obstacle.GetAsset(out asset))
                {
                    //asset.rect.x
                }
            }
            Console.WriteLine(click);
            //AssetsManager assetsManager = AssetsManager.GetInstance();
        }
    }
}
