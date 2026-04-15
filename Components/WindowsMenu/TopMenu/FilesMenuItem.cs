using Hades_Map_Editor.Components.Windows;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Hades_Map_Editor.Components
{
    public class FilesMenuItem : ToolStripMenuItem, IComponent
    {
        public ToolStripMenuItem
            newProject, newHadesProject, newHades2Project, openMapOrProject, tempImport, recentProjects,
            save, saveAs, saveAll, export, exportAs, exportAsImage, 
            close, closeAll, parameters, exit;
        HadesMapEditor app;
        public FilesMenuItem(HadesMapEditor app) : base("Files")
        {
            this.app = app;
            Initialize();
            Populate();
            Dock = DockStyle.Top;
        }
        public void Initialize()
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            ((ToolStripDropDownMenu)(DropDown)).ShowImageMargin = true;
            ((ToolStripDropDownMenu)(DropDown)).ShowCheckMargin = false;

            newProject = new ToolStripMenuItem("New");
            newHadesProject = new ToolStripMenuItem("New Hades Project");
            newHades2Project = new ToolStripMenuItem("New Hades 2 Project");
            openMapOrProject = new ToolStripMenuItem("Open .thing_text/.hades_map");
            tempImport = new ToolStripMenuItem("Open .thing_text (Temporary)");
            recentProjects = new ToolStripMenuItem("Recent Projects");
            save = new ToolStripMenuItem("Save");
            saveAs = new ToolStripMenuItem("Save As");
            saveAll = new ToolStripMenuItem("Save All");
            export = new ToolStripMenuItem("Export");
            exportAs = new ToolStripMenuItem("Export As");
            exportAsImage = new ToolStripMenuItem("Export As Image");
            close = new ToolStripMenuItem("Close");
            closeAll = new ToolStripMenuItem("Close All");
            parameters = new ToolStripMenuItem("Parameters");
            exit = new ToolStripMenuItem("Exit");

            DropDownItems.Add(newProject);
            newProject.DropDownItems.Add(newHadesProject);
            newProject.DropDownItems.Add(newHades2Project);
            DropDownItems.Add(openMapOrProject);
            DropDownItems.Add(tempImport);
            DropDownItems.Add(recentProjects);
            foreach (string project in configManager.GetAllProjectPath())
            {
                recentProjects.DropDownItems.Add(new ToolStripMenuItem(Path.GetFileName(project)));
            }
            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(save);
            DropDownItems.Add(saveAs);
            DropDownItems.Add(saveAll);
            DropDownItems.Add(export);
            DropDownItems.Add(exportAs);
            DropDownItems.Add(exportAsImage);
            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(parameters);
            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(close);
            DropDownItems.Add(closeAll);
            DropDownItems.Add(exit);
        }

        public void Populate()
        {
            newHadesProject.Click += NewHadesProject_Action;
            newHadesProject.Enabled = true;
            newHades2Project.Click += NewHades2Project_Action;
            newHades2Project.Enabled = false;
            openMapOrProject.Click += OpenMapOrProject_Action;
            tempImport.Click += TemporaryImport_Action;
            if (recentProjects.DropDownItems.Count > 0)
            {
                foreach (ToolStripMenuItem recentAction in recentProjects.DropDownItems)
                {
                    recentAction.Click += Recent_Action;
                }
            }
            else
            {
                recentProjects.Enabled = false;
            }
            save.Click += Save_Action;
            save.Enabled = false;
            saveAs.Click += SaveAs_Action;
            saveAs.Enabled = false;
            saveAll.Click += SaveAll_Action;
            saveAll.Enabled = false;
            export.Click += Export_Action;
            export.Enabled = false;
            exportAs.Click += ExportAs_Action;
            exportAs.Enabled = false;
            exportAsImage.Click += ExportAsImage_Action;
            exportAsImage.Enabled = false;
            close.Click += Close_Action;
            close.Enabled = false;
            closeAll.Click += CloseAll_Action;
            closeAll.Enabled = false;
            parameters.Click += Parameters_Action;
            exit.Click += Exit_Action;
            exit.Enabled = false;
        }
        private void NewHadesProject_Action(object sender, EventArgs e)
        {
            MainWindows.ShowDialog();
        }
        private void NewHades2Project_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void OpenMapOrProject_Action(object sender, EventArgs e)
        {
            SaveManager saveManager = SaveManager.GetInstance();
            try
            {
                app.tabPage.CreateNewTabPage(saveManager.LoadProject(""));
            }
            catch (Exception){ }
        }
        private void TemporaryImport_Action(object sender, EventArgs e)
        {
            SaveManager saveManager = SaveManager.GetInstance();
            try
            {
                app.tabPage.CreateNewTabPage(saveManager.ImportMap(""));
            }
            catch (Exception) { }
        }
        private void Recent_Action(object sender, EventArgs e)
        {
            SaveManager saveManager = SaveManager.GetInstance();
            try
            {
                app.tabPage.CreateNewTabPage(saveManager.LoadProject(""));
            }
            catch (Exception) { }
        }

        private void Save_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveAs_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void SaveAll_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Export_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void ExportAs_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void ExportAsImage_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Close_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void CloseAll_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Parameters_Action(object sender, System.EventArgs e)
        {
            app.parametersDialog.OpenDialog();
        }
        private void Exit_Action(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
