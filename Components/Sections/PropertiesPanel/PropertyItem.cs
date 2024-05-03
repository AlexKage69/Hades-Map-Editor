using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.PropertiesSection
{
    public abstract class PropertyItem<K>: TableLayoutPanel, IComponent
    {
        string title;
        Label nameLabel;
        public PropertyItem(string label)
        {
            title = label;
            Initialize();
            Populate();
            //Children needs to call Initialize() and Populate();
        }
        public void Initialize()
        {
            BackColor = System.Drawing.Color.Gray;
            AutoScroll = false;
            Dock = DockStyle.Top;
            AutoSize = true;
            //Size = new System.Drawing.Size(100,100);
            

            nameLabel = new Label();
            nameLabel.Text = title;
            nameLabel.AutoSize = false;
            nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            nameLabel.BackColor = System.Drawing.Color.BlueViolet;
            nameLabel.Size = new System.Drawing.Size(180,25);
            nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Controls.Add(nameLabel);
        }
        public void Populate()
        {

        }
        public abstract void Update(K value);
    }
}
