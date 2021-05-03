using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace DiagramsProject
{
    public abstract class Shape
    {
        protected Shape(Graphics graphics, string text, Styling styling, PointF position)
        {
            Draw.EnsureIsNotNull(styling, nameof(styling));
            Draw.EnsureIsNotNull(graphics, nameof(graphics));
            Text = text;
            Graphics = graphics;
            Styling = styling;
            Position = position;
            var textMeasurements = graphics.MeasureString(text, styling.DrawFont);
            const float widthAdjustment = 20;
            const float heightAdjustment = 10;
            TextWidth = textMeasurements.Width + widthAdjustment;
            TextHeight = textMeasurements.Height + heightAdjustment;
            TextFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        public string Text { get; }

        public StringFormat TextFormat { get; }

        public Graphics Graphics { get; }

        public float TextWidth { get; }

        public float TextHeight { get; }

        public Styling Styling { get; }

        public PointF Position { get; }

        public float Width { get; protected set; }

        public float Height { get; protected set; }

        public abstract void DrawShape();
    }
}
