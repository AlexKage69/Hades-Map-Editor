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

namespace Hades_Map_Editor
{
    public static class Utility
    {
        public static Image LoadImage(string filePath, Rect cropArea)
        {
            Bitmap copy;
            using (var stream = File.OpenRead(filePath))
            {
                Bitmap orig = (Bitmap)Bitmap.FromStream(stream);
                copy = orig.Clone(new Rectangle(cropArea.x, cropArea.y, cropArea.width, cropArea.height), orig.PixelFormat);
            }
            return copy;
        }
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public static void FlipImage(Image source, bool xFlip, bool yFlip)
        {
            RotateFlipType rft = xFlip ? (yFlip ? RotateFlipType.RotateNoneFlipXY : RotateFlipType.RotateNoneFlipX) : (yFlip ? RotateFlipType.RotateNoneFlipY : RotateFlipType.RotateNoneFlipNone);
            source.RotateFlip(rft);
            //return source;
        }
        public static string GetPathName(string path)
        {
            return path.Split('\\').Last();
        }
    }
}
