using Hades_Map_Helper.Data;
using Hades_Map_Helper.Managers;
using Hades_Map_Helper.PropertiesSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.Sections
{
    public class PropertiesPanel: Panel, IComponent, Focusable
    {
        private Label noSelectionLable;
        private Panel attributePanel;
        private Obstacle currentObstacle;
        PropertyCheckbox activateAtRange, active, allowMovementReaction, causesOcculsion, clutter, collision, 
            createsShadows, drawVfxOnTop, flipHorizontal, flipVertical, ignoreGridManager, invert, stopsLight,
            useBoundsForSortArea;
        PropertyDouble activationRange, ambient, angle, hue, offsetZ, parallaxAmount, saturation, scale, 
            skewAngle, skewScale, tallness, value;
        PropertyInt attachToID, helpTextId, id, sortIndex;
        PropertyTextbox comments, dataType, name;
        
        //PropertyLocation location
        //PropertyIds attachedIDs;
        //PropertyGroup groupNames;
        //PropertyColor color;
        //PropertyPoints points;
        public PropertiesPanel()
        {
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            BackColor = System.Drawing.Color.Blue;
            AutoScroll = true;
            AutoSize = true;
            Dock = DockStyle.Fill;

            noSelectionLable = new Label();
            noSelectionLable.Text = "No Selection";
            noSelectionLable.AutoSize = false;
            noSelectionLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            noSelectionLable.Dock = DockStyle.Top;
            noSelectionLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            

            attributePanel = new TableLayoutPanel();
            //attributePanel.Size = new System.Drawing.Size(280,500);
            attributePanel.AutoSize = true;
            attributePanel.BackColor = System.Drawing.Color.Red;
            attributePanel.Visible = true;

            activateAtRange = new PropertyCheckbox("Activate At Range");
            activationRange = new PropertyDouble("Activation Range");
            active = new PropertyCheckbox("Active");
            allowMovementReaction = new PropertyCheckbox("Allow Movement Reaction");
            ambient = new PropertyDouble("Ambient");
            angle = new PropertyDouble("Angle");
            attachToID = new PropertyInt("Attached To ID");
            //attachedIDs = 
            causesOcculsion = new PropertyCheckbox("Causes Occulsion");
            clutter = new PropertyCheckbox("Clutter");
            collision = new PropertyCheckbox("Collision");
            //color = 
            comments = new PropertyTextbox("Comments");
            createsShadows = new PropertyCheckbox("Creates Shadows");
            dataType = new PropertyTextbox("Data Type");
            drawVfxOnTop = new PropertyCheckbox("Draw Vfx On Top");
            flipHorizontal = new PropertyCheckbox("Flip Horizontal");
            flipVertical = new PropertyCheckbox("Flip Vertical");
            //groupNames = 
            //helpTextId = new PropertyInt("Help Text Id");
            hue = new PropertyDouble("Hue");
            id = new PropertyInt("Id");
            ignoreGridManager = new PropertyCheckbox("Ignore Grid Manager");
            invert = new PropertyCheckbox("Invert");
            //location =
            name = new PropertyTextbox("Name");
            offsetZ = new PropertyDouble("Offset Z");
            parallaxAmount = new PropertyDouble("Parallax Amount");
            //points =
            saturation = new PropertyDouble("Saturation");
            scale = new PropertyDouble("Scale");
            skewAngle = new PropertyDouble("Skew Angle");
            skewScale = new PropertyDouble("Skew Scale");
            sortIndex = new PropertyInt("Sort Index");
            stopsLight = new PropertyCheckbox("Stops Light");
            tallness = new PropertyDouble("Tallness");
            useBoundsForSortArea = new PropertyCheckbox("Use Bounds For Sort Area");
            value = new PropertyDouble("Value");

            attributePanel.Controls.Add(activateAtRange);
            attributePanel.Controls.Add(activationRange);
            attributePanel.Controls.Add(active);
            attributePanel.Controls.Add(allowMovementReaction);
            attributePanel.Controls.Add(ambient);
            attributePanel.Controls.Add(angle);
            attributePanel.Controls.Add(attachToID);
            //attributePanel.Controls.Add(attachedIDs);
            attributePanel.Controls.Add(causesOcculsion);
            attributePanel.Controls.Add(clutter);
            attributePanel.Controls.Add(collision);
            //attributePanel.Controls.Add(color);
            attributePanel.Controls.Add(comments);
            attributePanel.Controls.Add(createsShadows);
            attributePanel.Controls.Add(dataType);
            attributePanel.Controls.Add(drawVfxOnTop);
            attributePanel.Controls.Add(flipHorizontal);
            attributePanel.Controls.Add(flipVertical);
            //attributePanel.Controls.Add(groupNames);
            //attributePanel.Controls.Add(helpTextId);
            attributePanel.Controls.Add(hue);
            attributePanel.Controls.Add(id);
            attributePanel.Controls.Add(ignoreGridManager);
            attributePanel.Controls.Add(invert);
            //attributePanel.Controls.Add(location);
            attributePanel.Controls.Add(name);
            attributePanel.Controls.Add(offsetZ);
            attributePanel.Controls.Add(parallaxAmount);
            //attributePanel.Controls.Add(points);
            attributePanel.Controls.Add(saturation);
            attributePanel.Controls.Add(scale);
            attributePanel.Controls.Add(skewAngle);
            attributePanel.Controls.Add(skewScale);
            attributePanel.Controls.Add(sortIndex);
            attributePanel.Controls.Add(stopsLight);
            attributePanel.Controls.Add(tallness);
            attributePanel.Controls.Add(useBoundsForSortArea);
            attributePanel.Controls.Add(value);
            //attributePanel.Visible = false;
            Controls.Add(attributePanel);
            Controls.Add(noSelectionLable);
        }
        public void Populate()
        {
            UnFocus();
        }
        public void UnFocus()
        {
            noSelectionLable.Visible = true;
            //attributePanel.Visible = false;
        }
        public void FocusOn(Obstacle obstacle)
        {
            currentObstacle = obstacle;
            activateAtRange.Update(currentObstacle.ActivateAtRange);
            activationRange.Update(currentObstacle.ActivationRange);
            active.Update(currentObstacle.Active);
            allowMovementReaction.Update(currentObstacle.AllowMovementReaction);
            ambient.Update(currentObstacle.Ambient);
            angle.Update(currentObstacle.Angle);
            attachToID.Update(currentObstacle.AttachToID);
            //attachedIDs.Update(currentObstacle.AttachedIDs);
            causesOcculsion.Update(currentObstacle.CausesOcculsion);
            clutter.Update(currentObstacle.Clutter);
            collision.Update(currentObstacle.Collision);
            //color.Update(currentObstacle.Color);
            comments.Update((string)currentObstacle.Comments);
            //createsShadows.Update((bool)currentObstacle.CreatesShadows);
            dataType.Update(currentObstacle.DataType);
            drawVfxOnTop.Update(currentObstacle.DrawVfxOnTop);
            flipHorizontal.Update(currentObstacle.FlipHorizontal);
            flipVertical.Update(currentObstacle.FlipVertical);
            //groupNames.Update(currentObstacle.GroupNames);
            //helpTextId.Update((int)currentObstacle.HelpTextID);
            hue.Update(currentObstacle.Hue);
            id.Update(currentObstacle.Id);
            ignoreGridManager.Update(currentObstacle.IgnoreGridManager);
            invert.Update(currentObstacle.Invert);
            //location.Update(currentObstacle.Location);
            name.Update(currentObstacle.Name);
            offsetZ.Update(currentObstacle.OffsetZ);
            parallaxAmount.Update(currentObstacle.ParallaxAmount);
            //points.Update(currentObstacle.Points);
            saturation.Update(currentObstacle.Saturation);
            scale.Update(currentObstacle.Scale);
            skewAngle.Update(currentObstacle.SkewAngle);
            skewScale.Update(currentObstacle.SkewScale);
            sortIndex.Update(currentObstacle.SortIndex);
            stopsLight.Update((bool)currentObstacle.StopsLight);
            tallness.Update(currentObstacle.Tallness);
            useBoundsForSortArea.Update(currentObstacle.UseBoundsForSortArea);
            value.Update(currentObstacle.Value);

            noSelectionLable.Visible = false;
            attributePanel.Visible = true;

        }
    }
}
