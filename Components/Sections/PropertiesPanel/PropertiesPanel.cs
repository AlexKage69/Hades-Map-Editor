using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using Hades_Map_Editor.PropertiesSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Sections
{
    public class PropertiesPanel: Panel, IComponent, Focusable
    {
        private ProjectData data;
        private Label noSelectionLabel;
        private Panel attributePanel;
        private Obstacles currentObstacle;
        PropertyCheckbox activateAtRange, active, allowMovementReaction, causesOcculsion, clutter, collision, 
            createsShadows, drawVfxOnTop, flipHorizontal, flipVertical, ignoreGridManager, invert, stopsLight,
            useBoundsForSortArea;
        PropertyDouble activationRange, ambient, angle, hue, offsetZ, parallaxAmount, saturation, scale, 
            skewAngle, skewScale, tallness, value;
        PropertyInt helpTextId, id, sortIndex;
        PropertyTextbox comments, dataType, name;
        PropertyTitle obstacleTitle, metadataTitle;

        PropertyID attachToID;
        PropertyLocation location;
        PropertyAttachedIDs attachedIDs;
        //PropertyGroup groupNames;
        PropertyColor color;
        //PropertyPoints points;
        public PropertiesPanel(ProjectData data)
        {
            this.data = data;
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            BackColor = System.Drawing.Color.DarkGray;
            //AutoScroll = true;
            //AutoSize = true;
            Dock = DockStyle.Fill;

            noSelectionLabel = new Label();
            noSelectionLabel.Text = "No Selection";
            noSelectionLabel.AutoSize = true;
            noSelectionLabel.Left = (ClientSize.Width - noSelectionLabel.Width) / 2;
            noSelectionLabel.Top = (ClientSize.Height - noSelectionLabel.Height) / 2;
            //noSelectionLable.Dock = DockStyle.Cente;
            noSelectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            attributePanel = new TableLayoutPanel();
            attributePanel.AutoScroll = true;
            //attributePanel.Size = new System.Drawing.Size(280,500);
            //attributePanel.AutoSize = true;
            attributePanel.Dock = DockStyle.Fill;
            attributePanel.BackColor = System.Drawing.Color.Red;
            attributePanel.Visible = true;

            obstacleTitle = new PropertyTitle("Obstacles Data");
            activateAtRange = new PropertyCheckbox("Activate At Range");
            activationRange = new PropertyDouble("Activation Range");
            active = new PropertyCheckbox("Active");
            allowMovementReaction = new PropertyCheckbox("Allow Movement Reaction");
            ambient = new PropertyDouble("Ambient");
            angle = new PropertyDouble("Angle");
            attachToID = new PropertyID("Attached To ID");
            attachedIDs = new PropertyAttachedIDs("Attached IDs");
            causesOcculsion = new PropertyCheckbox("Causes Occulsion");
            clutter = new PropertyCheckbox("Clutter");
            collision = new PropertyCheckbox("Collision");
            color = new PropertyColor("Color");
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
            location = new PropertyLocation("Location");
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

            
            attributePanel.Controls.Add(obstacleTitle);
            attributePanel.Controls.Add(activateAtRange);
            attributePanel.Controls.Add(activationRange);
            attributePanel.Controls.Add(active);
            attributePanel.Controls.Add(allowMovementReaction);
            attributePanel.Controls.Add(ambient);
            attributePanel.Controls.Add(angle);
            attributePanel.Controls.Add(attachToID);
            attributePanel.Controls.Add(attachedIDs);
            attributePanel.Controls.Add(causesOcculsion);
            attributePanel.Controls.Add(clutter);
            attributePanel.Controls.Add(collision);
            attributePanel.Controls.Add(color);
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
            attributePanel.Controls.Add(location);
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
            Controls.Add(noSelectionLabel);
        }
        public void Populate()
        {
            UnFocus();
        }
        public void UnFocus()
        {
            noSelectionLabel.Visible = true;
            attributePanel.Visible = false;
        }
        public void FocusOn(int obsID)
        {
            currentObstacle = data.mapData.GetFromId(obsID);
            activateAtRange.Update(currentObstacle.ActivateAtRange);
            activationRange.Update(currentObstacle.ActivationRange);
            active.Update(currentObstacle.Active);
            allowMovementReaction.Update(currentObstacle.AllowMovementReaction);
            ambient.Update(currentObstacle.Ambient);
            angle.Update(currentObstacle.Angle);
            attachToID.Update(currentObstacle.AttachToID);
            attachedIDs.Update(currentObstacle.AttachedIDs.ToArray());
            causesOcculsion.Update(currentObstacle.CausesOcculsion);
            clutter.Update(currentObstacle.Clutter);
            collision.Update(currentObstacle.Collision);
            color.Update(currentObstacle.GetColor());
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
            location.Update(currentObstacle.GetLocation());
            name.Update(currentObstacle.Name);
            offsetZ.Update(currentObstacle.OffsetZ);
            parallaxAmount.Update(currentObstacle.ParallaxAmount);
            //points.Update(currentObstacle.Points);
            saturation.Update(currentObstacle.Saturation);
            scale.Update(currentObstacle.Scale);
            skewAngle.Update(currentObstacle.SkewAngle);
            skewScale.Update(currentObstacle.SkewScale);
            sortIndex.Update(currentObstacle.SortIndex);
            if(currentObstacle.StopsLight == null)
            {
                stopsLight.Update(false);
            }
            else
            {
                stopsLight.Update((bool)currentObstacle.StopsLight);
            }
            tallness.Update(currentObstacle.Tallness);
            useBoundsForSortArea.Update(currentObstacle.UseBoundsForSortArea);
            value.Update(currentObstacle.Value);

            noSelectionLabel.Visible = false;
            attributePanel.Visible = true;

        }
    }
}
