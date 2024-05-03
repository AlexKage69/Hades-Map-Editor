using Hades_Map_Editor.Data;
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
    public sealed class ConfigManager
    {
        private static ConfigManager _instance;
        private string configPath;
        private ConfigData configs;
        public static ConfigManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConfigManager();
            }
            return _instance;
        }
        private ConfigManager() {
            configPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\config.json";
            configs = LoadConfig(configPath);
            CreateResourcesFolder();
        }
        private ConfigData LoadConfig(string path)
        {
            if(File.Exists(path)){
                string text = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<ConfigData>(text);
            }
            else
            {
                return CreateConfig(path);
            }
        }
        private ConfigData CreateConfig(string path)
        {
            ConfigData newConfig = new ConfigData();
            SaveConfig(path, newConfig);
            return newConfig;
        }
        private void SaveConfig(string path, ConfigData configs)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                string json = JsonConvert.SerializeObject(configs);
                file.Write(json);
            }
        }
        // Variable
        public void CreateResourcesFolder()
        {
            configs.ResourcesPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Resources";
            if (!Directory.Exists(configs.ResourcesPath))
            {
                Directory.CreateDirectory(configs.ResourcesPath);
            }           
        }
        public string GetHadesPath(bool reset = false)
        {
            if (configs.HadesPath == null || reset)
            {
                try
                {
                    configs.HadesPath = SetPath("Select your Hades Game Folder");
                }
                catch (Exception)
                {
                    return "";
                }
            }
            return configs.HadesPath;
        }
        public string GetResourcesPath()
        {
            if (configs.ResourcesPath == null)
            {
                configs.ResourcesPath = SetPath("Select your Temporary Resources Folder");
            }
            return configs.ResourcesPath;
        }
        public string GetProjectPath(int i)
        {
            return configs.OpenProjects[i];
        }
        public List<string> GetAllProjectPath()
        {
            return configs.OpenProjects;
        }
        public List<string> GetRecentPath()
        {
            return configs.RecentProjects;
        }
        public string SetPath(string description = null)
        {
            string newPath = "";
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog { };
            if(description != null)
            {
                openFileDialog1.Description = description;
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                newPath = openFileDialog1.SelectedPath;
                if (newPath == "") // Didn't load
                {
                    throw new NoFileLoadedException();
                }
            }
            SaveConfig(configPath, configs);
            return newPath;
        }
        public void SetProjectPath(string path)
        {
            if (!configs.OpenProjects.Contains(path))
            {
                configs.OpenProjects.Add(path);
                if (!configs.RecentProjects.Contains(path))
                {
                    configs.RecentProjects.Add(path);
                }
                if (configs.RecentProjects.Count > 10)
                {
                    configs.RecentProjects.RemoveAt(0);
                }
            }
            SaveConfig(configPath, configs);
        }
        public void RemoveProjectPath(string path)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog { };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (configs.OpenProjects.Contains(path))
                {
                    configs.OpenProjects.Remove(openFileDialog1.SelectedPath);
                }
            }
            SaveConfig(configPath, configs);
        }
    }
    public class ConfigData
    {
        public List<string> OpenProjects;
        public List<string> RecentProjects;
        public string HadesPath;
        public string ResourcesPath;
        public ConfigData()
        {
            OpenProjects = new List<string>();
            RecentProjects = new List<string>();
        }
    }
}
