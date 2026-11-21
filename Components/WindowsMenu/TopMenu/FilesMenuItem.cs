using Hades_Map_Editor.Components.Dialogs;
using Hades_Map_Editor.Data.Project;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Hades_Map_Editor.TopMenu
{
    public class FilesMenuItem : ToolStripMenuItem, IComponent
    {
        public ToolStripMenuItem
            newProject, newHadesProject, openMapOrProject, /*tempImport,*/ recentProjects,
            save, saveAs, saveAll, export, exportAs, exportAsImage, 
            close, closeAll, parameters, exit;
        //ParametersDialog parametersDialog;
        public FilesMenuItem() : base("Files")
        {
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
            newHadesProject = new ToolStripMenuItem("New Hades Map Project");
            openMapOrProject = new ToolStripMenuItem("Create from .thing_bin");
            //tempImport = new ToolStripMenuItem("Open .thing_text (Temporary)");
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
            newHadesProject.Enabled = false;
            DropDownItems.Add(openMapOrProject);
            //DropDownItems.Add(tempImport);
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
            openMapOrProject.Click += OpenMapOrProject_Action;
            //tempImport.Click += TemporaryImport_Action;
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
        }
        private void NewHadesProject_Action(object sender, EventArgs e)
        {
            IOManager saveManager = IOManager.GetInstance();
            try
            {
                (Parent as TopMenuStrip).GetMainControl().CreateNewTabPage(new ProjectData(saveManager.GetFileDialog(IOManager.DefaultCreateProject)));
            }
            catch (Exception) { }
        }
        private void OpenMapOrProject_Action(object sender, EventArgs e)
        {
            IOManager saveManager = IOManager.GetInstance();
            try
            {
                (Parent as TopMenuStrip).GetMainControl().CreateNewTabPage(saveManager.LoadProject(saveManager.GetFileDialog(IOManager.DefaultLoadProject)));
            }
            catch (Exception){ }
        }
        private void TemporaryImport_Action(object sender, EventArgs e)
        {
            IOManager saveManager = IOManager.GetInstance();
            try
            {
                (Parent as TopMenuStrip).GetMainControl().CreateNewTabPage(saveManager.ImportMap(saveManager.GetFileDialog(IOManager.DefaultImportThingBin)));
            }
            catch (Exception) { }
        }
        private void Recent_Action(object sender, EventArgs e)
        {
            IOManager saveManager = IOManager.GetInstance();
            try
            {
                (Parent as TopMenuStrip).GetMainControl().CreateNewTabPage(saveManager.LoadProject(""));
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
            (Parent as TopMenuStrip).GetMainControl().parametersDialog.OpenDialog();
        }
        private void Exit_Action(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
