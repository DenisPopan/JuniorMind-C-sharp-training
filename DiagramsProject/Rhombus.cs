﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Rhombus : Shape
    {
        public Rhombus(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = Math.Max(Text.Width, Text.Height);
            Height = Width;
            Text.Position = new PointF(Position.X + Width / 2, Position.Y + Width / 2);
        }

        public override void DrawShape()
        {
            var px = Position.X + Width / 2;
            var py = Position.Y;
            var diagonal = Width;
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