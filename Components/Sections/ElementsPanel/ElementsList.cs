using Hades_Map_Helper.Data;
using Hades_Map_Helper.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.ElementsSection
{
    public class ElementsList : ListBox, IComponent, Focusable
    {
        ProjectData projectData;
        public Dictionary<int, Obstacle> listBoxIndex;
        public ElementsList(ProjectData projectData)
        {
            this.projectData = projectData;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            listBoxIndex = new Dictionary<int, Obstacle>();
            Dock = DockStyle.Fill;
            MultiColumn = false;
            SelectionMode = SelectionMode.One;
            DrawMode = DrawMode.OwnerDrawFixed;
            DrawItem += new DrawItemEventHandler(ElementsList_DrawItem);
        }

        public void Populate()
        {
            BeginUpdate();
            foreach (var obstacle in projectData.mapData.GetActiveObstacles())
            {
                listBoxIndex.Add(Items.Add(obstacle.Id + ":" + obstacle.Name), obstacle);
            }
            EndUpdate();
            SelectedIndexChanged += Action_SelectElement;
        }
        public void Action_SelectElement(object sender, EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            ListBox listBox = (ListBox)sender;
            Obstacle obs = listBoxIndex[listBox.SelectedIndex];
            Console.WriteLine(obs.Id);
            formManager.GetPropertiesPanel().FocusOn(obs);
            formManager.GetMapPanel().FocusOn(obs);
        }
        private void ElementsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            if(e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.
            Obstacle obs = listBoxIndex[e.Index];

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
        public void FocusOn(Obstacle obs)
        {
            foreach (var index in listBoxIndex)
            {
                if(obs.Id == index.Value.Id)
                {
                    SelectedIndex = index.Key;
                    break;
                }
            }
        }
    }
}
