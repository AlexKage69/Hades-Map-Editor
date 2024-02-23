using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Helper
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    
    public class SubAtlaseJsonWrapper
    {
        public SubAtlaseJson info;
        public Image image;
        public SubAtlaseJsonWrapper(SubAtlaseJson info, Image image)
        {
            this.info = info;
            this.image = image;
        }
    }
    public class AtlasJson
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
    }
    public class SubAtlaseJson
    {
        public string name { get; set; }
        public Rect rect { get; set; }
        public Point topLeft { get; set; }
        public Point originalSize { get; set; }
        public Scale scaleRatio { get; set; }
        public bool isMulti { get; set; }
        public bool isMip { get; set; }
        public bool isAlpha8 { get; set; }
        public List<Point> hull { get; set; }
        public string GetName()
        {
            return name.Split('\\').Last().Replace("_", "");
        }
    }

    public struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    public struct Scale
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public struct Rect
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Rect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }

}
