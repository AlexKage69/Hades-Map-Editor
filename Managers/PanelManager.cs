using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.Managers
{
    public class PanelManager
    {
        private Dictionary<PanelNames,Rect> panels;
        private HadesMapHelperForm form;
        public PanelManager(HadesMapHelperForm form)
        {
            this.form = form;
            panels = new Dictionary<PanelNames, Rect>();
            float firstHeight = 0.07f, secondHeight = 0.4f, firstWidth = 0.2f, secondWidth = 0.3f;
            panels.Add(PanelNames.ToolbarPanel, new Rect(0, 0, GetWidth(1f) - 10, GetHeight(firstHeight)));
            panels.Add(PanelNames.PropertyPanel, new Rect(0, GetHeight(firstHeight), GetWidth(firstWidth), GetHeight(1f- firstHeight- secondHeight)));
            panels.Add(PanelNames.ElementPanel, new Rect(0, GetHeight(1f-secondHeight), GetWidth(firstWidth), GetHeight(secondHeight) - 50));
            panels.Add(PanelNames.AssetPanel, new Rect(GetWidth(1f- secondWidth), GetHeight(firstHeight), GetWidth(secondWidth) - 10, GetHeight(1f - firstHeight) - 50));
            panels.Add(PanelNames.MapPanel, new Rect(GetWidth(firstWidth), GetHeight(firstHeight), GetWidth(1f - firstWidth - secondWidth), GetHeight(1f - firstHeight)-50));
        }
        public System.Drawing.Point GetLocation(PanelNames panel)
        {
            return new System.Drawing.Point(panels[panel].x, panels[panel].y);
        }
        public Size GetSize(PanelNames panel)
        {
            return new Size(panels[panel].width, panels[panel].height);
        }
        public HadesMapHelperForm GetForm()
        {
            return form;
        }
        private int GetWidth(float percent)
        {
            return (int)(form.oldSize.Width * percent);
        }
        private int GetHeight(float percent)
        {
            return (int)(form.oldSize.Height * percent);
        }
    }
    public enum PanelNames
    {
        AssetPanel,
        ElementPanel,
        MapPanel,
        PropertyPanel,
        ToolbarPanel
    }

}
