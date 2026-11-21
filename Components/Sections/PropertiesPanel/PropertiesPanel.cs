using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using Hades_Map_Editor.PropertiesSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;

namespace Hades_Map_Editor.PropertiesSection
{
    public class PropertiesPanel: SubPanel, IComponent, Focusable
    {
        private Label noSelectionLabel;
        private PropertiesTable propertiesListView;
        public PropertiesPanel(ProjectPage projectPage) : base(projectPage, "Properties")
        {
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public new void Initialize()
        {
            //BackColor = System.Drawing.Color.DarkGray;
            //AutoScroll = true;
            //AutoSize = true;
            Dock = DockStyle.Fill;
            Name = "Properties";

            noSelectionLabel = new Label();
            noSelectionLabel.Text = "No Selection";
            noSelectionLabel.AutoSize = true;
            noSelectionLabel.Left = (ClientSize.Width - noSelectionLabel.Width) / 2;
            noSelectionLabel.Top = (ClientSize.Height - noSelectionLabel.Height) / 2;
            //noSelectionLable.Dock = DockStyle.Cente;
            noSelectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            propertiesListView = new PropertiesTable(this);
            propertiesListView.Visible = true;

            ContentPanel.Controls.Add(propertiesListView);
            ContentPanel.Controls.Add(noSelectionLabel);
        }
        public new void Populate()
        {
            UnFocus();
            closeButton.Click += CloseButton_Click;
        }
        public void UnFocus()
        {
            noSelectionLabel.Visible = true;
            propertiesListView.Visible = false;
        }
        public void FocusOn(int obsID)
        {
            propertiesListView.FocusOn(obsID);

            noSelectionLabel.Visible = false;
            propertiesListView.Visible = true;

        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            parent.TogglePropertiesPanel();
        }
    }
}
