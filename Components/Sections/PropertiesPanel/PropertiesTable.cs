using Hades_Map_Editor.Data.Map;
using Hades_Map_Editor.Managers;
using Hades_Map_Editor.PropertiesSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.PropertiesSection
{
    public class PropertiesTable: TableLayoutPanel, Focusable
    {
        private Obstacle currentObstacle;
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
        PropertiesPanel parent;
        public PropertiesTable(PropertiesPanel propertiesPanel)
        {
            parent = propertiesPanel;
            Initialize();
            Populate();
            //properties = new ThingTextProperties(this, panel);
        }
        public void Initialize()
        {
            BackColor = System.Drawing.Color.LightGray;
            //AutoSize = true;
            Name = "Properties";

            ColumnCount = 2;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //attributePanel.Size = new System.Drawing.Size(280,500);
            //attributePanel.AutoSize = true;
            Dock = DockStyle.Fill;
            //BackColor = System.Drawing.Color.Red;

            obstacleTitle = new PropertyTitle(parent, ".map_thing");
            activateAtRange = new PropertyCheckbox(parent, "Activate At Range");
            activationRange = new PropertyDouble(parent, "Activation Range");
            active = new PropertyCheckbox(parent, "Active");
            allowMovementReaction = new PropertyCheckbox(parent, "Allow Movement Reaction");
            ambient = new PropertyDouble(parent, "Ambient");
            angle = new PropertyDouble(parent, "Angle");
            attachToID = new PropertyID(parent, "Attached To ID");
            attachedIDs = new PropertyAttachedIDs(parent, "Attached IDs");
            causesOcculsion = new PropertyCheckbox(parent, "Causes Occulsion");
            clutter = new PropertyCheckbox(parent, "Clutter");
            collision = new PropertyCheckbox(parent, "Collision");
            color = new PropertyColor(parent, "Color");
            comments = new PropertyTextbox(parent, "Comments");
            createsShadows = new PropertyCheckbox(parent, "Creates Shadows");
            dataType = new PropertyTextbox(parent, "Data Type");
            drawVfxOnTop = new PropertyCheckbox(parent, "Draw Vfx On Top");
            flipHorizontal = new PropertyCheckbox(parent, "Flip Horizontal");
            flipVertical = new PropertyCheckbox(parent, "Flip Vertical");
            //groupNames = 
            //helpTextId = new PropertyInt("Help Text Id");
            hue = new PropertyDouble(parent, "Hue");
            id = new PropertyInt(parent, "Id");
            ignoreGridManager = new PropertyCheckbox(parent, "Ignore Grid Manager");
            invert = new PropertyCheckbox(parent, "Invert");
            location = new PropertyLocation(parent, "Location");
            name = new PropertyTextbox(parent, "Name");
            offsetZ = new PropertyDouble(parent, "Offset Z");
            parallaxAmount = new PropertyDouble(parent, "Parallax Amount");
            //points =
            saturation = new PropertyDouble(parent, "Saturation");
            scale = new PropertyDouble(parent, "Scale");
            skewAngle = new PropertyDouble(parent, "Skew Angle");
            skewScale = new PropertyDouble(parent, "Skew Scale");
            sortIndex = new PropertyInt(parent, "Sort Index");
            stopsLight = new PropertyCheckbox(parent, "Stops Light");
            tallness = new PropertyDouble(parent, "Tallness");
            useBoundsForSortArea = new PropertyCheckbox(parent, "Use Bounds For Sort Area");
            value = new PropertyDouble(parent, "Value");

            
            Controls.Add(obstacleTitle.GetTitle());
            SetCellPosition(obstacleTitle.GetTitle(), new TableLayoutPanelCellPosition(0, 0));
            SetColumnSpan(obstacleTitle.GetTitle(), 2);

            AddControls(activateAtRange);
            AddControls(activationRange);
            AddControls(active);
            AddControls(allowMovementReaction);
            AddControls(ambient);
            AddControls(angle);
            AddControls(attachToID);
            AddControls(attachedIDs);
            AddControls(causesOcculsion);
            AddControls(clutter);
            AddControls(collision);
            AddControls(color);
            AddControls(comments);
            AddControls(createsShadows);
            AddControls(dataType);
            AddControls(drawVfxOnTop);
            AddControls(flipHorizontal);
            AddControls(flipVertical);
            //AddControls(groupNames);
            //AddControls(helpTextId);
            AddControls(hue);
            AddControls(id);
            AddControls(ignoreGridManager);
            AddControls(invert);
            AddControls(location);
            AddControls(name);
            AddControls(offsetZ);
            AddControls(parallaxAmount);
            //AddControls(points);
            AddControls(saturation);
            AddControls(scale);
            AddControls(skewAngle);
            AddControls(skewScale);
            AddControls(sortIndex);
            AddControls(stopsLight);
            AddControls(tallness);
            AddControls(useBoundsForSortArea);
            AddControls(value);
        }
        public void Populate()
        {

        }
        private void AddControls(IPropertyItem item)
        {
            Controls.Add(item.GetTitle());
            Controls.Add(item.GetPanel());
        }
        public void FocusOn(int obsID)
        {
            currentObstacle = parent.GetData().mapData.GetFromId(obsID);
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
        }

        public void UnFocus()
        {
        }
    }
}
