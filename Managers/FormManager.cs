using Hades_Map_Editor.AssetsSection;
using Hades_Map_Editor.BottomMenu;
using Hades_Map_Editor.Components;
using Hades_Map_Editor.Components.Dialogs;
using Hades_Map_Editor.Data;
using Hades_Map_Editor.TopMenu;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Editor.Managers
{
    public class FormManager
    {
        private static HadesMapEditor form;
        private static FormManager _instance;
        public static FormManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FormManager();
            }
            return _instance;
        }
        private FormManager() {
            
        }
        public static void SetForm(HadesMapEditor forms)
        {
            form = forms;
        }
        /*public PropertiesPanel GetPropertiesPanel()
        {
            return GetCurrentTab().propertiesPanel;
        }
        public ElementsPanel GetElementsPanel()
        {
            return GetCurrentTab().elementsPanel;
        }
        public MapPanel GetMapPanel()
        {
            return GetCurrentTab().mapPanel;
        }
        public AssetsPanel GetAssetsPanel()
        {
            return GetCurrentTab().assetsPanel;
        }*/
        public void StartLoading(BackgroundWorker bw)
        {
           form.topMenuStrip.GetMainControl().loadingDialog.ShowDialog(bw);
        }
        public void EndLoading()
        {
            LoadingDialog loadingDialog = form.topMenuStrip.GetMainControl().loadingDialog;
            loadingDialog.Invoke((MethodInvoker)(() => loadingDialog.Close()));
        }
        public void ChangeLoadingStatus(string packageLoaded, int value, int maximum)
        {
            LoadingDialog loadingDialog = form.topMenuStrip.GetMainControl().loadingDialog;
            loadingDialog.Invoke((MethodInvoker)(() => loadingDialog.UpdateText("Loaded: " + packageLoaded, value,maximum)));
        }
        public bool HasTabOpen()
        {
            return form.tabPage.tabPages.Count > 0;
        }
        public ProjectPage GetCurrentTab()
        {
            if(form.tabPage.tabPages.Count <= 0){
                return null;
            }
            return form.tabPage.tabPages[form.tabPage.SelectedIndex];
        }
        public TopMenuStrip GetTopMenu()
        {
            return form.topMenuStrip;
        }
        public BottomMenuStrip GetBottomMenu()
        {
            return form.bottomMenuStrip;
        }

    }   
}
