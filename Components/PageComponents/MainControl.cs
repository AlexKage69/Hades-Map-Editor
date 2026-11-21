using Hades_Map_Editor.Components.Dialogs;
using Hades_Map_Editor.Data.Project;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor
{
    public class MainControl : TabControl, IComponent
    {
        public List<ProjectPage> tabPages;
        public ParametersDialog parametersDialog;
        public ParametersDialog metadataDialog;
        public LoadingDialog loadingDialog;
        ContextMenu tabContextMenu;
        MenuItem save, close;
        TabPage lastSelected;
        public MainControl()
        {
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            tabPages = new List<ProjectPage>();
            BackColor = System.Drawing.Color.Green;
            parametersDialog = new ParametersDialog();
            loadingDialog = new LoadingDialog();
            //tabControl.Layout = 
            //tabControl.Location = new System.Drawing.Point(60, 16);
            Dock = DockStyle.Fill;
            //tabControl.Size = new System.Drawing.Size(app.form.Width-5, app.form.Height-55);
            SelectedIndex = 0;
            TabIndex = 0;

            // Create ContextMenu for Tabs
            tabContextMenu = new ContextMenu();
            save = tabContextMenu.MenuItems.Add("Save");
            close = tabContextMenu.MenuItems.Add("Close");
        }
        public void Populate()
        {
            MouseClick += MainControl_MouseClick;

            save.Click += MainControl_SaveTab;
            close.Click += MainControl_CloseTab;
        }
        public void CreateNewTabPage(ProjectData data)
        {
            ProjectPage tabPage = new ProjectPage(data);
            tabPage.Text = data.name;
            //tabPage.Size = new System.Drawing.Size(app.form.Width, app.form.Height);
            Form parent = FindForm();
            if (parent != null && parent.MdiChildren != null)//ParentReference is null
                tabPage.TabIndex = parent.MdiChildren.Length;
            tabPages.Add(tabPage);
            //tabPage1.Controls.Add(this.tab1Button1);

            Controls.Add(tabPage);
        }   
        public ProjectPage GetCurrentProjectPage() { return tabPages[SelectedIndex]; }

        private void MainControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int ix = 0; ix < TabCount; ++ix)
                {
                    if (GetTabRect(ix).Contains(e.Location))
                    {
                        tabContextMenu.Show(this, e.Location);//places the menu at the pointer position
                        lastSelected = TabPages[ix];
                        break;
                    }
                }
            }
        }
        private void MainControl_SaveTab(object sender, EventArgs e)
        {
            Console.WriteLine("Save");
            if (lastSelected != null)
            {
                IOManager ioManager = IOManager.GetInstance();
                ioManager.SaveProject(((ProjectPage)lastSelected).GetData());
            }
        }
        private void MainControl_CloseTab(object sender, EventArgs e)
        {
            Console.WriteLine("Close");
            if(lastSelected != null)
            {
                ConfigManager configManager = ConfigManager.GetInstance();
                configManager.RemoveProjectPath(((ProjectPage)lastSelected).GetData().projectPath);                
                lastSelected.Dispose();
            }
        }
    }
}
