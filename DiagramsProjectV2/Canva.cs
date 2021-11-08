using System.Drawing;

namespace DiagramsProjectV2
{
    public static class Canva
    {
        public static Graphics Graphics { get; private set; }

        public static Bitmap Bitmap { get; private set; }

        public static void InitialiseDrawing(int commandsCount)
        {
            Bitmap = new Bitmap(commandsCount * 114, 2000);
            Graphics = Graphics.FromImage(Bitmap);
            Graphics.Clear(Color.White);
        }

        public static void SaveDrawing(string location)
        {
            Bitmap.Save(location, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
