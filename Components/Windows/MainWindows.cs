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
    public static class MainWindows
    {
        private static async Task ShowDialogAsync(this Window form)
        {
            await Task.Yield(); // this is the magic ;o)
            form.ShowDialog();
        }
        public static async void ShowStartupWindow()
        {
            var dialog = new StartupWindow();
            dialog.ShowDialog();

            //var task = WorkAsync(dialog.Progress);
            //var dialogTask = dialog.ShowDialogAsync();
            //await task;
            //dialog.Close();
            //await dialogTask;
        }
        public static async void ShowDialog()
        {
            var dialog = new LoadWindow();
            var task = WorkAsync(dialog.Progress);
            var dialogTask = dialog.ShowDialogAsync();
            await task;
            dialog.Close();
            await dialogTask;
        }

        private static async Task WorkAsync(IProgress<double> progress)
        {
            for (int i = 0; i < 100; i++)
            {
                progress.Report(i);
                await Task.Delay(25).ConfigureAwait(false);
            }
        }
    }
}
