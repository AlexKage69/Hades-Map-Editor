using Hades_Map_Helper.Components;
using Hades_Map_Helper.Data;
using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    public class HadesMapHelper
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
        public HadesMapHelper(Form parent)
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
            /*bool createNew = false;
            if (createNew)
            {
                MapData mapData = new MapData();
                mapData.Obstacles = new List<Obstacle>();
                projectData = new ProjectData("C:/", mapData);
            }
            else
            {
                string path = @"C:\Users\Alexandre-i5\source\repos\Hades Map Helper\test_data\sample\";
                if (File.Exists(path + @"\A_Combat01.hades_map"))
                {
                    projectData = saveManager.LoadProject(path + @"\A_Combat01.hades_map");
                }
                else
                {
                    projectData = saveManager.ImportMap(path + @"\A_Combat01.map_text");
                }
            }
            tabPage.CreateNewTabPage(projectData);*/
            // Read the Application History to open what was open when closed.
        }
    }
}
