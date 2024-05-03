using Hades_Map_Helper.Data;
using Hades_Map_Helper.ElementsSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.Sections
{
    public class ElementsPanel : Panel, IComponent
    {
        ProjectData data;
        public ElementsList listBox;
        public Dictionary<int, int> listBoxIndex;
        public ElementsPanel(ProjectData projectData)
        {
            data = projectData;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            listBoxIndex = new Dictionary<int, int>();
            Dock = DockStyle.Fill;
            listBox = new ElementsList(data);
            
            Controls.Add(listBox);
        }

        public void Populate()
        {
        }        
    }
}
