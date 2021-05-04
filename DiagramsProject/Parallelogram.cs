﻿using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Parallelogram : Shape
    {
        const float ShapeEndWidth = 20;
        readonly bool isLeftOriented;

        public Parallelogram(Graphics graphics, string text, Styling styling, PointF position, bool isLeftOriented) : base(graphics, text, styling, position)
        {
            this.isLeftOriented = isLeftOriented;
            Width = Text.Width + 2 * ShapeEndWidth;
            Height = Text.Height;
            Text.Position = new PointF(Position.X + Width / 2, Position.Y + Height / 2);
        }

        public override void DrawShape()
        {
            RectangleF rectangle = new RectangleF(Position.X, Position.Y, Width, Height);
            using GraphicsPath parallelogramPath = new GraphicsPath();

            if (isLeftOriented)
            {
                parallelogramPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Right - ShapeEndWidth, rectangle.Top);
                parallelogramPath.AddLine(rectangle.Right - ShapeEndWidth, rectangle.Top, rectangle.Right, rectangle.Bottom);
                parallelogramPath.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left + ShapeEndWidth, rectangle.Bottom);
                parallelogramPath.CloseFigure();
            }
            else
            {
                parallelogramPath.AddLine(rectangle.Left + ShapeEndWidth, rectangle.Top, rectangle.Right, rectangle.Top);
                parallelogramPath.AddLine(rectangle.Right, rectangle.Top, rectangle.Right - ShapeEndWidth, rectangle.Bottom);
                parallelogramPath.AddLine(rectangle.Right - ShapeEndWidth, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                parallelogramPath.CloseFigure();
            }

            Draw.DrawAndFillShapePathAndText(this, parallelogramPath);
        }
    }
}