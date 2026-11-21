using Hades_Map_Editor.Data.Asset;
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
    public class AssetTable : Panel, IComponent, IPaging
    {
        private List<AssetData> assets;
        private int currentPage, maxPage;
        private int maxRow, maxColumn, wsize = 100, hsize = 118;
        public AssetItem[] assetsPanel;
        private AssetItem currentAsset;

        public AssetTable(AssetsPanel assetsPanel, int maxRow = 5, int maxColumn = 7)
        {
            Parent = assetsPanel;
            this.maxRow = maxRow;
            this.maxColumn = maxColumn;
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            currentPage = -1;
            //AutoSize = true;
            Dock = DockStyle.Fill;
            BackColor = Color.Aquamarine;
            assetsPanel = new AssetItem[maxRow * maxColumn];
            
            for (int i = 0; i < assetsPanel.Length; i++)
            {
                AssetItem panel = new AssetItem((AssetsPanel)Parent);
                assetsPanel[i] = panel;
                //panel.AutoSize = true;
                panel.Size = new System.Drawing.Size(wsize, hsize);
                panel.Location = new System.Drawing.Point((i % maxRow) * wsize, (i / maxRow) * hsize);
                panel.BackColor = (i % 2 == 0) ? System.Drawing.Color.CadetBlue : System.Drawing.Color.Cornsilk;
                Controls.Add(panel);
            }
        }
        public int GetMaxPage()
        {
            return maxPage;
        }
        public void Populate()
        {
        }
        public void LoadItems(List<AssetData> assets)
        {
            this.assets = assets;
            currentPage = -1;
            maxPage = (assetsPanel.Length / (maxRow * maxColumn));
            GoToPage(1);            
        }
        public void GoToPage(int pageNumber)
        {
            pageNumber = pageNumber-1;
            if (pageNumber == currentPage)
            {
                return;
            }
            currentPage = pageNumber;
            
            var pageCapacity = maxRow * maxColumn;
            for (int i = 0; i < assetsPanel.Length; i++) {
                var index = currentPage * pageCapacity + i;
                if (index< assets.Count-1)
                {
                    assetsPanel[i].Visible = true;
                    assetsPanel[i].SetImage(assets[index].name, assets[index].GetImage(new Size(wsize, wsize)));
                }
                else
                {
                    assetsPanel[i].Visible = false;
                }
            }
        }

        public int GetCurrentPage()
        {
            return currentPage;
        }
        public void SelectAsset(AssetItem selected)
        {
            if(currentAsset != null)
            {
                currentAsset.Unselect();
            }
            foreach (var panel in assetsPanel)
            {
                if(panel == selected){
                    currentAsset = panel;
                    break;
                }
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
