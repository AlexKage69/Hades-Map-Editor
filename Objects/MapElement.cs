using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.Objects
{
    class MapElement
    {
        public Obstacle info;
        public Rectangle area;
        public MapElement(Obstacle obstacle)
        {
            info = obstacle;
        }
        public int GetXCenter()
        {
            return (int)(info.Location.X * AssetManager.OVERALL_SCALE);
        }
        public int GetYCenter()
        {
            return (int)(info.Location.Y * AssetManager.OVERALL_SCALE);
        }
    }
}
