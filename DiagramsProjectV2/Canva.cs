using System.Drawing;

namespace DiagramsProjectV2
{
    public static class Canva
    {
        public static Graphics Graphics { get; private set; }

        public static Bitmap Bitmap { get; } = new Bitmap(4000, 2000);

        public static void InitialiseDrawing()
        {
            Graphics = Graphics.FromImage(Bitmap);
            Graphics.Clear(Color.White);
        }

        public static void SaveDrawing(string location)
        {
            Bitmap.Save(location, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
