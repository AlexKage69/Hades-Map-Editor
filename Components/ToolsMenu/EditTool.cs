using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.Components
{
    static class EditTool
    {
        public static ToolStripPanel Temporary()
        {
            // Create ToolStripPanel controls.
            ToolStripPanel tspTop = new ToolStripPanel();
            //ToolStripPanel tspBottom = new ToolStripPanel();
            //ToolStripPanel tspLeft = new ToolStripPanel();
            //ToolStripPanel tspRight = new ToolStripPanel();

            // Dock the ToolStripPanel controls to the edges of the form.
            tspTop.Dock = DockStyle.Bottom;

            // Create ToolStrip controls to move among the
            // ToolStripPanel controls.

            // Create the "Top" ToolStrip control and add
            // to the corresponding ToolStripPanel.
            ToolStrip tsTop = new ToolStrip();
            tsTop.Items.Add("Top");
            tspTop.Join(tsTop);
            // Add the ToolStripPanels to the form in reverse order.
            return tspTop;
        }
    }
}
