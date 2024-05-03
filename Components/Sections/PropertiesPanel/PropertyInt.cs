using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.PropertiesSection
{
    public class PropertyInt: PropertyTextbox, IComponent
    {
        public PropertyInt(string label) : base(label)
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
        public void Update(int value)
        {
            base.Update(string.Format("{0:0}", value));
        }
    }
}
