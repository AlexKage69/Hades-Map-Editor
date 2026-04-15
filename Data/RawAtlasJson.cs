using Hades_Map_Editor.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hades_Map_Editor.Data.Obstacles;

namespace Hades_Map_Editor
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class RawAtlasJson
    {
        public string name { get; set; }
        public int version { get; set; }
        public List<SubAtlaseJson> subAtlases { get; set; }
        public bool isReference { get; set; }
        public string referencedTextureName { get; set; }

        public string GetName()
        {
            return name.Split('\\').Last();
        }
        public void AppendAssets(Dictionary<AssetType, Dictionary<string, Asset>> assets)
        {
            if (subAtlases != null && subAtlases.Count > 0){
                MapAssetCommon parent = new MapAssetCommon(this);
                foreach (var assetSub in subAtlases)
                {
                    Asset asset =  new Asset(parent, assetSub);
                    if (!assets.ContainsKey(asset.type))
                    {
                        Dictionary<string, Asset> list = new Dictionary<string, Asset>();
                        assets.Add(asset.type, list);
                    }
                    if (!assets[asset.type].ContainsKey(asset.name))
                    {
                        assets[asset.type].Add(asset.name, asset);
                    }
                    else if (assetSub.IsBetterThan(assets[asset.type][asset.name]))
                    {
                        assets[asset.type][asset.name] = asset;
                    }                    
                }
            }
        }
    }
    public class SubAtlaseJson
    {
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
        public List<JsonPoint> hull { get; set; }
        private List<Point> pHull
        {
            get { return new List<Point>(); }
            set
            {
                pHull = new List<Point>();
            }
        }
        private Rectangle pRect;
        private Point pTopLeft;
        private Point pOriginalSize;

        public List<Point> GetHull()
        {
            return pHull;
        }
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
        public string GetName()
        {
            return name.Split('\\').Last().Replace("_", "");
        }
        public bool IsBetterThan(Asset asset)
        {
            return Math.Abs(asset.scaleRatio.x - 1.0) < Math.Abs(scaleRatio.x - 1.0);
        }
    }
    public struct Scale
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public struct JsonRect
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public JsonRect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }

}
