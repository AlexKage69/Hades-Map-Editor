using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Editor.Data
{
    public class Assets
    {
        public Dictionary<string, Dictionary<AssetType, Dictionary<string, Asset>>> biomes;
        public Assets()
        {
            biomes = new Dictionary<string, Dictionary<AssetType, Dictionary<string, Asset>>>();
        }
    }
    public class Asset
    {
        public AssetType type { get; set; }
        public string biome { get; set; }
        public string assetpath { get; set; }
        public string name { get; set; }
        public Rect rect { get; set; }
        public Point topLeft { get; set; }
        public Point originalSize { get; set; }
        public Scale scaleRatio { get; set; }
        public bool isMulti { get; set; }
        public bool isMip { get; set; }
        public bool isAlpha8 { get; set; }
        public List<Point> hull { get; set; }

        private Image image;
        public Asset() { }
        public Asset(MapAssetCommon parent, SubAtlaseJson json)
        {
            ConfigManager configManager = ConfigManager.GetInstance();            
            biome = parent.name.Split('\\').Last().Split('_').First();
            assetpath = configManager.GetResourcesPath() + biome + @"\textures\atlases\" + parent.GetName() + ".png";
            name = json.name.Split('\\').Last().Replace("_", "");
            rect = json.rect;
            topLeft = json.topLeft;
            originalSize = json.originalSize;
            scaleRatio = json.scaleRatio;
            isMulti = json.isMulti;
            isMip = json.isMip;
            isAlpha8 = json.isAlpha8;
            hull = json.hull;
            Enum.TryParse(json.name.Split('\\').First(), out AssetType myType);
            type = myType;
        }
        public Image GetImage(double scale = -1, double height = -1)
        {
            if (image == null)
            {
               image = Utility.LoadImage(assetpath, rect);
            }
            if(height != -1)
            {
                return Utility.ResizeImage(image, (int)(scale), (int)(height));
            }
            if(scale != -1)
            {
                return Utility.ResizeImage(image, (int)(rect.width * scale * scaleRatio.x), (int)(rect.height * scale * scaleRatio.y));
            }
            return new Bitmap(image);
        }
    }
    public enum AssetType
    {
        None,
        Fx,
        Tilesets,
    }
}
