using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.Components
{
    public class TopMenuStrip: MenuStrip, IComponent
    {
        public ToolStripMenuItem 
            filesMenu, filesSubNew, filesNewAction, filesOpenProjectAction, filesOpenMapAction, filesSubRecent,
            editMenu, assetUndoAction, assetRedoAction, assetCutAction, assetCopyAction, assetPasteAction, 
                assetDeleteAction, assetPreferencesAction,
            viewMenu, 
            assetMenu, assetCompileAction, assetFetchAction, assetSubBiomes,
            mapMenu, 
            helpMenu;
    HadesMapHelper app;
        public List<ToolStripMenuItem> filesRecentAction, assetSubBiomesAction;
        public TopMenuStrip(HadesMapHelper app)
        {
            this.app = app;
            Initialize();
            Populate();
            Dock = DockStyle.Top;
        }
        public void Initialize()
        {
            // Create a MenuStrip control with a new window.
            CreateMainMenuNew();
            CreateMainMenuEdit();
            CreateMainMenuView();
            CreateMainMenuAssets();
            CreateMainMenuMap();
            CreateMainMenuHelp();


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
            filesNewAction.Click += (s, e) => Action_New(s, e);
            filesOpenProjectAction.Click += (s, e) => Action_OpenProject(s, e);
            filesOpenMapAction.Click += (s, e) => Action_OpenMap(s, e);
            assetFetchAction.Click += (s, e) => Action_FetchAssets(s, e);
            assetCompileAction.Click += (s, e) => Action_CompileResources(s, e);
        }

        private void CreateMainMenuNew()
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            filesMenu = new ToolStripMenuItem("Files");
            ((ToolStripDropDownMenu)(filesMenu.DropDown)).ShowImageMargin = false;
            ((ToolStripDropDownMenu)(filesMenu.DropDown)).ShowCheckMargin = true;
            // New feature (Create)
            filesSubNew = new ToolStripMenuItem("New");
            filesNewAction = new ToolStripMenuItem("New Map");
            filesNewAction.Enabled = false;
            // Open feature (Open, Recent)
            filesOpenProjectAction = new ToolStripMenuItem("Open .hades_map");
            filesOpenMapAction = new ToolStripMenuItem("Open .map_text");
            filesSubRecent = new ToolStripMenuItem("Recent Maps");
            filesRecentAction = new List<ToolStripMenuItem>();
            filesSubNew.DropDownItems.Add(filesNewAction);
            filesMenu.DropDownItems.Add(filesSubNew);
            filesMenu.DropDownItems.Add(filesOpenProjectAction);
            filesMenu.DropDownItems.Add(filesOpenMapAction);
            foreach (string project in configManager.GetRecentPath())
            {
                ToolStripMenuItem recentAction = new ToolStripMenuItem(Path.GetFileName(project));
                filesRecentAction.Add(recentAction);
                filesSubRecent.DropDownItems.Add(recentAction);
            }
            filesMenu.DropDownItems.Add(filesSubRecent);

            // Save feature (Save, As, All)

            // Close feature (Close, All, Quit)

        }
        private void CreateMainMenuEdit()
        {
            editMenu = new ToolStripMenuItem("Edit");
            assetUndoAction = new ToolStripMenuItem("Undo");
            assetUndoAction.Enabled = false;
            assetRedoAction = new ToolStripMenuItem("Redo");
            assetRedoAction.Enabled = false;
            assetCutAction = new ToolStripMenuItem("Cut");
            assetCutAction.Enabled = false;
            assetCopyAction = new ToolStripMenuItem("Copy");
            assetCopyAction.Enabled = false;
            assetPasteAction = new ToolStripMenuItem("Paste");
            assetPasteAction.Enabled = false;
            assetDeleteAction = new ToolStripMenuItem("Delete");
            assetDeleteAction.Enabled = false;
            //assetSelectAllAction = new ToolStripMenuItem("Select All");
            assetPreferencesAction = new ToolStripMenuItem("Preferences...");
            assetPreferencesAction.Enabled = false;

            editMenu.DropDownItems.Add(assetUndoAction);
            editMenu.DropDownItems.Add(assetRedoAction);
            editMenu.DropDownItems.Add(new ToolStripSeparator());
            editMenu.DropDownItems.Add(assetCutAction);
            editMenu.DropDownItems.Add(assetCopyAction);
            editMenu.DropDownItems.Add(assetPasteAction);
            editMenu.DropDownItems.Add(assetDeleteAction);
            editMenu.DropDownItems.Add(new ToolStripSeparator());
            editMenu.DropDownItems.Add(assetPreferencesAction);
        }
        private void CreateMainMenuView()
        {
            viewMenu = new ToolStripMenuItem("View");
        }
        private void CreateMainMenuAssets()
        {
            AssetsManager assetsManager = AssetsManager.GetInstance();
            assetFetchAction = new ToolStripMenuItem("Fetch Raw Assets");
            assetCompileAction = new ToolStripMenuItem("Compile Assets");
            assetSubBiomes = new ToolStripMenuItem("Biomes");
            assetSubBiomesAction = new List<ToolStripMenuItem>();
            foreach (string biome in assetsManager.Biomes())
            {
                ToolStripMenuItem biomeAction = new ToolStripMenuItem(biome);
                assetSubBiomesAction.Add(biomeAction);
                assetSubBiomes.DropDownItems.Add(biomeAction);
            }
            assetMenu = new ToolStripMenuItem("Asset");
            assetMenu.DropDownItems.Add(assetFetchAction);            
            assetMenu.DropDownItems.Add(assetCompileAction);
            assetMenu.DropDownItems.Add(assetSubBiomes);
        }
        private void CreateMainMenuMap()
        {
            mapMenu = new ToolStripMenuItem("Map");
        }
        private void CreateMainMenuHelp()
        {
            helpMenu = new ToolStripMenuItem("Help");
        }
        private void Action_New(object sender, EventArgs e)
        {
            //app.tabPage.CreateNewTabPage("New file");
        }
        private void Action_OpenProject(object sender, EventArgs e)
        {
            SaveManager saveManager = SaveManager.GetInstance();
            try
            {
                app.tabPage.CreateNewTabPage(saveManager.LoadProject(""));
            }
            catch (Exception){}
            //app.tabPageController.CreateNewTabPage(app.form, "New file");
        }
        private void Action_OpenMap(object sender, EventArgs e)
        {
            SaveManager saveManager = SaveManager.GetInstance();
            try
            {
                app.tabPage.CreateNewTabPage(saveManager.LoadProject(""));
            }
            catch (Exception) { }

            //app.tabPageController.CreateNewTabPage(app.form, "New file");
        }
        private void Action_FetchAssets(object sender, EventArgs e)
        {
            AssetsManager assetsManager = AssetsManager.GetInstance();
            try
            {
                assetsManager.FetchAssetsFromGame();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            //app.tabPageController.CreateNewTabPage(app.form, "New file");
        }
        private void Action_CompileResources(object sender, EventArgs e)
        {
            AssetsManager assetsManager = AssetsManager.GetInstance();
            try
            {
                assetsManager.CompileAssets();
                //FormManager.GetInstance().GetAssetsPanel().Refresh();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            //app.tabPageController.CreateNewTabPage(app.form, "New file");
        }
    }
}
