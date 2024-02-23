using Hades_Map_Helper.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Helper.Managers
{
    class MapManager
    {
        private string assetPath, mapPath;
        private ThingText originalThingText;
        private Dictionary<int, MapElement> elements;
        private List<int> inUsed;
        private double f_x = 10000, f_y = 10000, l_x, l_y;
        private AssetManager assetManager;
        private Size sideBuffer = new Size(25, 25);
        public MapManager(AssetManager assetManager)
        {
            assetPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\data\";
            elements = new Dictionary<int, MapElement>();
            inUsed = new List<int>();
            this.assetManager = assetManager;
            OpenMap(assetPath + @"sample\A_Combat01.json");
        }
        public bool isLoaded()
        {
            return originalThingText != null;
        }
        public void OpenMap(string mapPath)
        {
            Console.WriteLine("Start Elements");
            this.mapPath = mapPath;
            using (StreamReader r = new StreamReader(this.mapPath))
            {
                string json = r.ReadToEnd();
                originalThingText = JsonConvert.DeserializeObject<ThingText>(json);
            }
            var limit = 0;
            foreach (var obstacle in originalThingText.Obstacles)
            {
                elements.Add(obstacle.Id, new MapElement(obstacle));
                if (assetManager.IsAssetExists(obstacle.Name))
                {
                    if(obstacle.Location.X < f_x)
                    {
                        f_x = obstacle.Location.X;
                    }
                    if (obstacle.Location.Y < f_y)
                    {
                        f_y = obstacle.Location.Y;
                    }
                    if (obstacle.Location.X > f_x)
                    {
                        l_x = obstacle.Location.X; 
                    }
                    if (obstacle.Location.Y > f_y)
                    {
                        l_y = obstacle.Location.Y;
                    }
                    inUsed.Add(obstacle.Id);
                }
                limit++;
            }
            Console.WriteLine("f_x:"+f_x+",f_y:"+f_y);
            Console.WriteLine("End Elements");
        }
        public Size GetMapSize()
        {
            return new Size((int)((l_x-f_x+sideBuffer.Width*2)),(int)((l_y - f_y + sideBuffer.Height * 2)));
        }
        public Size GetMapOriginator()
        {
            return new Size((int)(f_x * AssetManager.OVERALL_SCALE), (int)(f_y * AssetManager.OVERALL_SCALE));
        }
        public int[] GetAllIds()
        {
            return inUsed.ToArray();
        }
        public MapElement GetElement(int id)
        {
            return elements[id];
        }

        public List<MapElement> GetElementsAt(System.Drawing.Point coordinates)
        {
            List<MapElement> list = new List<MapElement>();
            foreach(var element in elements)
            {
                var area = element.Value.area;
                int xMin = area.X, 
                    xMax = area.X + area.Width,
                    yMin = area.Y,
                    yMax = area.Y + area.Height;

                if (coordinates.X > xMin && coordinates.X < xMax && coordinates.Y > yMin && coordinates.Y < yMax) {
                    list.Add(element.Value);
                }
            }
            return list;
        }
    }
}
