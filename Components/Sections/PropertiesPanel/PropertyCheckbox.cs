using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.PropertiesSection
{
    public class PropertyCheckbox: PropertyItem<bool>, IComponent
    {
        CheckBox checkBox;
        public PropertyCheckbox(string label) : base(label)
        {
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public new void Initialize()
        {
            checkBox = new CheckBox();
            checkBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            checkBox.Dock = DockStyle.Right;
            Controls.Add(checkBox);
        }
        public new void Populate()
        {
        }
        public override void Update(bool value)
        {
            checkBox.Checked = value;
        }
    }
}
