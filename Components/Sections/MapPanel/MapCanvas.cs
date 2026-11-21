using Hades_Map_Editor.Data.Map;
using Hades_Map_Editor.Data.Asset;
using Hades_Map_Editor.ElementsSection;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static Community.CsharpSqlite.Sqlite3;

namespace Hades_Map_Editor.MapSection
{
    public class MapCanvas : Panel, IComponent
    {
        List<Obstacle> listOfLoadedAssets;
        static int X = 200, Y = 200;
        float CurrentScale = 0.25f;
        double minOffsetX, maxOffsetX, minOffsetY, maxOffsetY;
        Obstacle selected;
        Image currentImage;
        Rectangle selectRect;
        PictureBox canvas;
        ContextMenu canvasContextMenu;
        public MapCanvas(MapPanel parent)
        {
            Parent = parent;
            listOfLoadedAssets = new List<Obstacle>();
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
        }
        public void Populate()
        {
            //MouseDown += new System.Windows.Forms.MouseEventHandler(MapCanvas_MouseDown);
            canvas.MouseDown += (s, e) => MapCanvas_MouseDown(s, e);
            canvas.ContextMenu = canvasContextMenu;
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
            canvas.Size = new System.Drawing.Size((int)((maxOffsetX - minOffsetX)* CurrentScale + 2 * X), (int)((maxOffsetY - minOffsetY) * CurrentScale + 2 * Y));
            Image finalImage = new Bitmap(canvas.Size.Width, canvas.Size.Height);
            Graphics finalGraphic = Graphics.FromImage(finalImage);
            FormManager formManager = FormManager.GetInstance();
            int temp = 0;
            listOfLoadedAssets = listOfLoadedAssets.OrderBy((Obstacle val) => { return val.GetLayerLevel(); }).ToList();
            finalGraphic.ScaleTransform(CurrentScale, CurrentScale);
            foreach (Obstacle obs in listOfLoadedAssets)
            {
                AssetData asset;
                formManager.GetBottomMenu().SetStatuts(obs.Name);
                if (!obs.Active || !obs.GetAsset(out asset))
                {
                    continue;
                }
                System.Drawing.Size size = obs.GetDimension();
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
        private Rectangle GetObstacleRect(Obstacle obs, bool withCanvasScale = false)
        {
            AssetData asset;
            float scale = (withCanvasScale) ? CurrentScale : 1.0f;
            if(obs.GetAsset(out asset))
            {
                return new Rectangle((int)((obs.Location.X - minOffsetX) * scale), (int)((obs.Location.Y - minOffsetY) * scale), (int)(asset.rect.width * obs.Scale * asset.scaleRatio.x * scale), (int)(asset.rect.height * obs.Scale * asset.scaleRatio.y * scale));
            }
            else
            {
                return new Rectangle(-1,-1,-1,-1);
            }
        }
        public System.Drawing.Size GetOffset(System.Drawing.Point location )
        {
            return new System.Drawing.Size((int)(location.X-minOffsetX),(int)(location.Y - minOffsetY));
        }
        public System.Drawing.Size GetMapOffset()
        {
            return canvas.Size;
        }
        private void MapCanvas_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (currentImage != null)
            {
                // Create a local version of the graphics object for the PictureBox.
                Graphics g = e.Graphics;
                //AssetsManager assetsManager = AssetsManager.GetInstance();
                g.DrawImage(currentImage, new PointF(0, 0));
                // Create pen.
                Pen blackPen = new Pen(System.Drawing.Color.White, 2);

                // Draw rectangle to screen.
                if (selectRect.X >= 0)
                {
                    e.Graphics.DrawRectangle(blackPen, selectRect.X, selectRect.Y, selectRect.Width, selectRect.Height);
                }
            }
        }
        public void SetSelect(Obstacle obs)
        {
            Rectangle rect = GetObstacleRect(obs,true);
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
        public float GetCurrentScale()
        {
            return CurrentScale;
        }
        private bool IsObstacleInCoordinate(Obstacle obstacle, System.Drawing.Point point)
        {
            Rectangle rect = GetObstacleRect(obstacle, true);
            return rect.X <= point.X && rect.X + rect.Width >= point.X && rect.Y <= point.Y && rect.Y + rect.Height >= point.Y;
        }
        private void LeftMouseDown(System.Drawing.Point point)
        {
            foreach (var obstacle in listOfLoadedAssets)
            {
                AssetData asset;
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
            var filteredList = listOfLoadedAssets.Where((Obstacle obs) => {
                return IsObstacleInCoordinate(obs, point);
            }).ToList();
            canvasContextMenu.MenuItems.Clear();
            foreach (var item in filteredList)
            {
                canvasContextMenu.MenuItems.Add(item.Id.ToString(), MapCanvas_SelectObstacle);
            }
            canvasContextMenu.Show(this, adjustedPoint);//places the menu at the pointer position
            //Console.WriteLine("Right:" + point.ToString());
        }
        private void MapCanvas_SelectObstacle(object sender, System.EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            int id = int.Parse(item.Text);
            var pp = (Parent as MapPanel).GetProjectPage();
            pp.elementsPanel.FocusOn(id);
            pp.propertiesPanel.FocusOn(id);
            pp.mapPanel.FocusOn(id);
        }
        /*private void CanvasContextMenu_MouseClick(object sender, MouseEventArgs me)
        {
            System.Drawing.Point coordinates = me.Location;
            canvasContextMenu.ContextMenu.Show(canvasContextMenu, new System.Drawing.Point(coordinates.X, coordinates.Y));
        }*/
    }
}
