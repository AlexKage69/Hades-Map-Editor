using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.AssetsSection
{
    public class AssetMenu : Panel, IDataFeed, IComponent
    {
        public ComboBox selectBiomeDropdown;
        Label selectBiomeLabel;
        Button addButton;
        AssetsPanel parent;
        public AssetMenu(AssetsPanel assetsPanel)
        {
            Parent = assetsPanel;
            parent = assetsPanel;
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            Height = 32;
            Dock = DockStyle.Top;

            selectBiomeLabel = new Label();
            selectBiomeLabel.Text = "Biome:";
            selectBiomeLabel.Dock = DockStyle.Left;
            selectBiomeLabel.AutoSize = true;

            selectBiomeDropdown = new ComboBox();
            selectBiomeDropdown.Size = new System.Drawing.Size(120, 32);
            selectBiomeDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            selectBiomeDropdown.TabIndex = 0;
            selectBiomeDropdown.Dock = DockStyle.Left;

            addButton = new FlatButton();
            addButton.Image = Configuration.GetIcon("add_button");
            addButton.Size = new Size(32, 32);
            addButton.Dock = DockStyle.Left;


            Controls.Add(addButton);
            Controls.Add(selectBiomeDropdown);
            Controls.Add(selectBiomeLabel);
        }
        public void Populate()
        { 
            selectBiomeDropdown.Enabled = false;
            addButton.Enabled = false;
            selectBiomeDropdown.SelectedIndexChanged += new System.EventHandler(SelectBiome_SelectedIndexChanged);
        }

        public void RefreshData()
        {
            selectBiomeDropdown.Items.Clear();
            AssetsManager am = AssetsManager.GetInstance();
            //var biomes = am.BiomesFiltered(AssetType.Tilesets);
            /*if (biomes.Count > 0)
            {
                selectBiomeDropdown.Enabled = true;
                selectBiomeDropdown.Items.AddRange(biomes.ToArray());
            }
            else
            {
                selectBiomeDropdown.Enabled = false;
            }*/
            selectBiomeDropdown.Enabled = false;
        }

        private void SelectBiome_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string biome = selectBiomeDropdown.SelectedItem.ToString();
            parent.ShowAssetTable(biome);
        }
    }
}
