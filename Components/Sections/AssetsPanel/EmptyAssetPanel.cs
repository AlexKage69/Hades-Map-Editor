using Hades_Map_Editor.Data;
using Hades_Map_Editor.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.AssetsSection
{
    public class EmptyAssetPanel : Panel, IComponent
    {
        Button loadButton, fetchButton;
        Label messageLabel;
        AssetsPanel parent;
        public EmptyAssetPanel(AssetsPanel assetsPanel)
        {
            parent = assetsPanel;
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            Dock = DockStyle.Fill;

            messageLabel = new Label();
            messageLabel.Text = "No asset found. Please press load, might take a few minutes.";
            messageLabel.Dock = DockStyle.Top;

            loadButton = new Button();
            loadButton.Text = "Load Assets";
            loadButton.Dock = DockStyle.Top;

            fetchButton = new Button();
            fetchButton.Text = "Fetch Assets";
            fetchButton.Dock = DockStyle.Top;

            Controls.Add(messageLabel);
            Controls.Add(loadButton);
            Controls.Add(fetchButton);
        }
        public void Populate()
        {
            fetchButton.Click += FetchButton_Click;
            loadButton.Click += LoadButton_Click;
            AssetsManager am = AssetsManager.GetInstance();
            if (!am.HasLoadedResources())
            {
                loadButton.Enabled = false;
            }
            else
            {
                fetchButton.Enabled = false;
            }
        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            formManager.StartLoading(new CompileAssetWorker());
        }
        private void FetchButton_Click(object sender, EventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            formManager.StartLoading(new FetchAssetWorker(GameVersion.Hades));
            loadButton.Enabled = true;
            fetchButton.Enabled = false;
        }
    }
}
