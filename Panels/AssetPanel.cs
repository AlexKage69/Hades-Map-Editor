using Hades_Map_Helper.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    class AssetPanel
    {
        private AssetManager assetManager;
        private PanelManager panelManager;
        private Panel panel;
        private Dictionary<string, PictureBox> picBoxes;
        private string selectedId;
        public AssetPanel(PanelManager panelManager, AssetManager assetManager)
        {
            this.panelManager = panelManager;
            this.assetManager = assetManager;
            picBoxes = new Dictionary<string, PictureBox>();
            CreatePanel();
            FillAssets();
        }
        private void CreatePanel()
        {
            panel = new Panel();
            panel.Location = panelManager.GetLocation(PanelNames.AssetPanel);
            panel.Size = panelManager.GetSize(PanelNames.AssetPanel);
            panel.BackColor = System.Drawing.Color.White;
            panel.Parent = panelManager.GetForm();
            panel.AutoScroll = true;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.SetAutoScrollMargin(0, 0);
        }
        public void FillAssets()
        {
            var index = 0;
            int assetSize = (panel.Size.Width) / 3;
            foreach (string asset in assetManager.GetAllNames())
            {
                Console.WriteLine(asset);
                picBoxes.Add(asset, new PictureBox());
                picBoxes[asset].Name = asset;
                picBoxes[asset].Image = assetManager.GetImage(asset);
                picBoxes[asset].ClientSize = new Size(assetSize, assetSize);
                picBoxes[asset].SizeMode = PictureBoxSizeMode.StretchImage;
                picBoxes[asset].BorderStyle = BorderStyle.None;
                picBoxes[asset].Location = new System.Drawing.Point((index % 4) * assetSize, (index / 4) * assetSize);
                picBoxes[asset].Parent = panel;
                picBoxes[asset].MouseClick += new MouseEventHandler((o, a) => {
                    var selectedId = ((PictureBox)o).Name;
                    Console.WriteLine("Click:" + selectedId);
                    // Previous selected
                    if (this.selectedId != null)
                    {
                        picBoxes[this.selectedId].BorderStyle = BorderStyle.None;
                    }
                    // Next selected
                    this.selectedId = selectedId;
                    picBoxes[this.selectedId].BorderStyle = BorderStyle.FixedSingle;
                });
                index++;
            }
        }
    }
}
