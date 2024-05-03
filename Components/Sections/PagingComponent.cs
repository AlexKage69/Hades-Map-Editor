using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    public class PagingComponent : StatusStrip, IComponent
    {
        ToolStripButton[] buttons;
        ToolStripLabel label1, label2;
        IPaging parent;

        public PagingComponent(IPaging parent)
        {
            this.parent = parent;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            buttons = new ToolStripButton[9];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new ToolStripButton();
                buttons[i].Dock = DockStyle.Bottom;
                buttons[i].Click += (s, e) => Action_GoToPage(s, e);
                //buttons[i].Visible = false;
                if (i == 2)
                {
                    label1 = new ToolStripLabel();
                    label1.Text = "...";
                    label1.Visible = false;
                    Items.Add(label1);
                }
                if (i == 8)
                {
                    label2 = new ToolStripLabel();
                    label2.Text = "...";
                    label2.Visible = false;
                    Items.Add(label2);
                }
                //buttons[i].Location = new System.Drawing.Point(i*20,0);
                Items.Add(buttons[i]);
            }           
            
        }

        public void Populate(){}
        public void GoToPage(int currentPage, int maxPage)
        {
            for(int i = 0; i < buttons.Length; i++)
            {
                //buttons[i].Click -= (s, e) => Action_GoToPage(s, e, int.Parse(buttons[i].Text));
                buttons[i].Visible = ((maxPage < 10 && i < maxPage )||(maxPage>=10 && ((i >= currentPage - 1 &&  i <= currentPage + 1) || (currentPage <= 3 && i <= 3)) || (currentPage >= maxPage-3 && i >= buttons.Length-3)));
                buttons[i].Text = i.ToString();
                buttons[i].BackColor = (i == currentPage) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
            }
        }
        private void Action_GoToPage(object sender, EventArgs e)
        {
            int newPage = int.Parse((sender as ToolStripButton).Text);
            parent.GoToPage(newPage);
        }
    }
}
