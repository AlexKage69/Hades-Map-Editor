using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper
{
    class ThingTextProperties
    {
        public Dictionary<Property, Label> labels;
        public Dictionary<Property, Control> controls;
        PropertyPanel parent;
        Label noSelection;
        Panel panel;
        public ThingTextProperties(PropertyPanel parent, Panel panel)
        {
            this.panel = panel;
            this.parent = parent;
            labels = new Dictionary<Property, Label>();
            controls = new Dictionary<Property, Control>();
            int yOffset = 0;
            noSelection = new Label();
            //labels[property].AutoSize = true;
            noSelection.Font = new Font("Calibri", 15);
            //noSelection.BorderStyle = BorderStyle.Fixed3D;
            //noSelection.ForeColor = System.Drawing.Color.Black;
            noSelection.Size = new Size(200, 45);
            noSelection.ImageAlign = ContentAlignment.MiddleCenter;
            //noSelection.Padding = new Padding(6);
            noSelection.Text = "Unselected";
            noSelection.Location = new System.Drawing.Point(150, 300);
            noSelection.Parent = panel;
            foreach (Property property in (Property[])Enum.GetValues(typeof(Property)))
            {
                labels[property] = new Label();
                //labels[property].AutoSize = true;
                labels[property].Font = new Font("Calibri", 15);
                labels[property].BorderStyle = BorderStyle.Fixed3D;
                labels[property].ForeColor = System.Drawing.Color.Black;
                labels[property].Size = new Size(165, 45);
                labels[property].ImageAlign = ContentAlignment.MiddleCenter;
                labels[property].Padding = new Padding(6);
                labels[property].Text = property.ToString();
                labels[property].Location = new System.Drawing.Point(0, yOffset);
                labels[property].Parent = panel;
                // Property others
                switch (property)
                {
                    //CheckBox type
                    case Property.ActivateAtRange:
                    case Property.Active:
                    case Property.AllowMovementReaction:
                    case Property.CausesOcculsion:
                    case Property.Clutter:
                    case Property.Collision:
                    case Property.CreatesShadows:
                    case Property.DrawVfxOnTop:
                    case Property.FlipHorizontal:
                    case Property.FlipVertical:
                    case Property.IgnoreGridManager:
                    case Property.Invert:
                    case Property.StopsLight:
                    case Property.UseBoundsForSortArea:
                        controls[property] = new CheckBox();
                        ((CheckBox)controls[property]).CheckedChanged += new EventHandler(CheckBoxClick);
                        controls[property].Parent = panel;
                        break;
                    //MaskedTextBox for Doubles
                    case Property.ActivationRange:
                        controls[property] = new MaskedTextBox();
                        controls[property].Parent = panel;
                        break;
                    // Can't change readonly
                    case Property.Id:
                    case Property.Name:
                        controls[property] = new Label();
                        break;
                    default:
                        controls[property] = new Control();
                        break;
                }
                controls[property].Name = property.ToString();
                controls[property].Location = new System.Drawing.Point(200, yOffset);
                controls[property].ClientSize = new Size(165, 45);
                controls[property].Padding = new Padding(6);
                controls[property].Parent = panel;
                yOffset += 45;
            }
            Unload();
        }
        public void Unload()
        {
            noSelection.Visible = true;
            foreach (var label in labels)
            {
                label.Value.Visible = false;
            }
            foreach (var control in controls)
            {
                control.Value.Visible = false;
            }
        }
        private void CheckBoxClick(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            Property selected = (Property)Enum.Parse(typeof(Property), control.Name);
            switch (selected)
            {
                //CheckBox type
                case Property.ActivateAtRange:
                    break;
                case Property.Active:
                    parent.CheckActive(((CheckBox)control).Checked);
                    break;
                case Property.AllowMovementReaction:
                case Property.CausesOcculsion:
                case Property.Clutter:
                case Property.Collision:
                case Property.CreatesShadows:
                case Property.DrawVfxOnTop:
                case Property.FlipHorizontal:
                case Property.FlipVertical:
                case Property.IgnoreGridManager:
                case Property.Invert:
                case Property.StopsLight:
                case Property.UseBoundsForSortArea:
                    break;
            }
        }
        public void Load(Obstacle info)
        {
            noSelection.Visible = false;
            //Active
            labels[Property.Active].Visible = true;
            ((CheckBox)controls[Property.Active]).Checked = info.Active;
            controls[Property.Active].Visible = true;
            //Id
            labels[Property.Id].Visible = true;
            ((Label)controls[Property.Id]).Text = info.Id.ToString();
            controls[Property.Id].Visible = true;
            //Name
            labels[Property.Name].Visible = true;
            ((Label)controls[Property.Name]).Text = info.Name;
            controls[Property.Name].Visible = true;
        }
        public Obstacle GetInfo()
        {
            return new Obstacle();
        } 
        public enum Property{
            ActivateAtRange,
            ActivationRange,
            Active,
            AllowMovementReaction,
            Ambient,
            Angle,
            AttachToID,
            AttachedIDs,
            CausesOcculsion,
            Clutter,
            Collision,
            Color,
            Comments,
            CreatesShadows,
            DataType,
            DrawVfxOnTop,
            FlipHorizontal,
            FlipVertical,
            GroupNames,
            HelpTextID,
            Hue,
            Id,
            IgnoreGridManager,
            Invert,
            Location,
            Name,
            OffsetZ,
            ParallaxAmount,
            Points,
            Saturation,
            Scale,
            SkewAngle,
            SkewScale,
            SortIndex,
            StopsLight,
            Tallness,
            UseBoundsForSortArea,
            Value
        }
    }


}
