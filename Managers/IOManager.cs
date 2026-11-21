using Hades_Map_Editor.Data.Asset;
using Hades_Map_Editor.Data.Map;
using Hades_Map_Editor.Data.Project;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Managers
{
    public sealed class IOManager
    {
        private static IOManager _instance;
        public static FileDialogConfig DefaultCreateProject, DefaultLoadProject, DefaultImportThingBin;
        public struct FileDialogConfig
        {
            public bool IsOpen;
            public string InitialDirectory;
            public string Title;
            public string DefaultExt;
            public string Filter;
            public bool CheckFileExists;
            public bool CheckPathExists;

            public FileDialogConfig(bool IsOpen, string InitialDirectory, string Title, string DefaultExt, string Filter, bool CheckFileExists, bool CheckPathExists)
            {
                this.IsOpen = IsOpen;
                this.InitialDirectory = InitialDirectory;
                this.Title = Title;
                this.DefaultExt = DefaultExt;
                this.Filter = Filter;
                this.CheckFileExists = CheckFileExists;
                this.CheckPathExists = CheckPathExists;
            }
        }
        public static IOManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new IOManager();
            }
            return _instance;
        }
        private IOManager() {
            //DefaultCreateProject = new FileDialogConfig (false, projectPath, "Create new project", "hades_map", "hades projects (*.hades_map)|*.hades_map", false, true);
            //DefaultLoadProject = new FileDialogConfig(true, projectPath, "Load project", "hades_map", "hades projects (*.hades_map, *.hades2_map)|*.hades_map, *.hades2_map", true, true);
            //DefaultImportThingBin = new FileDialogConfig(true, projectPath, "Import .thing_bin", "hades_map", "thing_bin (*.thing_bin)|*.thing_bin", true, true);
        }
        public string GetFileDialog(FileDialogConfig config)
        {
            string path = "";
            FileDialog fileDialog;
            if (config.IsOpen) {
                fileDialog = new OpenFileDialog();
            } else {
                fileDialog = new SaveFileDialog();
            }
            fileDialog.InitialDirectory = config.InitialDirectory;
            fileDialog.Title = config.Title;

            fileDialog.CheckFileExists = config.CheckFileExists;
            fileDialog.CheckPathExists = config.CheckPathExists;
            
            fileDialog.DefaultExt = config.DefaultExt;
            fileDialog.Filter = config.Filter;
            fileDialog.FilterIndex = 2;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                path = fileDialog.FileName;
            }
            if(path == "")
            {
                throw (new Exception());
            }
            return path;
        }
        public string GetFolderDialog(string initialPath, string description)
        {
            string path = "";
            FolderBrowserDialog folderDialog;
            folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = initialPath;
            folderDialog.Description = description;

            folderDialog.ShowNewFolderButton = true;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderDialog.SelectedPath;
            }
            if (path == "")
            {
                throw (new Exception());
            }
            return path;
        }
        public void SaveProject(ProjectData projectData)
        {
            ConfigManager config = ConfigManager.GetInstance();
            if (!Directory.Exists(projectData.projectPath))
            {
                Console.WriteLine("Missing: " + projectData.projectPath);
                config.RemoveProjectPath(projectData.projectPath);
            }
            else
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(projectData.projectPath))
                    {
                        string json = JsonConvert.SerializeObject(projectData);
                        file.Write(json);
                    }
                    config.AddProjectPath(projectData.projectPath);
                }
                catch (Exception)
                {
                    config.RemoveProjectPath(projectData.projectPath);
                }
            }
        }
        public ProjectData LoadProject(string path)
        {
            ProjectData projectData;
            if (!File.Exists(path))
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
            config.AddProjectPath(projectData.projectPath);
            return projectData;
        }
        public void SaveJson(string path, object obj)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                string json = JsonConvert.SerializeObject(obj);
                file.Write(json);
            }
        }
        public ProjectData ImportMap(string mapPath)
        {
            MapData mapData;
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
                AssetData asset;
                if (assetsManager.GetAsset(obs.Name, out asset))
                {
                    obs.SetAsset(asset);
                }
            }
        }
        public void SaveAssets(AssetData _assets)
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            using (StreamWriter file = new StreamWriter(configManager.GetConfigurations().resourcesPath + @"\MetaAssets.json"))
            {
                string json = JsonConvert.SerializeObject(_assets);
                file.Write(json);
            }
        }
        public AssetData LoadAssets()
        {
            AssetData assets;
            ConfigManager configManager = ConfigManager.GetInstance();
            string assetsPath = configManager.GetConfigurations().resourcesPath;
            if (File.Exists(assetsPath))
            {
                using (StreamReader r = new StreamReader(assetsPath))
                {
                    string json = r.ReadToEnd();
                    assets = JsonConvert.DeserializeObject<AssetData>(json);
                }
                //assets.LoadImages();
            }
            else
            {
                assets = new AssetData();
                using (StreamWriter file = new StreamWriter(assetsPath))
                {
                    string json = JsonConvert.SerializeObject(assets);
                    file.Write(json);
                }
            }
            return assets;
        }
    }
}
