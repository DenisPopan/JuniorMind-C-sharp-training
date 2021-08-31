using System;
using System.Drawing;

namespace DiagramsProjectV2
{
    public static class Canva
    {
        public static Graphics Graphics { get; private set; }

        public static Bitmap Bitmap { get; private set; } = new Bitmap(1920, 1080);

        public static void InitialiseDrawing()
        {
            Bitmap = new Bitmap(1920, 1080);
            Graphics = Graphics.FromImage(Bitmap);
            Graphics.Clear(Color.White);
        }

        public static void SaveDrawing(string location)
        {
            Bitmap.Save(location, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
