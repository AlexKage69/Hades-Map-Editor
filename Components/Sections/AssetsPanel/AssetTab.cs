using Hades_Map_Helper.Data;
using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.AssetsSection
{
    public class AssetTab : TabPage, IComponent, IPaging
    {
        private List<Asset> assets;
        private int currentPage;
        private int maxRow = 5, maxColumn = 7, wsize = 100, hsize = 120;
        public Dictionary<int, AssetPanel> assetsPanel;
        public PagingComponent paging;

        public AssetTab()
        {
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            currentPage = -1;
            assetsPanel = new Dictionary<int, AssetPanel>();
            
            for (int i = 0; i < maxRow * maxColumn; i++)
            {
                AssetPanel panel = new AssetPanel();
                assetsPanel.Add(i, panel);
                panel.Size = new System.Drawing.Size(wsize, hsize);
                panel.Location = new System.Drawing.Point((i % maxRow) * wsize, (i / maxRow) * hsize);
                panel.BackColor = (i % 2 == 0) ? System.Drawing.Color.CadetBlue : System.Drawing.Color.Cornsilk;
                Controls.Add(panel);
            }
            paging = new PagingComponent(this);
            Controls.Add(paging);
        }
        public void Populate()
        {
        }
        public void LoadItems(List<Asset> assets)
        {
            this.assets = assets;
            GoToPage(0);            
        }
        public void GoToPage(int pageNumber)
        {
            if (pageNumber == currentPage)
            {
                return;
            }
            currentPage = pageNumber;
            
            var pageCapacity = maxRow * maxColumn;
            for (int i = 0; i < assetsPanel.Count; i++) {
                var index = currentPage * pageCapacity + i;
                if (index< assets.Count-1)
                {
                    assetsPanel[i].Visible = true;
                    assetsPanel[i].SetImage(assets[index].name, assets[index].GetImage(wsize, wsize));
                }
                else
                {
                    assetsPanel[i].Visible = false;
                }
            }
            paging.GoToPage(currentPage, assets.Count/ pageCapacity);
        }

        public int GetCurrentPage()
        {
            return currentPage;
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
