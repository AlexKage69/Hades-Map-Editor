using Hades_Map_Editor.Data.Project;
using Hades_Map_Editor.PropertiesSection;
using Hades_Map_Editor.AssetsSection;
using Hades_Map_Editor.ElementsSection;
using Hades_Map_Editor.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor
{
    public class ProjectPage : TabPage, IComponent
    {
        ProjectData data;
        ToolStripContainer toolStripContainer;
        CustomSplitContainer mainSplitContainer, leftSplitContainer, rightSplitContainer;
        public PropertiesPanel propertiesPanel;
        public AssetsPanel assetsPanel;
        public ElementsPanel elementsPanel;
        public MapPanel mapPanel;
        public bool filterUnassigned = false;
        public ProjectPage(ProjectData data)
        {
            this.data = data;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            CreateSplitContainers();
            CreatePropertiesPanel();
            CreateElementsPanel();
            CreateMapPanel();
            CreateAssetsPanel();
        }

        public void Populate()
        {
        }
        public ProjectData GetData() { return data; }
        private void CreateSplitContainers()
        {
            toolStripContainer = new ToolStripContainer();
            mainSplitContainer = new CustomSplitContainer();
            leftSplitContainer = new CustomSplitContainer();
            rightSplitContainer = new CustomSplitContainer();

            mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            mainSplitContainer.SplitterDistance = 25;
            mainSplitContainer.SplitterWidth = 3;
            leftSplitContainer.BackColor = System.Drawing.Color.Beige;
            leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            leftSplitContainer.SplitterWidth = 3;
            leftSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            rightSplitContainer.BackColor = System.Drawing.Color.AntiqueWhite;
            rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            rightSplitContainer.SplitterDistance = 100;
            rightSplitContainer.SplitterWidth = 3;
            toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            toolStripContainer.BackColor = System.Drawing.Color.AliceBlue;


            // Add it to panels of SplitContainerAdv
            mainSplitContainer.Panel1.Controls.Add(leftSplitContainer);
            mainSplitContainer.Panel2.Controls.Add(rightSplitContainer);
            toolStripContainer.ContentPanel.Controls.Add(mainSplitContainer);
            Controls.Add(toolStripContainer);
        }
        private void CreatePropertiesPanel()
        {
            propertiesPanel = new PropertiesPanel(this);
            leftSplitContainer.Panel1.Controls.Add(propertiesPanel);
        }
        private void CreateElementsPanel()
        {
            elementsPanel = new ElementsPanel(this);
            leftSplitContainer.Panel2.Controls.Add(elementsPanel);
        }
        private void CreateMapPanel()
        {
            mapPanel = new MapPanel(this);
            rightSplitContainer.Panel1.Controls.Add(mapPanel);
            toolStripContainer.TopToolStripPanel.Controls.Add(mapPanel.GetMapToolStrip());
        }
        private void CreateAssetsPanel()
        {
            assetsPanel = new AssetsPanel(this);
            rightSplitContainer.Panel2.Controls.Add(assetsPanel);
        }
        private void CreateGroupBoxes(TabPage tabPage)
        {
            // Create and initialize a GroupBox and a Button control.
            GroupBox groupBox1 = new GroupBox();
            SplitContainer splitContainer1 = new SplitContainer();
            SplitContainer splitContainer2 = new SplitContainer();
            groupBox1.BackColor = System.Drawing.Color.Red;
            groupBox1.Dock = DockStyle.Left;
            splitContainer1.BackColor = System.Drawing.Color.Chocolate;
            splitContainer2.BackColor = System.Drawing.Color.Yellow;
            // Add the Button to the GroupBox.
            groupBox1.Controls.Add(splitContainer1);
            groupBox1.Controls.Add(splitContainer2);
            splitContainer1.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox1.SuspendLayout();

            // Add the GroupBox to the Form.
            tabPage.Controls.Add(groupBox1);

            // Create and initialize a GroupBox and a Button control.
            GroupBox groupBox2 = new GroupBox();
            groupBox2.Location = new System.Drawing.Point(120, 120);
            groupBox2.BackColor = System.Drawing.Color.Aquamarine;
            groupBox2.FlatStyle = FlatStyle.Flat;
            groupBox2.Dock = DockStyle.Fill;

            // Set the FlatStyle of the GroupBox.
            groupBox2.FlatStyle = FlatStyle.Standard;


            // Add the GroupBox to the Form.
            tabPage.Controls.Add(groupBox2);

            // Create and initialize a GroupBox and a Button control.
            GroupBox groupBox3 = new GroupBox();
            Button button3 = new Button();
            groupBox3.BackColor = System.Drawing.Color.Orange;
            button3.Text = "Click";
            button3.Location = new System.Drawing.Point(20, 20);
            groupBox3.Dock = DockStyle.Right;

            // Add the Button to the GroupBox.
            groupBox3.Controls.Add(button3);

            // Add the GroupBox to the Form.
            tabPage.Controls.Add(groupBox3);
        }
        public bool IsAssetsPanelOpen()
        {
            return !rightSplitContainer.Panel2Collapsed;
        }
        public bool IsElementsPanelOpen()
        {
            return !(mainSplitContainer.Panel1Collapsed || leftSplitContainer.Panel2Collapsed);
        }
        public bool IsPropertiesPanelOpen()
        {
            return !(mainSplitContainer.Panel1Collapsed || leftSplitContainer.Panel1Collapsed);
        }
        public void ToggleAssetsPanel()
        {
            rightSplitContainer.Panel2Collapsed = !rightSplitContainer.Panel2Collapsed;
        }
        public void ToggleElementsPanel()
        {
            if (IsElementsPanelOpen()) {
                if (IsPropertiesPanelOpen())
                {
                    leftSplitContainer.Panel2Collapsed = true;
                }
                else
                {
                    mainSplitContainer.Panel1Collapsed = true;
                }
            }
            else
            {
                if (mainSplitContainer.Panel1Collapsed)
                {
                    mainSplitContainer.Panel1Collapsed = false;
                }
                else
                {
                    leftSplitContainer.Panel2Collapsed = false;
                }
            }
           
        }
        public void TogglePropertiesPanel()
        {
            if (IsPropertiesPanelOpen())
            {
                if (IsElementsPanelOpen())
                {
                    leftSplitContainer.Panel1Collapsed = true;
                }
                else
                {
                    mainSplitContainer.Panel1Collapsed = true;
                }
            }
            else
            {
                if (mainSplitContainer.Panel1Collapsed)
                {
                    mainSplitContainer.Panel1Collapsed = false;
                }
                else
                {
                    leftSplitContainer.Panel1Collapsed = false;
                }
            }
        }
    }
}
