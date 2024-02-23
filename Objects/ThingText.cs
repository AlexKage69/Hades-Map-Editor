using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Helper
{
    public class ThingText
    {
        public List<Obstacle> Obstacles { get; set; }
    }
    public class Obstacle
    {
        public bool ActivateAtRange { get; set; }
        public double ActivationRange { get; set; }
        public bool Active { get; set; }
        public bool AllowMovementReaction { get; set; }
        public double Ambient { get; set; }
        public double Angle { get; set; }
        public int AttachToID { get; set; }
        public List<object> AttachedIDs { get; set; }
        public bool CausesOcculsion { get; set; }
        public bool Clutter { get; set; }
        public bool Collision { get; set; }
        public Color Color { get; set; }
        public object Comments { get; set; }
        public bool? CreatesShadows { get; set; }
        public string DataType { get; set; }
        public bool DrawVfxOnTop { get; set; }
        public bool FlipHorizontal { get; set; }
        public bool FlipVertical { get; set; }
        public List<string> GroupNames { get; set; }
        public object HelpTextID { get; set; }
        public double Hue { get; set; }
        public int Id { get; set; }
        public bool IgnoreGridManager { get; set; }
        public bool Invert { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
        public double OffsetZ { get; set; }
        public double ParallaxAmount { get; set; }
        public List<object> Points { get; set; }
        public double Saturation { get; set; }
        public double Scale { get; set; }
        public double SkewAngle { get; set; }
        public double SkewScale { get; set; }
        public int SortIndex { get; set; }
        public bool? StopsLight { get; set; }
        public double Tallness { get; set; }
        public bool UseBoundsForSortArea { get; set; }
        public double Value { get; set; }
    }
    public struct Color
    {
        public int A { get; set; }
        public int B { get; set; }
        public int G { get; set; }
        public int R { get; set; }
    }

    public struct Location
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

}
