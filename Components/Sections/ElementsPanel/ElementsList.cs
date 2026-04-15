using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.ElementsSection
{
    public class ElementsList : ListBox, IComponent, Focusable
    {
        ProjectData projectData;
        public Dictionary<int, Obstacles> listBoxIndex;
        public ElementsList(ProjectData projectData)
        {
            this.projectData = projectData;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            listBoxIndex = new Dictionary<int, Obstacles>();
            Dock = DockStyle.Fill;
            MultiColumn = false;
            SelectionMode = SelectionMode.One;
            DrawMode = DrawMode.OwnerDrawFixed;
            DrawItem += new DrawItemEventHandler(ElementsList_DrawItem);
        }

        public void Populate()
        {
            SelectedIndexChanged += Action_SelectElement;
        }
        public void Action_SelectElement(object sender, EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            ListBox listBox = (ListBox)sender;
            Obstacles obs = listBoxIndex[listBox.SelectedIndex];
            Console.WriteLine(obs.Id);
            formManager.GetPropertiesPanel().FocusOn(obs.Id);
            formManager.GetMapPanel().FocusOn(obs.Id);
        }
        private void ElementsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            if(e.Index < 0)
            {
                return;
            }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          System.Drawing.Color.LightGray); // Choose the color.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.
            Obstacles obs = listBoxIndex[e.Index];

            if (!obs.HasAsset())
            {
                myBrush = Brushes.Orange;
            }
            else if (!obs.Active)
            {
                myBrush = Brushes.Red;
            }
            else if (obs.Active)
            {
                myBrush = Brushes.Green;
            }
            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(Items[e.Index].ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
        public void UnFocus()
        {
            ClearSelected();
        }
        public void FocusOn(int id)
        {
            foreach (var index in listBoxIndex)
            {
                if(id == index.Value.Id)
                {
                    SelectedIndex = index.Key;
                    break;
                }
            }
        }
    }
}
