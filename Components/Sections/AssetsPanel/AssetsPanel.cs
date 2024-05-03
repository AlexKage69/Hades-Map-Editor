using Hades_Map_Editor.AssetsSection;
using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Sections
{
    public class AssetsPanel : Panel, IComponent
    {
        private Label notLoadedLable;
        public Dictionary<string, AssetTab> assetsTab;
        public TabControl assetsTabControl;

        public AssetsPanel()
        {
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            BackColor = System.Drawing.Color.White;
            AutoScroll = true;
            Dock = DockStyle.Fill;

            notLoadedLable = new Label();
            notLoadedLable.Text = "Loading...";
            notLoadedLable.AutoSize = false;
            notLoadedLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            notLoadedLable.Dock = DockStyle.Fill;
            notLoadedLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Controls.Add(notLoadedLable);

            assetsTabControl = new TabControl();
            assetsTabControl.Dock = DockStyle.Fill;
            Controls.Add(assetsTabControl);
            //BorderStyle = BorderStyle.FixedSingle;
            //SetAutoScrollMargin(0, 0);
        }
        public void Populate()
        {
            AssetsManager assetsManager = AssetsManager.GetInstance();
            Dictionary<string, Dictionary<AssetType, Dictionary<string, Asset>>> biomes = assetsManager.GetAssets();
            if(biomes != null && biomes.Count != 0)
            {
                notLoadedLable.Visible = false;
                foreach (var biome in biomes)
                {
                    if (biome.Value.ContainsKey(AssetType.Tilesets)) {
                        AssetTab assetTab = new AssetTab();
                        assetTab.Text = biome.Key;
                        assetTab.LoadItems(biome.Value[AssetType.Tilesets].Values.ToList());

                        assetsTabControl.Controls.Add(assetTab);
                    }
                }                
            }
            else
            {
                notLoadedLable.Visible = true;
                notLoadedLable.Text = "Failed to load assets.";
            }
        }
        /*public void Select(int selectedId)
        {
            properties.Load(elementManager.GetElement(selectedId).info);
            this.selectedId = selectedId;
        }*/
        /*public void CheckActive(bool value)
        {
            if(selectedId != 0)
            {
                elementManager.GetElement(selectedId).info.Active = value;
            }
        }*/
    }
}
