using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor
{
    public class CustomSplitContainer : SplitContainer
    {
        [DefaultValue(typeof(Color), "Black")]
        public System.Drawing.Color SplitterColor { get; set; } = System.Drawing.Color.Black;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            Rectangle rect = SplitterRectangle;

            using (Pen pen = new Pen(SplitterColor))
            {
                if (Orientation == Orientation.Vertical)
                {
                    g.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
                    g.DrawLine(pen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
                }
                else
                {
                    g.DrawLine(pen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
                    g.DrawLine(pen, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
                }
            }
        }
    }

    public class CustomContainer : ScrollableControl, IContainerControl
    {
        private Control activeControl;
        public CustomContainer()
        {
            // Make the container control Blue so it can be distinguished on the form.
            this.BackColor = System.Drawing.Color.Blue;

            // Make the container scrollable.
            this.AutoScroll = true;
        }

        // Add implementation to the IContainerControl.ActiveControl property.
        public Control ActiveControl
        {
            get
            {
                return activeControl;
            }

            set
            {
                // Make sure the control is a member of the ControlCollection.
                if (this.Controls.Contains(value))
                {
                    activeControl = value;
                }
            }
        }

        // Add implementations to the IContainerControl.ActivateControl(Control) method.
        public bool ActivateControl(Control active)
        {
            if (this.Controls.Contains(active))
            {
                // Select the control and scroll the control into view if needed.
                active.Select();
                this.ScrollControlIntoView(active);
                this.activeControl = active;
                return true;
            }
            return false;
        }
    }
}
