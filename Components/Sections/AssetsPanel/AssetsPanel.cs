using Hades_Map_Editor.AssetsSection;
using Hades_Map_Editor.Data.Asset;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.AssetsSection
{
    public class AssetsPanel : SubPanel, IDataFeed, IComponent
    {
        EmptyAssetPanel emptyAssetPanel;
        AssetMenu assetMenu;
        AssetTable assetTable;
        PagingComponent paging;
        private int maxRow = 5, maxColumn = 7, wsize = 100, hsize = 120;

        public AssetsPanel(ProjectPage projectPage):base(projectPage,"Hades Assets")
        {
            Initialize();
            Populate();
        }
        public new void Initialize()
        {
            
            assetMenu = new AssetMenu(this);
            assetTable = new AssetTable(this);
            emptyAssetPanel = new EmptyAssetPanel(this);
            paging = new PagingComponent(assetTable);

            BottomPanel.Controls.Add(paging);
            TopPanel.Controls.Add(assetMenu);
            ContentPanel.Controls.Add(emptyAssetPanel);
            ContentPanel.Controls.Add(assetTable);
        }
        public new void Populate()
        {
            RefreshData();
            closeButton.Click += CloseButton_Click;
            assetTable.Visible = false;

            /*if(assetMenu.selectBiomeDropdown.Items.Count > 0)
            {
                ShowAssetTable(assetMenu.selectBiomeDropdown.Items[0].ToString());
            }*/
        }

        public void RefreshData()
        {
            AssetsManager am = AssetsManager.GetInstance();
            /*if (am.HasAssets())
            {
                emptyAssetPanel.Visible = false;
            }
            else
            {
                emptyAssetPanel.Visible = true;
            }*/
            emptyAssetPanel.Visible = false;
            assetMenu.RefreshData();
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            parent.ToggleAssetsPanel();
        }
        public void ShowAssetTable(string biomeName)
        {
            if(assetMenu.selectBiomeDropdown.SelectedItem == null || assetMenu.selectBiomeDropdown.SelectedItem.ToString() != biomeName)
            {
                assetMenu.selectBiomeDropdown.SelectedItem = biomeName;
            }
            AssetsManager am = AssetsManager.GetInstance();
            List<AssetData> list = am.GetBiomeAssets(biomeName).GetAssetsByType(AssetType.Tilesets);
            paging.SetupPaging((int)Math.Ceiling((double)list.Count / (maxRow * maxColumn)));
            assetTable.LoadItems(list);
            assetTable.Visible = true;
            emptyAssetPanel.Visible = false;
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
