using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Helper
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class MapAssetCommon
    {
        public string name { get; set; }
        public int version { get; set; }
        public bool isReference { get; set; }
        public string referencedTextureName { get; set; }
        public MapAssetCommon() { }
        public MapAssetCommon(RawAtlasJson json)
        {
            name = json.name;
            version = json.version;
            isReference = json.isReference;
            referencedTextureName = json.referencedTextureName;
        }
        public string GetName()
        {
            return name.Split('\\').Last();
        }
    }
    public class MapAsset
    {
        public MapAssetCommon parent { get; set; }
        public string name { get; set; }
        public Rect rect { get; set; }
        public Point topLeft { get; set; }
        public Point originalSize { get; set; }
        public Scale scaleRatio { get; set; }
        public bool isMulti { get; set; }
        public bool isMip { get; set; }
        public bool isAlpha8 { get; set; }
        public List<Point> hull { get; set; }
        public MapAsset(){}
        public MapAsset(MapAssetCommon parent, SubAtlaseJson json)
        {
            this.parent = parent;
            name = json.name;
            rect = json.rect;
            topLeft = json.topLeft;
            originalSize = json.originalSize;
            scaleRatio = json.scaleRatio;
            isMulti = json.isMulti;
            isMip = json.isMip;
            isAlpha8 = json.isAlpha8;
            hull = json.hull;
        }
        public string GetName()
        {
            return name.Split('\\').Last().Replace("_", "");
        }
        public string GetPackageName()
        {
            var name = parent.name.Split('\\').Last().Split('_').First();
            Console.WriteLine(name);
            return name;
        }
    }
}
