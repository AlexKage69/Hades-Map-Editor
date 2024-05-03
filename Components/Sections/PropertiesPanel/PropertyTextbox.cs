using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.PropertiesSection
{
    public class PropertyTextbox : PropertyItem<string>, IComponent
    {
        protected TextBox textBox;
        public PropertyTextbox(string label) : base(label)
        {
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public new void Initialize()
        {

            textBox = new TextBox();
            textBox.Dock = DockStyle.Right;
            Leave += new EventHandler(Property_Leave);
            Controls.Add(textBox);

            //BorderStyle = BorderStyle.FixedSingle;
            //SetAutoScrollMargin(0, 0);
        }
        public new void Populate()
        {
        }
        public override void Update(string value)
        {
            textBox.Text = value;
        }
        private void Property_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("1:"+Text);
            try
            {
                // Convert the text to a Double and determine if it is a negative number.
                if (double.Parse(Text) < 0)
                {
                    // If the number is negative, display it in Red.
                    ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    // If the number is not negative, display it in Black.
                    ForeColor = System.Drawing.Color.Black;
                }
            }
            catch
            {
                // If there is an error, display the text using the system colors.
                ForeColor = System.Drawing.Color.White;
            }
        }
    }
}
