using Hades_Map_Editor.Data.Raw;
using Hades_Map_Editor.Data.Asset;
using Hades_Map_Editor.Properties;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Editor.Managers
{
    public class AssetsManager
    {
        private Dictionary<string, BiomeData> biomes;
        private static AssetsManager _instance;
        //private bool processOngoing;
        public static AssetsManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AssetsManager();
            }
            return _instance;
        }
        private AssetsManager() {
            IOManager saveManager = IOManager.GetInstance();
            try
            {
                //hadesAssets = saveManager.LoadAssets(GameVersion.Hades);
                //hades2Assets = saveManager.LoadAssets(GameVersion.Hades2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //hadesAssets = new AssetData();
                //hades2Assets = new AssetData();
            }
        }
        /*public void LoadImages(string selectedBiome = "all")
        {
            if (selectedBiome == "all")
            {
                foreach (var biome in biomes)
                {
                    foreach (var type in biome.Value.assetsData)
                    {
                        foreach (var asset in type.Value)
                        {
                            asset.Value.LoadImage();
                        }
                    }
                }
            }
            else
            {
                if (biomeData.ContainsKey(selectedBiome))
                {
                    foreach (var type in biomeData[selectedBiome].assetsData)
                    {
                        foreach (var asset in type.Value)
                        {
                            asset.Value.LoadImage();
                        }
                    }
                }
            }
        }*/
        public BiomeData GetBiomeAssets(string selectedBiome)
        {
            return biomes[selectedBiome];
        }
        public List<string> GetBiomes()
        {
            return biomes.Keys.ToList();
        }
        /*public List<string> BiomesFiltered(AssetType type)
        {
            AssetData ad = (gameVersion == GameVersion.Hades) ? hadesAssets : hades2Assets;
            return ad.biomeData.Where(v => { return v.Value.assetsData.ContainsKey(type) && v.Value.assetsData[type].Keys.Count > 0; }).ToDictionary(i => i.Key, i => i.Value).Keys.ToList();
        }*/
        public bool HasLoadedResources()
        {
            ConfigManager configManager = ConfigManager.GetInstance();
            return Directory.GetDirectories(configManager.GetConfigurations().resourcesPath).Any();
        }
        public bool HasBiome()
        {
            return biomes.Count > 0;
        }
        public bool AddBiome(string key, BiomeData biome)
        {
            biomes.Add(key, biome);
            return true;
        }
        /*public void CompileAssets()
        {
            FormManager formManager = FormManager.GetInstance();
            var task = Task.Factory.StartNew(() =>
            {
                AssetData assets = new AssetData();
                ConfigManager configManager = ConfigManager.GetInstance();
                Console.WriteLine("Start Compilation");
                var directories = Directory.GetDirectories(configManager.GetPath(ConfigType.ResourcesPath));
                foreach (var fulldirectory in directories)
                {
                    string manifestPath = fulldirectory + @"\manifest";
                    if (!Directory.Exists(manifestPath))
                    {
                        continue;
                    }
                    string directory = Path.GetFileName(fulldirectory);
                    assets.biomeData.Add(directory, new BiomeAssetData());
                    var assetFiles = Directory.GetFiles(manifestPath);
                    foreach (var assetFile in assetFiles)
                    {
                        using (StreamReader r = new StreamReader(assetFile))
                        {
                            string json = r.ReadToEnd();
                            var asset = JsonConvert.DeserializeObject<RawAtlasJson>(json);
                            asset.AppendAssets(new BiomeAssetData());
                            formManager.GetBottomMenu().SetStatuts(asset.name);
                        }
                    }
                }
                IOManager saveManager = IOManager.GetInstance();
                saveManager.SaveAssets(assets);
                this.assets = assets;
                formManager.EndLoading();
                formManager.GetBottomMenu().SetStatuts("Compiling - done");
            });
        }*/
        /*public void LoadImages()
        {
            assets.LoadImages();
        }*/
        public bool GetAsset( string id, out AssetData asset)
        {
            foreach (var biome in biomes)
            {
                foreach (var type in biome.Value.assetsData)
                {
                    if (type.Value.ContainsKey(id))
                    {
                        asset = type.Value[id];
                        return true;
                    }
                }
            }
            asset = null;
            return false;
        }

        /*public List<string> GetAllNames()
        {
            return assets.Where((v)=> { return v.Value.GetAssetType() == MapAssetType.Tileset; }).ToDictionary(i => i.Key, i => i.Value).Keys.ToList().GetRange(0,5);
        }
        public Image GetImage(string subAssetName)
        {
            return assets[subAssetName].GetImage();
        }*/
        /*public Image CreateImage()
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(@"C:\SomeFolder\Images\image.png");
            bitmap.DecodePixelWidth = 100;
            bitmap.EndInit();
            return new Image { Source = bitmap, Margin = new Thickness(0), Stretch = Stretch.None, ClipToBounds = false };
        }*/
    }
    public class FetchAssetWorker : BackgroundWorker
    {
        AssetsManager am;
        int doneProcess, maxProcess;
        GameVersion version;
        ConfigManager configManager;
        public FetchAssetWorker(GameVersion version) : base()
        {
            am = AssetsManager.GetInstance();
            configManager = ConfigManager.GetInstance();
            this.version = version;
            DoWork += DoWorkFunc;
            RunWorkerCompleted += RunWorkerCompletedFunc;
        }
        private void DoWorkFunc(object sender, DoWorkEventArgs e)
        {
            FetchAssetsFromGameAsync();
        }
        private void RunWorkerCompletedFunc(object sender, RunWorkerCompletedEventArgs e)
        {
            //FormManager.GetInstance().EndLoading();
        }
        private async void FetchAssetsFromGameAsync()
        {
            try
            {
                doneProcess = 0;
                FormManager formManager = FormManager.GetInstance();
                //formManager.GetBottomMenu().SetStatuts("Fetch assets...");

                DirectoryInfo di = new DirectoryInfo(((version == GameVersion.Hades)?"\\Content\\Win\\Packages": "\\Content\\Packages\\1080p"));
                FileInfo[] packages = di.GetFiles("*.pkg");
                maxProcess = packages.Length;
                List<Task> tasks = new List<Task>();
                foreach (FileInfo fi in packages)
                {
                    string packageName = Path.GetFileNameWithoutExtension(fi.Name);
                    tasks.Add(RunProcessAsync(packageName).ContinueWith(taskid => { UpdateDialogMessage(packageName); }));
                }
                //_ = RunProcessAsync(configManager.GetPythonPath(), "Erebus", configManager.GetHadesPath(), configManager.GetResourcesPath());
                //_ = RunProcessAsync(configManager.GetPythonPath(), "Asphodel", configManager.GetHadesPath(), configManager.GetResourcesPath());
                //_ = RunProcessAsync(configManager.GetPythonPath(), "Elysium", configManager.GetHadesPath(), configManager.GetResourcesPath());
                //_ = RunProcessAsync(configManager.GetPythonPath(), "Surface", configManager.GetHadesPath(), configManager.GetResourcesPath());
                //_ = RunProcessAsync(configManager.GetPythonPath(), "Styx", configManager.GetHadesPath(), configManager.GetResourcesPath());

                Task task = Task.WhenAll(tasks);
                await task.ContinueWith(allTask =>
                {
                    formManager.EndLoading();
                });

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }
        private void UpdateDialogMessage(string packageName)
        {
            doneProcess += 1;
            FormManager formManager = FormManager.GetInstance();
            formManager.ChangeLoadingStatus(packageName, doneProcess, maxProcess);
        }


        private Task<int> RunProcessAsync(string packageName)
        {
            string hadesPath = configManager.GetConfigurations().hadesPath;
            string resourcesPath = configManager.GetConfigurations().resourcesPath;
            if (!Directory.Exists(resourcesPath + @"\" + packageName))
            {
                Directory.CreateDirectory(resourcesPath + @"\" + packageName);
            }
            else
            {
                Directory.Delete(resourcesPath + @"\" + packageName, true);
                Directory.CreateDirectory(resourcesPath + @"\" + packageName);
            }
            string pythonScriptPath = (true) ? "\\Python\\assets_extract_h1.py\"" : "\\Python\\assets_extract_h2.py\"";
            if(!File.Exists(configManager.GetConfigurations().pythonPath + @"\python.exe"))
            {
                throw (new Exception());
            }
            var tcs = new TaskCompletionSource<int>();
            var process = new Process
            {
                StartInfo = {
                    FileName = configManager.GetConfigurations().pythonPath+@"\python.exe",
                    Arguments = string.Format("{0} {1} {2} {3}", "\"" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + pythonScriptPath, "\""+packageName+"\"", "\""+hadesPath+"\"", "\""+resourcesPath+"\""),
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                },
                EnableRaisingEvents = true
            };
            process.Exited += (sender, args) =>
            {
                process.WaitForExit();
                tcs.SetResult(process.ExitCode);
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }
    }
    public class CompileAssetWorker : BackgroundWorker{
        AssetsManager am;
        int doneProcess, maxProcess;
        public CompileAssetWorker() : base()
        {
            am = AssetsManager.GetInstance();
            DoWork += DoWorkFunc;
            RunWorkerCompleted += RunWorkerCompletedFunc;
        }
        private void DoWorkFunc(object sender, DoWorkEventArgs e)
        {
            CompileAsset();
        }
        private void RunWorkerCompletedFunc(object sender, RunWorkerCompletedEventArgs e)
        {
            FormManager formManager = FormManager.GetInstance();
            formManager.EndLoading();
        }
        private void CompileAsset()
        {
            doneProcess = 0;
            maxProcess = 0;
            FormManager formManager = FormManager.GetInstance();
            AssetData assets = new AssetData();
            ConfigManager configManager = ConfigManager.GetInstance();
            string resourcesPath = configManager.GetConfigurations().resourcesPath;
            var directories = Directory.GetDirectories(resourcesPath);
            foreach (var fulldirectory in directories)
            {
                string manifestPath = fulldirectory + @"\manifest";
                if (!Directory.Exists(manifestPath))
                {
                    continue;
                }
                string directory = Path.GetFileName(fulldirectory);
                var assetFiles = Directory.GetFiles(manifestPath);
                var newBiome = new BiomeData();
                maxProcess += assetFiles.Length;
                foreach (var assetFile in assetFiles)
                {
                    using (StreamReader r = new StreamReader(assetFile))
                    {
                        string json = r.ReadToEnd();
                        var asset = JsonConvert.DeserializeObject<RawAtlasJson>(json);
                        //asset.AppendAssets(newBiome[directory]);
                    }
                    UpdateDialogMessage(directory +"["+ Path.GetFileNameWithoutExtension(assetFile).Split('.')[0] + "]");
                }
                am.AddBiome(directory, newBiome);
            }
            IOManager saveManager = IOManager.GetInstance();
            saveManager.SaveAssets(assets);
            //am.SetAssets(assets);
        }
        private void UpdateDialogMessage(string packageName)
        {
            doneProcess += 1;
            FormManager formManager = FormManager.GetInstance();
            formManager.ChangeLoadingStatus(packageName, doneProcess, maxProcess);
        }
    }

    public class MapEncodeWorker : BackgroundWorker
    {
        AssetsManager am;
        int doneProcess, maxProcess;
        GameVersion version;
        ConfigManager configManager;
        public MapEncodeWorker(GameVersion version) : base()
        {
            am = AssetsManager.GetInstance();
            configManager = ConfigManager.GetInstance();
            this.version = version;
            DoWork += DoWorkFunc;
            RunWorkerCompleted += RunWorkerCompletedFunc;
        }
        private void DoWorkFunc(object sender, DoWorkEventArgs e)
        {
            FetchAssetsFromGameAsync();
        }
        private void RunWorkerCompletedFunc(object sender, RunWorkerCompletedEventArgs e)
        {
            //FormManager.GetInstance().EndLoading();
        }
        private async void FetchAssetsFromGameAsync()
        {
            try
            {
                doneProcess = 0;
                FormManager formManager = FormManager.GetInstance();
                //formManager.GetBottomMenu().SetStatuts("Fetch assets...");

                DirectoryInfo di = new DirectoryInfo(((true) ? "\\Content\\Win\\Packages" : "\\Content\\Packages\\1080p"));
                FileInfo[] packages = di.GetFiles("*.pkg");
                maxProcess = packages.Length;
                List<Task> tasks = new List<Task>();
                foreach (FileInfo fi in packages)
                {
                    string packageName = Path.GetFileNameWithoutExtension(fi.Name);
                    tasks.Add(RunProcessAsync(version, packageName).ContinueWith(taskid => { UpdateDialogMessage(packageName); }));

                    Task task = Task.WhenAll(tasks);
                    await task.ContinueWith(allTask =>
                    {
                        formManager.EndLoading();
                    });
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }
        private void UpdateDialogMessage(string packageName)
        {
            doneProcess += 1;
            FormManager formManager = FormManager.GetInstance();
            formManager.ChangeLoadingStatus(packageName, doneProcess, maxProcess);
        }


        private Task<int> RunProcessAsync(GameVersion hadesVersion, string packageName)
        {
            string hadesPath = configManager.GetConfigurations().hadesPath;
            string resourcesPath = configManager.GetConfigurations().resourcesPath + "\\" + hadesVersion.ToString();
            if (!Directory.Exists(resourcesPath + @"\" + packageName))
            {
                Directory.CreateDirectory(resourcesPath + @"\" + packageName);
            }
            else
            {
                Directory.Delete(resourcesPath + @"\" + packageName, true);
                Directory.CreateDirectory(resourcesPath + @"\" + packageName);
            }
            string pythonScriptPath = (hadesVersion == GameVersion.Hades) ? "\\Python\\assets_extract_h1.py\"" : "\\Python\\assets_extract_h2.py\"";
            if (!File.Exists(configManager.GetConfigurations().pythonPath + @"\python.exe"))
            {
                throw (new Exception());
            }
            var tcs = new TaskCompletionSource<int>();
            var process = new Process
            {
                StartInfo = {
                    FileName = configManager.GetConfigurations().pythonPath+@"\python.exe",
                    Arguments = string.Format("{0} {1} {2} {3}", "\"" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + pythonScriptPath, "\""+packageName+"\"", "\""+hadesPath+"\"", "\""+resourcesPath+"\""),
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                },
                EnableRaisingEvents = true
            };
            process.Exited += (sender, args) =>
            {
                process.WaitForExit();
                tcs.SetResult(process.ExitCode);
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }
    }
}
