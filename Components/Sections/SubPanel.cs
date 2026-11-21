using Hades_Map_Editor.Data.Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Hades_Map_Editor
{
    public class SubPanel : Panel, IComponent
    {
        string windowTitle;
        protected ProjectPage parent;
        protected ProjectData data;
        public GroupBox ContentPanel;
        public Panel TopPanel, BottomPanel;
        private Panel TitlePanel, UpperPanel;
        Label title;
        protected Button closeButton;
        

        public SubPanel(ProjectPage pp, string windowTitle)
        {
            parent = pp;
            this.windowTitle = windowTitle;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            Dock = DockStyle.Fill;
            ContentPanel = new GroupBox();
            ContentPanel.BackColor = Color.LightGray;
            ContentPanel.Dock = DockStyle.Fill;
            TitlePanel = new Panel();
            TitlePanel.BackColor = Color.LightGray;
            TitlePanel.Dock = DockStyle.Top;
            TitlePanel.AutoSize = true;
            UpperPanel = new Panel();
            UpperPanel.BackColor = Color.LightGray;
            UpperPanel.Dock = DockStyle.Top;
            UpperPanel.AutoSize = true;
            TopPanel = new Panel();
            TopPanel.BackColor = Color.LightGray;
            TopPanel.Dock = DockStyle.Top;
            TopPanel.AutoSize = true;
            //TopPanel.AutoSize = true;
            BottomPanel = new Panel();
            BottomPanel.BackColor = Color.LightGray;
            BottomPanel.Dock = DockStyle.Bottom;
            BottomPanel.AutoSize = true;
            title = new Label();
            title.Text = windowTitle;
            title.AutoSize = true;
            title.Margin = new Padding(10);
            title.TextAlign = ContentAlignment.MiddleLeft;
            closeButton = new FlatButton();
            closeButton.Image = Image.FromFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Res\close_button.png");
            closeButton.Size = new Size(32,32);
            closeButton.Dock = DockStyle.Right;

            Controls.Add(ContentPanel);
            Controls.Add(UpperPanel);
            Controls.Add(BottomPanel);
            UpperPanel.Controls.Add(TopPanel);
            UpperPanel.Controls.Add(TitlePanel);
            TitlePanel.Controls.Add(closeButton);
            TitlePanel.Controls.Add(title);
        }
        public void Populate() {
        }
        public ProjectPage GetProjectPage()
        {
            return parent;
        }
        public ProjectData GetData()
        {
            return GetProjectPage().GetData();
        }
    }
}
