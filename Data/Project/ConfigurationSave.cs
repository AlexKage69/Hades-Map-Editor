using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Editor.Data.Project
{
    public class ConfigurationSave
    {
        public string hadesPath;
        public string hades2Path;
        public string resourcesPath;
        public string projectPath;
        public string pythonPath;
        public List<string> projectList;
        public ConfigurationSave()
        {
            projectList = new List<string>();
        }
    }
}
