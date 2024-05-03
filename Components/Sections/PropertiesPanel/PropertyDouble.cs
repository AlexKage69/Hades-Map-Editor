using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.PropertiesSection
{
    public class PropertyDouble : PropertyTextbox, IComponent
    {
        public PropertyDouble(string label) : base(label)
        {
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public new void Initialize()
        {
        }
        public new void Populate()
        {
        }
        public void Update(double value)
        {
            base.Update(string.Format("{0:0.00}", value));
        }
    }
}
