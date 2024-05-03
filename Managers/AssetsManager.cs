using Hades_Map_Helper.Data;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Helper.Managers
{
    public class AssetsManager
    {
        private Assets assets;
        private static AssetsManager _instance;
        public static AssetsManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AssetsManager();
            }
            return _instance;
        }
        private AssetsManager() {
            SaveManager saveManager = SaveManager.GetInstance();
            try
            {
                assets = saveManager.LoadAssets();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                assets = CompileAssets();
            }
        }
        public Dictionary<string, Dictionary<AssetType, Dictionary<string, Asset>>> GetAssets()
        {
            return assets.biomes;
        }
        public List<string> Biomes()
        {
            return new List<string>(assets.biomes.Keys);
        }
        public Assets CompileAssets()
        {
            Assets assets = new Assets();
            ConfigManager configManager = ConfigManager.GetInstance();
            Console.WriteLine("Start Compilation");
            var directories = Directory.GetDirectories(configManager.GetResourcesPath());
            foreach (var fulldirectory in directories)
            {
                string directory = Path.GetFileName(fulldirectory);
                Dictionary<AssetType, Dictionary<string, Asset>> biomeAssets = new Dictionary<AssetType, Dictionary<string,Asset>>();
                assets.biomes.Add(directory, biomeAssets);
                var assetFiles = Directory.GetFiles(fulldirectory + @"\manifest");
                foreach (var assetFile in assetFiles)
                {
                    using (StreamReader r = new StreamReader(assetFile))
                    {
                        string json = r.ReadToEnd();
                        var asset = JsonConvert.DeserializeObject<RawAtlasJson>(json);
                        asset.AppendAssets(biomeAssets);
                    }
                }
            }
            Console.WriteLine("End Compilation");
            SaveManager saveManager = SaveManager.GetInstance();
            saveManager.SaveAssets(assets);
            Console.WriteLine("Saved Compilation");
            return assets;
        }
        public void CreateAsset()
        {

        }
        /*public void InitAssets()
        {
            Console.WriteLine("Start Assets");
            var assetFiles = Directory.GetFiles(assetPath + @"Tartarus\manifest");
            foreach (var assetFile in assetFiles)
            {
                using (StreamReader r = new StreamReader(assetFile))
                {
                    string json = r.ReadToEnd();
                    var asset = JsonConvert.DeserializeObject<RawAtlasJson>(json);
                    Console.WriteLine("Loading:" + asset.GetName());
                    asset.AppendAssets(assets);
                    //break;
                }
            }
            Console.WriteLine("End Assets");
        }*/
        public bool GetAsset(string id, out Asset asset)
        {
            foreach(var biome in assets.biomes)
            {
                foreach (var type in biome.Value)
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
        public void FetchAssetsFromGame()
        {
            try
            {
                ConfigManager configManager = ConfigManager.GetInstance();

                _ = RunProcessAsync("Tartarus",configManager.GetHadesPath(), configManager.GetResourcesPath());
                _ = RunProcessAsync("Erebus", configManager.GetHadesPath(), configManager.GetResourcesPath());
                _ = RunProcessAsync("Asphodel", configManager.GetHadesPath(), configManager.GetResourcesPath());
                _ = RunProcessAsync("Elysium", configManager.GetHadesPath(), configManager.GetResourcesPath());
                _ = RunProcessAsync("Surface", configManager.GetHadesPath(), configManager.GetResourcesPath());
                _ = RunProcessAsync("Styx", configManager.GetHadesPath(), configManager.GetResourcesPath());
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }

        
static async Task<int> RunProcessAsync(string packageName, string hadesPath, string resourcesPath)
        {
            if (!Directory.Exists(resourcesPath + @"\"+packageName))
            {
                Directory.CreateDirectory(resourcesPath + @"\" + packageName);
            }
            else
            {
                Directory.Delete(resourcesPath + @"\" + packageName);
                Directory.CreateDirectory(resourcesPath + @"\" + packageName);
            }
            var tcs = new TaskCompletionSource<int>();
            var process = new Process
            {
                StartInfo = {
                    FileName = @"C:\Users\Alexandre-i5\AppData\Local\Microsoft\WindowsApps\python.exe",
                    Arguments = string.Format("{0} {1} {2} {3}", "\"" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Python\\extract.py\"", "\""+packageName+"\"", "\""+hadesPath+"\"", "\""+resourcesPath+"\""),
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                },
                EnableRaisingEvents = true
            };

            process.Exited += (sender, args) =>
            {
                tcs.SetResult(process.ExitCode);
                process.Dispose();
            };

            process.Start();

            return await tcs.Task;
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
}
