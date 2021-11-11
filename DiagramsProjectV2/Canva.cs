using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DiagramsProjectV2
{
    public static class Canva
    {
        public static Graphics Graphics { get; private set; }

        public static Bitmap Bitmap { get; private set; }

        public static void InitialiseDrawing()
        {
            Bitmap = new Bitmap(5, 5);
            Graphics = Graphics.FromImage(Bitmap);
            Graphics.Clear(Color.White);
        }

        public static void UpdateBitmapSize(int width, int height)
        {
            Bitmap = new Bitmap(Bitmap, width, height);
            Graphics = Graphics.FromImage(Bitmap);
            Graphics.Clear(Color.White);
        }

        public static void SaveDrawing(string location)
        {
            var finalImage = ResizeImage(Bitmap, Bitmap.Width / 2, Bitmap.Height / 2);
            finalImage.Save(location, ImageFormat.Png);
            finalImage.Dispose();
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            ProjectUtils.EnsureIsNotNull(image, nameof(image));
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
    }
}
