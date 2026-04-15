using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Community.CsharpSqlite.Sqlite3;

namespace Hades_Map_Editor.MapSection
{
    public class MapCanvas : Panel, IComponent
    {
        List<Obstacles> listOfLoadedAssets;
        static int X = 200, Y = 200;
        float CurrentScale = 1.0f;
        double minOffsetX, maxOffsetX, minOffsetY, maxOffsetY;
        Obstacles selected;
        Image currentImage;
        Rectangle selectRect;
        PictureBox canvas;
        ContextMenu canvasContextMenu;
        public MapCanvas()
        {
            listOfLoadedAssets = new List<Obstacles>();
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            UnsetSelect();
            //CurrentScale = 1.0f;
            Name = "Canvas";
            BackColor = System.Drawing.Color.Transparent;
            AutoScroll = true;
            Dock = DockStyle.Fill;
            
            //overlayCanvas = new Panel();
            //overlayCanvas.BackColor = System.Drawing.Color.Transparent;
            //Controls.Add(overlayCanvas);

            canvas = new PictureBox();
            canvas.BackColor = System.Drawing.Color.LightGray;
            canvas.Paint += new PaintEventHandler(MapCanvas_Paint);
            Controls.Add(canvas);
            canvasContextMenu = new ContextMenu();
            canvasContextMenu.MenuItems.Add("Item 1");
            canvasContextMenu.MenuItems.Add("Item 2");
        }
        public void Populate()
        {
            //MouseDown += new System.Windows.Forms.MouseEventHandler(MapCanvas_MouseDown);
            canvas.MouseDown += (s, e) => MapCanvas_MouseDown(s, e);
            canvas.ContextMenu = canvasContextMenu;
        }
        public void AddItem(Obstacles obs)
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
        public void SelectItem(Obstacles obstacle)
        {
            selected = obstacle;
        }
        public void MapRefresh()
        {
            canvas.Size = new Size((int)((maxOffsetX - minOffsetX)* CurrentScale + 2 * X), (int)((maxOffsetY - minOffsetY) * CurrentScale + 2 * Y));
            Image finalImage = new Bitmap(canvas.Size.Width, canvas.Size.Height);
            Graphics finalGraphic = Graphics.FromImage(finalImage);
            FormManager formManager = FormManager.GetInstance();
            int temp = 0;
            listOfLoadedAssets = listOfLoadedAssets.OrderByDescending((Obstacles val) => { return val.GetLayerLevel(); }).ToList();
            finalGraphic.ScaleTransform(CurrentScale, CurrentScale);
            foreach (Obstacles obs in listOfLoadedAssets)
            {
                Asset asset;
                formManager.GetBottomMenu().SetStatuts(obs.Name);
                if (!obs.Active || !obs.GetAsset(out asset))
                {
                    continue;
                }
                Size size = obs.GetDimension();
                using (System.Drawing.Image image = asset.GetImage(size))
                {
                    Utility.FlipImage(image, obs.FlipHorizontal,obs.FlipVertical);
                    finalGraphic.DrawImage(image, new PointF((float)(obs.Location.X - minOffsetX), (float)(obs.Location.Y - minOffsetY)));
                    temp++;
                }
            }
            //BackColor = System.Drawing.Color.Transparent;
            currentImage = finalImage;
        }
        private Rectangle GetObstacleRect(Obstacles obs)
        {
            Asset asset;
            if(obs.GetAsset(out asset))
            {
                return new Rectangle((int)(obs.Location.X - minOffsetX), (int)(obs.Location.Y - minOffsetY), (int)(asset.rect.width * obs.Scale * asset.scaleRatio.x), (int)(asset.rect.height * obs.Scale * asset.scaleRatio.y));
            }
            else
            {
                return new Rectangle(-1,-1,-1,-1);
            }
        }
        public Size GetOffset(Point location )
        {
            return new Size((int)(location.X-minOffsetX),(int)(location.Y - minOffsetY));
        }
        public Size GetMapOffset()
        {
            return canvas.Size;
        }
        private void MapCanvas_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;
            //AssetsManager assetsManager = AssetsManager.GetInstance();
            g.DrawImage(currentImage, new PointF(0, 0));
            // Create pen.
            Pen blackPen = new Pen(System.Drawing.Color.White, 2);

            // Draw rectangle to screen.
            if(selectRect.X >= 0)
            {
                e.Graphics.DrawRectangle(blackPen, selectRect.X, selectRect.Y, selectRect.Width, selectRect.Height);
            }
        }
        public void SetSelect(Obstacles obs)
        {
            Rectangle rect = GetObstacleRect(obs);
            if(rect.X < 0)
            {
                return;
            }
            selectRect = rect;
        }
        public void UnsetSelect()
        {
            selectRect = new Rectangle(-1, -1, -1, -1);
        }
        public bool CanZoomIn()
        {
            return CurrentScale > 0.25f;
        }
        public void ZoomIn()
        {
            if(!CanZoomIn()){
                return;
            }
            switch (CurrentScale)
            {
                case 1.5f:
                    CurrentScale = 1.25f;
                    break;
                case 1.25f:
                    CurrentScale = 1.0f;
                    break;
                case 1.0f:
                    CurrentScale = 0.75f;
                    break;
                case 0.75f:
                    CurrentScale = 0.5f;
                    break;
                case 0.5f:
                    CurrentScale = 0.25f;
                    break;
                default:
                    CurrentScale = 0.25f;
                    break;
            }
            MapRefresh();
            Refresh();
        }
        public bool CanZoomOut()
        {
            return CurrentScale < 1.5;
        }
        public void ZoomOut()
        {
            if (!CanZoomOut())
            {
                return;
            }
            switch (CurrentScale)
            {
                case 1.25f:
                    CurrentScale = 1.5f;
                    break;
                case 1.0f:
                    CurrentScale = 1.25f;
                    break;
                case 0.75f:
                    CurrentScale = 1.0f;
                    break;
                case 0.5f:
                    CurrentScale = 0.75f;
                    break;
                case 0.25f:
                    CurrentScale = 0.5f;
                    break;
                default:
                    CurrentScale = 1.5f;
                    break;
            }
            MapRefresh();
            Refresh();
        }
        private void MapCanvas_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        LeftMouseDown(point);
                    }
                    break;
                case MouseButtons.Right:
                    {
                        RightMouseDown(point);
                    }
                    break;
            }

        }
        private void LeftMouseDown(System.Drawing.Point point)
        {
            foreach (var obstacle in listOfLoadedAssets)
            {
                Asset asset;
                if (obstacle.GetAsset(out asset))
                {
                    //asset.rect.x
                }
            }
            Console.WriteLine("Left:" + point.ToString());
            //AssetsManager assetsManager = AssetsManager.GetInstance();
        }
        private void RightMouseDown(System.Drawing.Point point)
        {
            System.Drawing.Point adjustedPoint = new System.Drawing.Point(point.X - HorizontalScroll.Value, point.Y - VerticalScroll.Value);
            canvasContextMenu.Show(this, adjustedPoint);//places the menu at the pointer position
            Console.WriteLine("Right:" + adjustedPoint.ToString());
            var filteredList = listOfLoadedAssets.Where((Obstacles obs) => {
                return false;
            }).ToList();
        }
        /*private void CanvasContextMenu_MouseClick(object sender, MouseEventArgs me)
        {
            System.Drawing.Point coordinates = me.Location;
            canvasContextMenu.ContextMenu.Show(canvasContextMenu, new System.Drawing.Point(coordinates.X, coordinates.Y));
        }*/
    }
}
