using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.ElementsSection
{
    public class ElementsMenu : Panel, IDataFeed, IComponent
    {
        CheckBox hideUnassigned;
        ElementsPanel parent;
        public ElementsMenu(ElementsPanel elementsPanel)
        {
            Parent = elementsPanel;
            parent = elementsPanel;
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            Height = 32;
            Dock = DockStyle.Top;

            hideUnassigned = new CheckBox();
            hideUnassigned.Text = "Hide Unassigned Asset:";
            hideUnassigned.Dock = DockStyle.Left;
            hideUnassigned.AutoSize = true;


            Controls.Add(hideUnassigned);
        }
        public void Populate()
        {
            hideUnassigned.CheckedChanged += new System.EventHandler(SelectBiome_SelectedIndexChanged);
        }

        public void LoadConfiguration()
        {
        }

        private void SelectBiome_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Console.WriteLine(hideUnassigned.Checked);
            parent.listBox.filterUnassignedAsset = hideUnassigned.Checked;
            parent.Refresh();
        }

        public void RefreshData()
        {
            hideUnassigned.Checked = false;
        }
    }
}
