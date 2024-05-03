using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Helper.Data
{
    public class ProjectData
    {
        public string projectPath;
        public string name;
        public MapData mapData;

        public ProjectData(string projectPath, MapData mapData)
        {
            this.mapData = mapData;
            this.projectPath = projectPath;
            name = Path.GetFileNameWithoutExtension(projectPath);
        }
        /*public void OpenMap(string mapPath)
        {
            var limit = 0;
            foreach (var obstacle in originalThingText.Obstacles)
            {
                elements.Add(obstacle.Id, new MapElement(obstacle));
                if (assetManager.IsAssetExists(obstacle.Name))
                {
                    if (obstacle.Location.X < f_x)
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
            Console.WriteLine("f_x:" + f_x + ",f_y:" + f_y + ",l_x:" + l_x + ",l_y:" + l_y);
            Console.WriteLine("End Elements");
        }*/
    }
}
