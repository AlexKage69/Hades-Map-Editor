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
    public class ViewMenuItem : ToolStripMenuItem, IComponent
    {
        public ToolStripMenuItem
            viewsAndToolbars, showAssetWindow, showElementWindow, showPropertyWindow, resetViews,
            viewAssetsType, showFx, showTilesets, showUnknowns, resetAssetsType,
            viewFilters, showParallax, showObstacles, showUnits, resetFilters,
            showWater, showUnloadedAssets, showGrid,
            zoomIn, zoomOut, normalSize,
            nextTab, previousTab,
            fullscreen;
        public List<ToolStripMenuItem> filesRecentAction;
        public ViewMenuItem() : base("View")
        {
            Initialize();
            Populate();
            Dock = DockStyle.Top;
        }
        public void Initialize()
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            ((ToolStripDropDownMenu)(DropDown)).ShowImageMargin = true;
            ((ToolStripDropDownMenu)(DropDown)).ShowCheckMargin = true;

            viewsAndToolbars = new ToolStripMenuItem("Views and Toolbars");
            showAssetWindow = new ToolStripMenuItem("Show Assets Window");
            showElementWindow = new ToolStripMenuItem("Show Elements Window");
            showPropertyWindow = new ToolStripMenuItem("Show Property Window");
            resetViews = new ToolStripMenuItem("Reset Windows Layout");
            showAssetWindow.CheckOnClick = true;
            showElementWindow.CheckOnClick = true;
            showPropertyWindow.CheckOnClick = true;

            viewAssetsType = new ToolStripMenuItem("View Assets Type");
            showFx = new ToolStripMenuItem("Show Fx Type");
            showTilesets = new ToolStripMenuItem("Show Tilesets Type");
            showUnknowns = new ToolStripMenuItem("Show Unknowns Type");
            resetAssetsType = new ToolStripMenuItem("Enable All Types");
            showFx.CheckOnClick = true;
            showTilesets.CheckOnClick = true;
            showUnknowns.CheckOnClick = true;

            viewFilters = new ToolStripMenuItem("Filter Content");
            showParallax = new ToolStripMenuItem("Show Parallax Elements");
            showObstacles = new ToolStripMenuItem("Show Obstacles");
            showUnits = new ToolStripMenuItem("Show Units");
            resetFilters = new ToolStripMenuItem("Reset to Default Filters");
            showParallax.CheckOnClick = true;
            showObstacles.CheckOnClick = true;
            showUnits.CheckOnClick = true;

            showWater = new ToolStripMenuItem("Show Water");
            showUnloadedAssets = new ToolStripMenuItem("Show Unloaded Assets");
            showGrid = new ToolStripMenuItem("Add Grid Reference");
            showWater.CheckOnClick = true;
            showUnloadedAssets.CheckOnClick = true;

            zoomIn = new ToolStripMenuItem("Zoom In");
            zoomOut = new ToolStripMenuItem("Zoom Out");
            normalSize = new ToolStripMenuItem("Normal Size");

            nextTab = new ToolStripMenuItem("Next Tab");
            previousTab = new ToolStripMenuItem("Previous Tab");

            fullscreen = new ToolStripMenuItem("Fullscreen");

            DropDownItems.Add(viewsAndToolbars);
            viewsAndToolbars.DropDownItems.Add(showAssetWindow);
            viewsAndToolbars.DropDownItems.Add(showElementWindow);
            viewsAndToolbars.DropDownItems.Add(showPropertyWindow);
            viewsAndToolbars.DropDownItems.Add(new ToolStripSeparator());
            viewsAndToolbars.DropDownItems.Add(resetViews);

            DropDownItems.Add(viewAssetsType);
            viewAssetsType.DropDownItems.Add(showFx);
            viewAssetsType.DropDownItems.Add(showTilesets);
            viewAssetsType.DropDownItems.Add(showUnknowns);
            viewAssetsType.DropDownItems.Add(new ToolStripSeparator());
            viewAssetsType.DropDownItems.Add(resetAssetsType);

            DropDownItems.Add(viewFilters);
            viewFilters.DropDownItems.Add(showParallax);
            viewFilters.DropDownItems.Add(showObstacles);
            viewFilters.DropDownItems.Add(showUnits);
            viewFilters.DropDownItems.Add(new ToolStripSeparator());
            viewFilters.DropDownItems.Add(resetFilters);

            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(showWater);
            DropDownItems.Add(showUnloadedAssets);
            DropDownItems.Add(showGrid);
            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(zoomIn);
            DropDownItems.Add(zoomOut);
            DropDownItems.Add(normalSize);
            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(nextTab);
            DropDownItems.Add(previousTab);
            DropDownItems.Add(new ToolStripSeparator());
            DropDownItems.Add(fullscreen);
        }

        public void Populate()
        {
            DropDownOpening += Self_Open;
            showAssetWindow.Click += ShowAssetWindow_Click;
            showElementWindow.Click += ShowElementWindow_Click;
            showPropertyWindow.Click += ShowPropertyWindow_Click;
            //resetViews.Click += ResetViews_Click;
            showFx.Click += ResetViews_Click;
            showTilesets.Click += Example_Action;
            showTilesets.Checked = true;
            showTilesets.Enabled = false;
            showUnknowns.Click += Example_Action;
            showUnknowns.Checked = true;
            showUnknowns.Enabled = false;
            resetAssetsType.Click += Example_Action;
            resetAssetsType.Enabled = false;
            showParallax.Click += Example_Action;
            showParallax.Checked = true;
            showParallax.Enabled = false;
            showObstacles.Click += Example_Action;
            showObstacles.Checked = true;
            showObstacles.Enabled = false;
            showUnits.Click += Example_Action;
            showUnits.Checked = true;
            showUnits.Enabled = false;
            resetFilters.Click += Example_Action;
            resetFilters.Enabled = false;
            showWater.Click += Example_Action;
            showWater.Checked = true;
            showWater.Enabled = false;
            showUnloadedAssets.Click += Example_Action;
            showUnloadedAssets.Checked = true;
            showUnloadedAssets.Enabled = false;
            showGrid.Click += Example_Action;
            showGrid.Enabled = false;
            zoomIn.Click += Example_Action;
            zoomIn.Enabled = false;
            zoomOut.Click += Example_Action;
            zoomOut.Enabled = false;
            normalSize.Click += Example_Action;
            normalSize.Enabled = false;
            nextTab.Click += Example_Action;
            nextTab.Enabled = false;
            previousTab.Click += Example_Action;
            previousTab.Enabled = false;
            fullscreen.Click += Example_Action;
            fullscreen.Enabled = false;
        }

        private void ResetViews_Click(object sender, EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
        }

        private void ShowAssetWindow_Click(object sender, System.EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            ProjectPage pp = formManager.GetCurrentTab();
            if(pp == null)
            {
                return;
            }
            pp.ToggleAssetsPanel();
            showAssetWindow.Checked = pp.IsAssetsPanelOpen();
        }

        private void ShowElementWindow_Click(object sender, System.EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            ProjectPage pp = formManager.GetCurrentTab();
            pp.ToggleElementsPanel();
            if (pp == null)
            {
                return;
            }
            showAssetWindow.Checked = pp.IsElementsPanelOpen();
        }

        private void ShowPropertyWindow_Click(object sender, System.EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            ProjectPage pp = formManager.GetCurrentTab();
            if (pp == null)
            {
                return;
            }
            pp.TogglePropertiesPanel();
            showAssetWindow.Checked = pp.IsPropertiesPanelOpen();
        }

        private void Example_Action(object sender, System.EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Console.WriteLine("Click:"+ item.Name);
        }
        private void Self_Open(object sender, EventArgs e)
        {
            Console.WriteLine("Map Clicked");
            FormManager formManager = FormManager.GetInstance();
            ProjectPage pp = formManager.GetCurrentTab();
            if (pp == null)
            {
                return;
            }
            showAssetWindow.Checked = pp.IsAssetsPanelOpen();
            showElementWindow.Checked = pp.IsElementsPanelOpen();
            showPropertyWindow.Checked = pp.IsPropertiesPanelOpen();
            //loadMapText.Enabled = hasMapOpen;
            //metadataView.Enabled = hasMapOpen;
        }
    }
}
