using Hades_Map_Editor.Data.Map;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IronPython.Runtime.Profiler;

namespace Hades_Map_Editor.ElementsSection
{
    public class ElementsList : ListBox, IComponent, Focusable
    {
        public Dictionary<int, Obstacle> listBoxIndex;
        public ElementsPanel parent;
        public bool filterUnassignedAsset;
        public ElementsList(ElementsPanel parent)
        {
            this.parent = parent;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            filterUnassignedAsset = false;
            listBoxIndex = new Dictionary<int, Obstacle>();
            Dock = DockStyle.Fill;
            MultiColumn = false;
            SelectionMode = SelectionMode.One;
            DrawMode = DrawMode.OwnerDrawVariable;
        }

        public void Populate()
        {
            DrawItem += new DrawItemEventHandler(ElementsList_DrawItem);
            MeasureItem += new MeasureItemEventHandler(ElementsList_MeasureItem);
            SelectedIndexChanged += Action_SelectElement;
        }
        public void Action_SelectElement(object sender, EventArgs e)
        {
            if(SelectedIndex < 0)
            {
                return;
            }
            string message = (string)((ListBox)sender).SelectedItem;
            int itemHeight = GetItemHeight(SelectedIndex);
            var pp = parent.GetProjectPage();
            if (message == null || itemHeight <= 0)
            {
                UnFocus();
                pp.propertiesPanel.UnFocus();
                pp.mapPanel.UnFocus();
                return;
            }
            int id = int.Parse(message.Split(':')[0]);
            //Console.WriteLine(id);
            pp.propertiesPanel.FocusOn(id);
            pp.mapPanel.FocusOn(id);
        }  
        
        private void ElementsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            if(e.Index < 0 || e.Bounds.Height <= 0)
            {
                return;
            }
            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.
            Obstacle obs = listBoxIndex[e.Index];
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;
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
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          System.Drawing.Color.LightGray); // Choose the color.
                e.DrawBackground();

                // Draw the current item text based on the current Font 
                // and the custom brush settings.
                e.Graphics.DrawString(Items[e.Index].ToString(),
                    e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                // If the ListBox has focus, draw a focus rectangle around the selected item.
                e.DrawFocusRectangle();
        }
        private void ElementsList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            Obstacle obs = listBoxIndex[e.Index];
            e.ItemHeight = 13;
            if (!obs.HasAsset() && filterUnassignedAsset)
            {
                e.ItemHeight = 0;
            }
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
