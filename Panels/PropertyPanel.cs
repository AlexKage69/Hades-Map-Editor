using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    class PropertyPanel
    {
        private PanelManager panelManager;
        private MapManager elementManager;
        private Panel panel;
        private ThingTextProperties properties;
        private int selectedId;
        public PropertyPanel(PanelManager panelManager, MapManager elementManager)
        {
            this.panelManager = panelManager;
            this.elementManager = elementManager;
            CreatePanel();
            properties = new ThingTextProperties(this, panel);
        }
        private void CreatePanel()
        {
            panel = new Panel();
            panel.Location = panelManager.GetLocation(PanelNames.PropertyPanel);
            panel.Size = panelManager.GetSize(PanelNames.PropertyPanel);
            panel.BackColor = System.Drawing.Color.White;
            panel.Parent = panelManager.GetForm();
            panel.AutoScroll = true;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.SetAutoScrollMargin(0, 0);
        }
        public void Select(int selectedId)
        {
            properties.Load(elementManager.GetElement(selectedId).info);
            this.selectedId = selectedId;
        }
        public void Unload()
        {
            properties.Unload();
            this.selectedId = 0;
        }
        public void CheckActive(bool value)
        {
            if(selectedId != 0)
            {
                elementManager.GetElement(selectedId).info.Active = value;
            }
        }
    }
}
