using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Components
{
    public class BottomMenuStrip: StatusStrip, IComponent
    {
        public BottomMenuStrip()
        {
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            Dock = DockStyle.Bottom;
            ToolStripStatusLabel toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel()
            {
                Name = "Biome",
                Text = "Tartarus",
            };
            Items.Add(toolStripStatusLabel1);
        }

        public void Populate()
        {

        }

    }
}
