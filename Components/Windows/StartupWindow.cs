using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Shapes;
using static IronPython.Modules._ast;

namespace Hades_Map_Editor.Components.Windows
{
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            Height = 200;
            Width = 400;
            WindowStyle = WindowStyle.None;
            InitializeComponent();

            Progress = new Progress<double>(progress => ProgressHandler(progress));
        }
        private void InitializeComponent()
        {

            Panel panel = new StackPanel();
            Label label = new Label();
            Button button = new Button();
            button.Click += NextButton_Click;
            label.Content = "Welcome to Hades Map Editor! This tool is used to create and edit Hades 1 & 2 Maps. Requirements are:\n - Python\n - Deppth \n Hades 1 \n Hades 2";
            button.Content = "Next";
            panel.Children.Add(label);
            panel.Children.Add(button);
            Content = panel;
        }

        public IProgress<double> Progress { get; }

        private void ProgressHandler(double progress)
        {
            //progressBar.Value = progress;
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
