using Hades_Map_Helper.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.Managers
{
    public sealed class SaveManager
    {
        private static SaveManager _instance;
        public static SaveManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }
        private SaveManager() { }
        public ProjectData CreateProject(string path)
        {
            MapData mapData = new MapData();
            mapData.Obstacles = new List<Obstacle>();
            ProjectData projectData =  new ProjectData(path, mapData);
            SaveProject(projectData);

            ConfigManager config = ConfigManager.GetInstance();
            config.SetProjectPath(projectData.projectPath);
            return projectData;
        }
        public void SaveProject(ProjectData projectData)
        {
            if (projectData.projectPath == "")
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = @"C:\Users\Alexandre-i5\source\repos\Hades Map Helper\test_data\sample\",
                    Title = "Browse Map Texts",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "hades_map",
                    Filter = "map texts (*.hades_map)|*.hades_map",
                    FilterIndex = 2,
                    RestoreDirectory = true,
                };

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    projectData.projectPath = saveFileDialog1.FileName;
                }
            }
            using (StreamWriter file = new StreamWriter(projectData.projectPath))
            {
                string json = JsonConvert.SerializeObject(projectData);
                file.Write(json);
            }
        }
        public ProjectData LoadProject(string path)
        {
            ProjectData projectData;
            if (path == "")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = @"C:\Users\Alexandre-i5\source\repos\Hades Map Helper\test_data\sample\",
                    Title = "Browse Map Texts",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "hades_map",
                    Filter = "map texts (*.hades_map)|*.hades_map",
                    FilterIndex = 2,
                    RestoreDirectory = true,

                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                }
            }
            if(path == "") // Didn't load
            {
                throw new NoFileLoadedException();
            }
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                projectData = JsonConvert.DeserializeObject<ProjectData>(json);
            }
            LoadAssetsToMapData(projectData.mapData);
            ConfigManager config = ConfigManager.GetInstance();
            config.SetProjectPath(projectData.projectPath);
            return projectData;            
        }
        public void ExportMap(MapData mapData)
        {

        }
        public ProjectData ImportMap(string mapPath)
        {
            MapData mapData;
            if (mapPath == "")
            {
                try
                {
                    mapPath = OpenFile("Browse Map Texts", "map_text", "map texts (*.map_text)|*.map_text");
                }
                catch (NoFileLoadedException e)
                {
                    throw e;
                }
                         
            }
            using (StreamReader r = new StreamReader(mapPath))
            {
                string json = r.ReadToEnd();
                mapData = JsonConvert.DeserializeObject<MapData>(json);
            }
            LoadAssetsToMapData(mapData);
            string projectPath = Path.ChangeExtension(mapPath, ".hades_map");
            ProjectData projectData = new ProjectData(projectPath, mapData);
            SaveProject(projectData);
            return projectData;

        }
        private void LoadAssetsToMapData(MapData mapData)
        {
            AssetsManager assetsManager = AssetsManager.GetInstance();
            foreach (Obstacle obs in mapData.Obstacles)
            {
                Asset asset;
                if (assetsManager.GetAsset(obs.Name, out asset))
                {
                    obs.SetAsset(asset);
                }
            }
        }
        public void SaveAssets(Assets _assets)
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            using (StreamWriter file = new StreamWriter(configManager.GetResourcesPath() + @"\assets.json"))
            {
                string json = JsonConvert.SerializeObject(_assets);
                file.Write(json);
            }
        }
        public Assets LoadAssets()
        {
            Assets assets;
            ConfigManager configManager = ConfigManager.GetInstance();
            if (File.Exists(configManager.GetResourcesPath() + @"\assets.json"))
            {
                using (StreamReader r = new StreamReader(configManager.GetResourcesPath() + @"\assets.json"))
                {
                    string json = r.ReadToEnd();
                    assets = JsonConvert.DeserializeObject<Assets>(json);
                }
            }
            else
            {
                throw new Exception("Missing assets file");
            }
            return assets;
        }
        private string OpenFile(
            string title = "Browse Files",
            string defaultExt = "hades_map",
            string filter = "map texts (*.hades_map)|*.hades_map",
            string initialDirectory = @"C:\Users\Alexandre-i5\source\repos\Hades Map Helper\test_data\sample\"    
            )
        {
            string path = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = initialDirectory,
                Title = title,

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = defaultExt,
                Filter = filter,
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
            }
            if(path == "")
            {
                throw new NoFileLoadedException();
            }
            else
            {
                return path;
            }
        }
    }
}
