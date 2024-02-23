using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    class ElementPanel
    {
        private PanelManager panelManager;
        private MapManager elementManager;
        private Panel panel;
        private Dictionary<int, Label> textboxes;
        private int selectedId;
        public ElementPanel(PanelManager panelManager, MapManager elementManager)
        {
            this.textboxes = new Dictionary<int, Label>();
            this.panelManager = panelManager;
            this.elementManager = elementManager;
            CreatePanel();
            FillElements();
        }
        private void CreatePanel()
        {
            panel = new Panel();
            panel.Location = panelManager.GetLocation(PanelNames.ElementPanel);
            panel.Size = panelManager.GetSize(PanelNames.ElementPanel);
            panel.BackColor = System.Drawing.Color.White;
            panel.Parent = panelManager.GetForm();
            panel.AutoScroll = true;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.SetAutoScrollMargin(0, 0);
        }
        private void FillElements()
        {
            int index = 0;
            int textSize = 14;
            foreach (int id in elementManager.GetAllIds())
            {
                var element = elementManager.GetElement(id);
                //Console.WriteLine(element.Name);
                string fullText = element.info.Id + ":" + element.info.Name;
                textboxes.Add(id, new Label());
                textboxes[id].Text = fullText;
                textboxes[id].Location = new System.Drawing.Point(0, index * textSize);
                textboxes[id].AutoSize = true;
                textboxes[id].Parent = panel;
                textboxes[id].ForeColor = System.Drawing.Color.Black;
                textboxes[id].BackColor = System.Drawing.Color.Transparent;
                textboxes[id].MouseClick += new MouseEventHandler((o, e) =>
                {
                    Label label = (Label)o;
                    int selectedId = int.Parse(label.Text.Split(':')[0]);
                    Console.WriteLine("Selected:" + label.Text + ";;;" + selectedId);
                    //Focus the map
                    panelManager.GetForm().Select(selectedId);

                });
                index++;
            }
            Console.WriteLine("Counted assets:" + index);
        }
        public void Select(int selectedId)
        {
            //Previous reset
            Unload();
            //Change next one
            this.selectedId = selectedId;
            textboxes[this.selectedId].ForeColor = System.Drawing.Color.White;
            textboxes[this.selectedId].BackColor = System.Drawing.Color.Black;

        }
        public void Unload()
        {
            if (selectedId != 0)
            {
                textboxes[selectedId].ForeColor = System.Drawing.Color.Black;
                textboxes[selectedId].BackColor = System.Drawing.Color.Transparent;
                selectedId = 0;
            }
        }
    }
}
