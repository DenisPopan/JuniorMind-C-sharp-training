using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Rhombus : Shape
    {
        public Rhombus(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling)
        {
            float width = Math.Max(Text.Width, Text.Height);
            Bounds = new RectangleF(position.X, position.Y, width, width);
            float halfWidth = Bounds.Width / 2;
            float halfHeight = Bounds.Height / 2;
            Text.Position = new PointF(position.X + halfWidth, position.Y + halfHeight);
        }

        public override void DrawShape()
        {
            var px = Bounds.X + Bounds.Width / 2;
            var py = Bounds.Y;
            var diagonal = Bounds.Width;
            var halfDiagonal = diagonal / 2;
            using var path = new GraphicsPath();
            path.AddLines(new[]
            {
                new PointF(px, py),
                new PointF(px + halfDiagonal, py + halfDiagonal),
                new PointF(px, py + diagonal),
                new PointF(px - halfDiagonal, py + halfDiagonal)
            });
            path.CloseFigure();
            Draw.DrawAndFillShapePathAndText(this, path);
        }
    }
}
