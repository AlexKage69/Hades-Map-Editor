using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Helper.Managers
{
    class AssetManager
    {
        public static double OVERALL_SCALE = 0.25;//0.125; 1 doesn't work because too high
        private string assetPath;
        private Dictionary<string, SubAtlaseJsonWrapper> allAssets;
        public AssetManager()
        {
            assetPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\data\";
            allAssets = new Dictionary<string, SubAtlaseJsonWrapper>();
            InitAssets();
        }
        public void InitAssets()
        {
            Console.WriteLine("Start Assets");
            var assetFiles = Directory.GetFiles(assetPath + @"Tartarus\manifest");
            foreach (var assetFile in assetFiles)
            {
                using (StreamReader r = new StreamReader(assetFile))
                {
                    string json = r.ReadToEnd();
                    var asset = JsonConvert.DeserializeObject<AtlasJson>(json);
                    Console.WriteLine("Loading:"+asset.GetName());
                    foreach (var assetSub in asset.subAtlases)
                    {
                        int width = (int)(assetSub.rect.width * assetSub.scaleRatio.x), height = (int)(assetSub.rect.height * assetSub.scaleRatio.y);
                        if (!allAssets.ContainsKey(assetSub.GetName()))
                        {
                            Image image = CloneImage(assetPath + @"Tartarus\textures\atlases\" + asset.GetName() + ".png",
                                new Rectangle(assetSub.rect.x, assetSub.rect.y, assetSub.rect.width, assetSub.rect.height), new Size(width, height));
                            SubAtlaseJsonWrapper newAsset = new SubAtlaseJsonWrapper(assetSub, image);
                            allAssets.Add(assetSub.GetName(), newAsset);
                        }
                        else
                        {
                            //Console.WriteLine("Not loaded:"+ assetSub.GetName());
                        }
                    }
                    //break;
                }
            }
            Console.WriteLine("End Assets");
        }
        private Image CloneImage(string filePath, Rectangle cropArea, Size newSize)
        {
            Bitmap copy;
            using (var stream = File.OpenRead(filePath))
            {
                Bitmap orig = (Bitmap)Bitmap.FromStream(stream);
                orig = resizeImage(orig.Clone(cropArea, orig.PixelFormat), new Size(newSize.Width, newSize.Height));
                copy = new Bitmap(orig);
            }
            return copy;
        }
        private static Bitmap resizeImage(Bitmap imgToResize, Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            /*float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;*/
            //New Width  
            int destWidth = (int)(size.Width* OVERALL_SCALE);
            //New Height  
            int destHeight = (int)(size.Height* OVERALL_SCALE);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return b;
        }
        public bool IsAssetExists(string id)
        {
            if (!allAssets.ContainsKey(id))
            {
                //Console.WriteLine("Don't have:"+id);
            }
            return allAssets.ContainsKey(id);
        }
        public string[] GetAllNames()
        {
            return allAssets.Keys.ToArray();
        }
        public Image GetImage(string subAssetName)
        {
            return allAssets[subAssetName].image;
        }
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
