using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.AssetsSection
{
    public class AssetPanel : Panel, IComponent
    {
        PictureBox picture;
        Label name;
        public AssetPanel()
        {
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            name = new Label();
            name.Location = new System.Drawing.Point(0,100);
            picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.AutoSize;

            Controls.Add(name);
            Controls.Add(picture);
        }
        public void Populate()
        {
        }
        public void SetImage(string name, Image image)
        {
            this.name.Text = name;
            picture.Image = image;
            picture.Refresh();
        }
        /*public void Select(int selectedId)
        {
            properties.Load(elementManager.GetElement(selectedId).info);
            this.selectedId = selectedId;
        }*/
        /*public void CheckActive(bool value)
        {
            if(selectedId != 0)
            {
                elementManager.GetElement(selectedId).info.Active = value;
            }
        }*/
    }
}
