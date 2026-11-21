using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Components.Dialogs
{
    class PathsTab : TabPage, IComponent
    {
        ConfigurationLink hadesPath, hades2Path, pythonPath, projectPath, resourcesPath;
        Button saveButton;
        public PathsTab() : base("Paths")
        {
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            Dock = DockStyle.Fill;
            saveButton = new Button();
            saveButton.Image = Image.FromFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Res\save_button.png");
            saveButton.Dock = DockStyle.Bottom;
            hadesPath = new ConfigurationLink("Hades Path", saveButton);
            hades2Path = new ConfigurationLink("Hades II Path", saveButton);
            pythonPath = new ConfigurationLink("Python Path", saveButton);
            projectPath = new ConfigurationLink("Default Project Path", saveButton);
            resourcesPath = new ConfigurationLink("Resources Path", saveButton);

            Controls.Add(saveButton);
            Controls.Add(hadesPath);
            Controls.Add(hades2Path);
            Controls.Add(pythonPath);
            Controls.Add(projectPath);
            Controls.Add(resourcesPath);
        }

        public void Populate()
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            hadesPath.SetText(configManager.GetConfigurations().hadesPath);
            hades2Path.SetText(configManager.GetConfigurations().hades2Path);
            pythonPath.SetText(configManager.GetConfigurations().pythonPath);
            projectPath.SetText(configManager.GetConfigurations().projectPath);
            resourcesPath.SetText(configManager.GetConfigurations().resourcesPath);

            saveButton.Click += (s, e) => Action_SaveButton(s, e);
            hadesPath.browseButton.Click += (s, e) => Action_HadesPath_BrowseButton(s, e);
            hades2Path.browseButton.Click += (s, e) => Action_Hades2Path_BrowseButton(s, e);
            pythonPath.browseButton.Click += (s, e) => Action_PythonPath_BrowseButton(s, e);
            projectPath.browseButton.Click += (s, e) => Action_ProjectPath_BrowseButton(s, e);
            resourcesPath.browseButton.Click += (s, e) => Action_ResourcesPath_BrowseButton(s, e);
            saveButton.Enabled = false;
        }
        private void Action_SaveButton(object sender, EventArgs e)
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            var changeconfig = configManager.GetConfigurations();
            changeconfig.hadesPath = hadesPath.GetText();
            changeconfig.hades2Path = hades2Path.GetText();
            changeconfig.projectPath = projectPath.GetText();
            changeconfig.pythonPath = pythonPath.GetText();
            changeconfig.resourcesPath = resourcesPath.GetText();

            saveButton.Enabled = false;
        }
        private void Action_HadesPath_BrowseButton(object sender, EventArgs e)
        {
            BrowseAction(hadesPath, "Select the path to Hades");
        }
        private void Action_Hades2Path_BrowseButton(object sender, EventArgs e)
        {
            BrowseAction(hades2Path, "Select the path to Hades 2");
        }
        private void Action_PythonPath_BrowseButton(object sender, EventArgs e)
        {
            BrowseAction(pythonPath, "Select the path to Python");
        }
        private void Action_ProjectPath_BrowseButton(object sender, EventArgs e)
        {
            BrowseAction(projectPath, "Select the path to Projects");
        }
        private void Action_ResourcesPath_BrowseButton(object sender, EventArgs e)
        {
            BrowseAction(resourcesPath, "Select the path to Resources");
        }
        private void BrowseAction(ConfigurationLink button, string description)
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            IOManager io = IOManager.GetInstance();
            string newPath = "";
            try
            {
                newPath = io.GetFolderDialog(button.GetText(), description);
            }
            catch { }
            if (newPath != "")
            {
                button.SetText(newPath);
                saveButton.Enabled = true;
            }
        }
    }
}
