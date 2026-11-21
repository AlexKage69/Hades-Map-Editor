using Hades_Map_Editor.Data.Project;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.TopMenu
{
    public class TopMenuStrip: MenuStrip, IComponent
    {
        public ToolStripMenuItem 
            filesMenu, editMenu, viewMenu, assetMenu, mapMenu, helpMenu;
        HadesMapEditor app;
        public TopMenuStrip(HadesMapEditor app)
        {
            this.app = app;
            Initialize();
            Populate();
            Dock = DockStyle.Top;
        }
        public void Initialize()
        {
            // Create a MenuStrip control with a new window.
            filesMenu = new FilesMenuItem();
            editMenu = new EditMenuItem();
            viewMenu = new ViewMenuItem();
            assetMenu = new AssetsMenuItem();
            mapMenu = new MapMenuItem();
            helpMenu = new HelpMenuItem();

            // Assign the ToolStripMenuItem that displays 
            // the list of child forms.
            MdiWindowListItem = filesMenu;

            // Add the window ToolStripMenuItem to the MenuStrip.
            Items.Add(filesMenu);
            Items.Add(editMenu);
            Items.Add(viewMenu);
            Items.Add(assetMenu);
            Items.Add(mapMenu);
            Items.Add(helpMenu);
        }

        public void Populate()
        {
        }
        public MainControl GetMainControl()
        {
            return app.tabPage;
        }
        public ProjectPage GetProjectPage()
        {
            return app.tabPage.GetCurrentProjectPage();
        }
        public ProjectData GetData()
        {
            return app.tabPage.GetCurrentProjectPage().GetData();
        }
    }
}
