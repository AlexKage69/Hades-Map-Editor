using Hades_Map_Editor.Data;
using Hades_Map_Editor.Data.Project;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace Hades_Map_Editor.Managers
{
    public sealed class ConfigManager
    {
        private static ConfigManager _instance;
        public static ConfigManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConfigManager();
            }
            return _instance;
        }
        private string configPath;
        private ConfigurationSave configurations;
        
        private ConfigManager() {
            configPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\config.json";
            LoadConfig();
        }
        public ConfigurationSave GetConfigurations()
        {
            return configurations;
        }
        private void LoadConfig(bool reset = false)
        {
            bool newConfig;
            if (!File.Exists(configPath) || reset)
            {
                newConfig = true;
                configurations = CreateConfig();
            }
            else
            {
                newConfig = false;
                string text = File.ReadAllText(configPath);
                configurations = JsonConvert.DeserializeObject<ConfigurationSave>(text);
            }
            if (newConfig)
            {
                SaveConfig();
            }
        }
        private ConfigurationSave CreateConfig()
        {
            var configsave = new ConfigurationSave
            {
                hadesPath = "G:\\SteamLibrary\\steamapps\\common\\Hades",
                hades2Path = "G:\\SteamLibrary\\steamapps\\common\\Hades II",
                projectPath = "G:\\CDrive\\source\\repos\\Hades-Map-Editor\\bin\\Debug\\Projects",
                pythonPath = "G:\\Python315",
                resourcesPath = "G:\\CDrive\\source\\repos\\Hades-Map-Editor\\bin\\Debug\\Resources"
            };
            return configsave;
            // Open Dialog to fetch all.
            /*configs = new Dictionary<string, ConfigData>();
            string defaultFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            bool hasHadesPath = configFile.TryGetValue(GameVersion.Hades.ToString(), out var hadesPath);
            configs.Add(GameVersion.Hades.ToString(), new ConfigData(GameVersion.Hades.ToString(), defaultFolder, @"Select your Hades Game Folder, I.e: C:\Hades", (hasHadesPath) ? hadesPath : ""));
            bool hasHades2Path = configFile.TryGetValue(GameVersion.Hades2.ToString(), out var hades2Path);
            configs.Add(GameVersion.Hades2.ToString(), new ConfigData(GameVersion.Hades2.ToString(), defaultFolder, @"Select your Hades II Game Folder, I.e: C:\Hades", (hasHades2Path) ? hades2Path : ""));
            bool hasResourcesPath = configFile.TryGetValue(ConfigPathType.Resources.ToString(), out var resourcesPath);
            configs.Add(ConfigPathType.Resources.ToString(), new ConfigData(ConfigPathType.Resources.ToString(), defaultFolder, @"Select your Temporary Resources Folder, I.e: C:\...\Documents\Resources", (hasResourcesPath) ? resourcesPath : ""));
            bool hasProjectPath = configFile.TryGetValue(ConfigPathType.Project.ToString(), out var projectPath);
            configs.Add(ConfigPathType.Project.ToString(), new ConfigData(ConfigPathType.Project.ToString(), defaultFolder, @"Select your Default Projects Folder, I.e: C:\...\Documents\Projects", (hasProjectPath) ? projectPath : ""));
            bool hasPythonPath = configFile.TryGetValue(ConfigPathType.Python.ToString(), out var pythonPath);
            configs.Add(ConfigPathType.Python.ToString(), new ConfigData(ConfigPathType.Python.ToString(), defaultFolder, @"Select your Python Executable Folder, I.e: C:\...\AppData\Local\Microsoft\WindowsApps", (hasPythonPath) ? pythonPath : ""));
            projectList = configFile.projectList;*/
        }
        private void SaveConfig()
        {
            var io = IOManager.GetInstance();
            io.SaveJson(configPath, configurations);
        }
        public void AddProjectPath(string path)
        {
            if(configurations.projectList.Find(v => { return v == path; }) == null)
            {
                configurations.projectList.Add(path);
                SaveConfig();
            }
        }
        public void RemoveProjectPath(string path)
        {
            configurations.projectList.RemoveAll(v => { return v == path; });
            SaveConfig();
        }
        public List<string> GetAllProjectPath()
        {
            List<string> result = new List<string>();
            List<string> toremove = new List<string>();
            foreach (var project in configurations.projectList)
            {
                if (!File.Exists(project))
                {
                    toremove.Add(project);
                }
                else
                {
                    result.Add(project);
                }
            }
            foreach (var project in toremove)
            {
                configurations.projectList.Remove(project);
            }
            return result;
        }
    }
    public class ProjectPath
    {
        public bool isOpen;
        public string path;
        public bool HasPath()
        {
            return Directory.Exists(path) && path != "";
        }
        public string GetPath()
        {
            return path;
        }
        public ProjectPath(string path)
        {
            this.path = path;
            isOpen = true;
        }
    }
    public class ConfigData
    {
        string configType;
        string path, defaultPath, defaultMessage;
        public string GetDefaultMessage()
        {
            return defaultMessage;
        }
        public void SetPath(string path)
        {
            this.path = path;
        }
        public bool HasPath()
        {
            return Directory.Exists(path) && path != "";
        }
        public string GetPath()
        {
            if (!HasPath())
            {
                return defaultPath;
            }
            else
            {
                return path;
            }
        }
        public ConfigData(string type, string defaultPath, string defaultMessage, string path = "")
        {
            configType = type;
            this.defaultPath = defaultPath;
            this.defaultMessage = defaultMessage;
            this.path = path;
        }
    }
}
