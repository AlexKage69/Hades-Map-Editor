using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Components.Dialogs
{
    public class LoadingDialog : Form
    {
        Label loadingText;
        ProgressBar progressBar;
        //BackgroundWorker bw;
        public LoadingDialog()
        {
            Initialize();
            Populate();
        }
        public void Initialize()
        {
            Text = "Loading...";
            Width = 350;
            Height = 100;
            ControlBox = false;
            MaximizeBox = false;
            MinimizeBox = false;
            CenterToScreen();
            //FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Dock = DockStyle.Fill;

            loadingText = new Label();
            loadingText.Text = "Starting...";
            loadingText.Dock = DockStyle.Top;

            progressBar = new ProgressBar();
            progressBar.Dock = DockStyle.Top;

            Controls.Add(loadingText);
            Controls.Add(progressBar);
        }

        public void Populate()
        {

        }

        public void UpdateText(string message, int value, int maximum)
        {
            loadingText.Text = message;
            progressBar.Maximum = maximum;
            progressBar.Value = value;
        }
        public void ShowDialog(BackgroundWorker bw)
        {
            bw.RunWorkerAsync();
            base.ShowDialog();
        }
    }
}
