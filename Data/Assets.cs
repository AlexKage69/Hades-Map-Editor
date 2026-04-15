using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hades_Map_Editor.Data.Obstacles;

namespace Hades_Map_Editor.Data
{
    public class Assets
    {
        public Dictionary<string, Dictionary<AssetType, Dictionary<string, Asset>>> biomes;
        public Assets()
        {
            biomes = new Dictionary<string, Dictionary<AssetType, Dictionary<string, Asset>>>();
        }
        public void LoadImages()
        {
            foreach (var biome in biomes)
            {
                foreach (var type in biome.Value)
                {
                    foreach (var asset in type.Value)
                    {
                        asset.Value.LoadImage();
                    }
                }
            }
        }
    }
    public class Asset
    {
        public AssetType type { get; set; }
        public string biome { get; set; }
        public string assetpath { get; set; }
        public string name { get; set; }
        public JsonRect rect
        {
            get { return new JsonRect { x = pRect.X, y = pRect.Y, width = pRect.Width, height = pRect.Height }; }
            set
            {
                pRect = new Rectangle(value.x, value.y, value.width, value.height);
            }
        }
        public JsonPoint topLeft
        {
            get { return new JsonPoint { X = pTopLeft.X, Y = pTopLeft.Y }; }
            set
            {
                pTopLeft = new Point((int)value.X, (int)value.Y);
            }
        }
        public JsonPoint originalSize
        {
            get { return new JsonPoint { X = pOriginalSize.X, Y = pOriginalSize.Y }; }
            set
            {
                pOriginalSize = new Point((int)value.X, (int)value.Y);
            }
        }
        public Scale scaleRatio { get; set; }
        public bool isMulti { get; set; }
        public bool isMip { get; set; }
        public bool isAlpha8 { get; set; }
        public List<Point> hull { get; set; }
        private Rectangle pRect;
        private Point pTopLeft;
        private Point pOriginalSize;

        private Image image;
        public Rectangle GetRect()
        {
            return pRect;
        }
        public Point GetTopLeft()
        {
            return pTopLeft;
        }
        public Point GetOriginalSize()
        {
            return pOriginalSize;
        }
        public Asset() { }
        public Asset(MapAssetCommon parent, SubAtlaseJson json)
        {
            ConfigManager configManager = ConfigManager.GetInstance();            
            biome = parent.name.Split('\\').Last().Split('_').First();
            assetpath = configManager.GetPath(ConfigType.ResourcesPath) + @"\" + biome + @"\textures\atlases\" + parent.GetName() + ".png";
            name = json.name.Split('\\').Last().Replace("_", "");
            pRect = json.GetRect();
            pTopLeft = json.GetTopLeft();
            pOriginalSize = json.GetOriginalSize();
            scaleRatio = json.scaleRatio;
            isMulti = json.isMulti;
            isMip = json.isMip;
            isAlpha8 = json.isAlpha8;
            hull = json.GetHull();
            Enum.TryParse(json.name.Split('\\').First(), out AssetType myType);
            type = myType;
            //image = Utility.LoadImage(assetpath, rect);
        }
        public Image GetImage(Size size = new Size())
        {
            if (image == null)
            {
                image = LoadImage();
            }
            if (size.Width == 0 || size.Height == 0)
            {
                return image;
            }
            else
            {
                return Utility.ResizeImage(image, size.Width, size.Height);
            }
        }
        public Image LoadImage()
        {
            if (image == null)
            {
                return Utility.LoadImage(assetpath, GetRect());
            }
            throw new Exception("Couldn't load image");
        }
    }
    public enum AssetType
    {
        None,
        Fx,
        Tilesets,
    }
}
