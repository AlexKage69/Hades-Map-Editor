using Hades_Map_Editor.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.ElementsSection
{
    public class ElementsPanel : SubPanel, IComponent, IDataFeed, Focusable
    {
        public ElementsList listBox;
        public Dictionary<int, int> listBoxIndex;
        public ElementsMenu headerPanel;

        public ElementsPanel(ProjectPage parent): base(parent,"Map Elements")
        {
            Parent = parent;
            Initialize();
            Populate();
        }
        public new void Initialize()
        {
            listBoxIndex = new Dictionary<int, int>();
            listBox = new ElementsList(this);
            headerPanel = new ElementsMenu(this);

            TopPanel.Controls.Add(headerPanel);
            ContentPanel.Controls.Add(listBox);
        }

        public new void Populate()
        {
            RefreshData();
            FilterList(false,true);
            closeButton.Click += CloseButton_Click;
        }

        public void UnFocus()
        {
            listBox.UnFocus();
        }
        public void FocusOn(int id)
        {
            listBox.FocusOn(id);
        }

        public void RefreshData()
        {
            data = GetData();
        }
        public void FilterList(bool hideAssets = false, bool hideUnassigned = false)
        {
            listBox.Items.Clear();
            //WRITING A FILE OR SOME SUCH THINGAMAGIG
            listBox.BeginUpdate();
            foreach (var obstacle in data.mapData.GetAllObstacles())
            {
                listBox.listBoxIndex.Add(listBox.Items.Add(obstacle.Id + ":" + obstacle.Name), obstacle);
            }
            listBox.EndUpdate();
            //_ = GetData();

        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            parent.ToggleElementsPanel();
        }
    }
}
