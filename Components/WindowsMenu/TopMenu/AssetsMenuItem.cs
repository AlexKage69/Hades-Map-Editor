using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Components
{
    public class AssetsMenuItem : ToolStripMenuItem, IComponent
    {
        public ToolStripMenuItem
            obstacles, createObstacle, createElement, createEmptyElement,
            fetch, compile, biomes;
        HadesMapEditor app;
        public AssetsMenuItem(HadesMapEditor app) : base("Assets")
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

            obstacles = new ToolStripMenuItem("Obstacles");
            createObstacle = new ToolStripMenuItem("Create Obstacle");
            fetch = new ToolStripMenuItem("Fetch Assets From Hades");
            compile = new ToolStripMenuItem("Compile Assets For Map");
            biomes = new ToolStripMenuItem("Biomes");

            DropDownItems.Add(obstacles);
            obstacles.DropDownItems.Add(createObstacle);
            DropDownItems.Add(fetch);
            DropDownItems.Add(compile);
            DropDownItems.Add(biomes);
            foreach (string biome in AssetsManager.GetInstance().Biomes())
            {
                biomes.DropDownItems.Add(new ToolStripMenuItem(biome));
            }
        }

        public void Populate()
        {
            DropDownOpening += Self_Open;
            fetch.Click += (s, e) => FetchAssets_Action(s, e);
            compile.Click += (s, e) => CompileResources_Action(s, e);
            if (biomes.DropDownItems.Count > 0)
            {
                foreach (ToolStripMenuItem biome in biomes.DropDownItems)
                {
                    biome.Click += Biome_Action;
                }
            }
            else
            {
                biomes.Enabled = false;
            }
        }

        private void Biome_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FetchAssets_Action(object sender, EventArgs e)
        {
            AssetsManager assetsManager = AssetsManager.GetInstance();
            try
            {
                assetsManager.FetchAssetsFromGameAsync();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            //app.tabPageController.CreateNewTabPage(app.form, "New file");
        }
        private void CompileResources_Action(object sender, EventArgs e)
        {
            AssetsManager assetsManager = AssetsManager.GetInstance();
            FormManager formManager = FormManager.GetInstance();
            try
            {
                Task compileTask = assetsManager.CompileAssets();
                compileTask.ContinueWith(task =>
                {
                    if (formManager.HasTabOpen())
                    {
                        //formManager.GetAssetsPanel().RefreshData();
                        //formManager.GetElementsPanel().RefreshData();
                    }
                });
                //FormManager.GetInstance().GetAssetsPanel().Refresh();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            //app.tabPageController.CreateNewTabPage(app.form, "New file");
        }
        private void Self_Open(object sender, EventArgs e)
        {
            //Console.WriteLine("Map Clicked");
            //FormManager formManager = FormManager.GetInstance();
            //bool hasMapOpen = formManager.HasTabOpen();
            //createElement.Enabled = hasMapOpen
            //createEmptyElement.Enabled = hasMapOpen;
        }
    }
}
