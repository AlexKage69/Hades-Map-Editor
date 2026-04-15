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
    public partial class LoadWindow : Window
    {
        public LoadWindow()
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
            label.Content = "This is a content";
            panel.Children.Add(label);
            Content = panel;
        }

        public IProgress<double> Progress { get; }

        private void ProgressHandler(double progress)
        {
            //progressBar.Value = progress;
        }
    }
}
