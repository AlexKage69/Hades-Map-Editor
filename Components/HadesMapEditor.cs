using Hades_Map_Editor.BottomMenu;
using Hades_Map_Editor.Components;
using Hades_Map_Editor.Components.Dialogs;
using Hades_Map_Editor.Data.Project;
using Hades_Map_Editor.Managers;
using Hades_Map_Editor.TopMenu;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor
{
    public class HadesMapEditor
    {
        public Form form;
        //public AssetManager assetManager;
        //public PanelManager panelManager;
        //public MapManager mapManager;

        public TopMenuStrip topMenuStrip;
        public BottomMenuStrip bottomMenuStrip;
        public MainControl tabPage;
        //AssetPanel assetPanel;
        //MapPanel mapPanel;
        //ToolbarPanel toolbarPanel;
        //ElementPanel elementPanel;
        //PropertyPanel2 propertyPanel;
        public HadesMapEditor(Form parent)
        {
            form = parent;
            FormManager.SetForm(this);
            tabPage = new MainControl();
            topMenuStrip = new TopMenuStrip(this);
            bottomMenuStrip = new BottomMenuStrip();

            ConfigManager configManager = ConfigManager.GetInstance();

            form.Controls.Add(tabPage);
            form.Controls.Add(topMenuStrip);
            form.Controls.Add(bottomMenuStrip);

            IOManager io = IOManager.GetInstance();
            foreach (string path in configManager.GetAllProjectPath())
            {
                try
                {
                    ProjectData projectData = io.LoadProject(path);
                    tabPage.CreateNewTabPage(projectData);
                }catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }           
        }
    }
}
