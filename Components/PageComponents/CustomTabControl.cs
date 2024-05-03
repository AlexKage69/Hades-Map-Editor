using Hades_Map_Editor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Components
{
    public class CustomTabControl : TabControl, IComponent
    {
        public List<CustomTabPage> tabPages;
        public CustomTabControl()
        {
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            tabPages = new List<CustomTabPage>();
            BackColor = System.Drawing.Color.Green;
            //tabControl.Layout = 
            //tabControl.Location = new System.Drawing.Point(60, 16);
            Dock = DockStyle.Fill;
            //tabControl.Size = new System.Drawing.Size(app.form.Width-5, app.form.Height-55);
            SelectedIndex = 0;
            TabIndex = 0;
        }
        public void Populate()
        {
        }
        public void CreateNewTabPage(ProjectData data)
        {
            CustomTabPage tabPage = new CustomTabPage(data);
            tabPage.Text = data.name;
            //tabPage.Size = new System.Drawing.Size(app.form.Width, app.form.Height);
            Form parent = FindForm();
            if (parent != null && parent.MdiChildren != null)//ParentReference is null
                tabPage.TabIndex = parent.MdiChildren.Length;
            tabPages.Add(tabPage);
            //tabPage1.Controls.Add(this.tab1Button1);

            Controls.Add(tabPage);
        }   
    }
}
