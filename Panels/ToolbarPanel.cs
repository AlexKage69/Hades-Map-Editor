using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    class ToolbarPanel
    {
        private PanelManager panelManager;
        private Panel panel;
        public ToolbarPanel(PanelManager panelManager)
        {
            this.panelManager = panelManager;
            CreatePanel();
        }
        private void CreatePanel()
        {
            panel = new Panel();
            panel.Location = panelManager.GetLocation(PanelNames.ToolbarPanel);
            panel.Size = panelManager.GetSize(PanelNames.ToolbarPanel);
            panel.BackColor = System.Drawing.Color.White;
            panel.Parent = panelManager.GetForm();
            panel.AutoScroll = true;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.SetAutoScrollMargin(0, 0);
        }
    }
}
