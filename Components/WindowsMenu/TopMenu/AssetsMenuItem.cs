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
    public class AssetsMenuItem : ToolStripMenuItem, IComponent
    {
        public ToolStripMenuItem
            createElement, createEmptyElement,
            hades1asset, hades2asset,
            h1fetch, h1compile, h1biomes,
            h2fetch, h2compile, h2biomes;
        public AssetsMenuItem() : base("Assets")
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

            hades1asset = new ToolStripMenuItem("Hades Assets");
            h1fetch = new ToolStripMenuItem("Fetch Assets From Hades");
            h1compile = new ToolStripMenuItem("Compile Assets For Map");
            h1biomes = new ToolStripMenuItem("Biomes");

            hades2asset = new ToolStripMenuItem("Hades II Assets");
            h2fetch = new ToolStripMenuItem("Fetch Assets From Hades II");
            h2compile = new ToolStripMenuItem("Compile Assets For Map");
            h2biomes = new ToolStripMenuItem("Biomes");


            DropDownItems.Add(hades1asset);
            hades1asset.DropDownItems.Add(h1fetch);
            hades1asset.DropDownItems.Add(h1compile);
            hades1asset.DropDownItems.Add(h1biomes);

            DropDownItems.Add(hades2asset);
            hades2asset.DropDownItems.Add(h2fetch);
            hades2asset.DropDownItems.Add(h2compile);
            hades2asset.DropDownItems.Add(h2biomes);

            /*foreach (string biome in AssetsManager.GetInstance().Biomes())
            {
                biomes.DropDownItems.Add(new ToolStripMenuItem(biome));
            }*/
        }

        public void Populate()
        {
            DropDownOpening += Self_Open;
            h1fetch.Click += (s, e) => FetchHadesAssets_Action(s, e);
            h1compile.Click += (s, e) => CompileResources_Action(s, e);
            h1biomes.Enabled = false;
            h2fetch.Click += (s, e) => FetchHades2Assets_Action(s, e);
            //h2compile.Click += (s, e) => CompileResources_Action(s, e);
            h2compile.Enabled = false;
            h2biomes.Enabled = false;
        }

        private void Biome_Action(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FetchHadesAssets_Action(object sender, EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            try
            {
                formManager.StartLoading(new FetchAssetWorker(GameVersion.Hades));
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private void FetchHades2Assets_Action(object sender, EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            try
            {
                formManager.StartLoading(new FetchAssetWorker(GameVersion.Hades2));
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private void CompileResources_Action(object sender, EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            try
            {
                formManager.StartLoading(new CompileAssetWorker());
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
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
