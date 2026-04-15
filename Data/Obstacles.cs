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
    public class GameObstacles
    {
        public Dictionary<string, GameObstacle> biomes;
        public GameObstacles()
        {
            biomes = new Dictionary<string, GameObstacle>();
        }
    }
    public class GameObstacle
    {
        public string name { get; set; }
        public string packageName { get; set; }
        public double thingTallness { get; set; }
        public string thingGraphic { get; set; }
        public Point thingOffset { get; set; }
    }
}
