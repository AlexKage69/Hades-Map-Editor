using Hades_Map_Helper.AssetsSection;
using Hades_Map_Helper.Components;
using Hades_Map_Helper.Data;
using Hades_Map_Helper.Sections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hades_Map_Helper.Managers
{
    public class FormManager
    {
        private static HadesMapHelper form;
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
        public static void SetForm(HadesMapHelper forms)
        {
            form = forms;
        }
        public PropertiesPanel GetPropertiesPanel()
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
        }
        
        public CustomTabPage GetCurrentTab()
        {
            return form.tabPage.tabPages[form.tabPage.SelectedIndex];
        }

    }   
}
