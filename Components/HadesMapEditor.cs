using Hades_Map_Editor.Components;
using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
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
        public CustomTabControl tabPage;

        //AssetPanel assetPanel;
        //MapPanel mapPanel;
        //ToolbarPanel toolbarPanel;
        //ElementPanel elementPanel;
        //PropertyPanel2 propertyPanel;
        public HadesMapEditor(Form parent)
        {
            form = parent;
            FormManager.SetForm(this);
            tabPage = new CustomTabControl();
            topMenuStrip = new TopMenuStrip(this);
            bottomMenuStrip = new BottomMenuStrip();

            ConfigManager configManager = ConfigManager.GetInstance();
            //configManager.SetConfig("ProjectPath", "", true);
            // Managers
            //assetManager = new AssetManager();
            //mapManager = new MapManager(assetManager);
            //panelManager = new PanelManager(form);

            // Panels
            //assetPanel = new AssetPanel(panelManager, assetManager);
            //mapPanel = new MapPanel(app);
            //toolbarPanel = new ToolbarPanel(panelManager);
            //elementPanel = new ElementPanel(panelManager, elementManager);
            //propertyPanel = new PropertyPanel(panelManager, elementManager);

            form.Controls.Add(tabPage);
            form.Controls.Add(topMenuStrip);
            ////form.Controls.Add(bottomMenuStrip);

            //ProjectData projectData;
            SaveManager saveManager = SaveManager.GetInstance();
            foreach (string path in configManager.GetAllProjectPath())
            {
                try
                {
                    ProjectData projectData = saveManager.LoadProject(path);
                    tabPage.CreateNewTabPage(projectData);
                }catch(Exception)
                {

                }
            }           
        }
    }
}
