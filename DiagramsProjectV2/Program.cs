﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DiagramsProjectV2
{
    public static class Program
    {
        public static List<Node> Nodes { get; } = new List<Node>();

        public static List<Edge> Edges { get; } = new List<Edge>();

        public static int LastNodeId { get; set; }

        public static void AddFlowchartElements(string[] commands)
        {
            ProjectUtils.EnsureIsNotNull(commands, nameof(commands));

            string[] nodesText;
            Node firstNode;
            Node secondNode;

            for (int i = 1; i < commands.Length; i++)
            {
                nodesText = commands[i].Split(" --- ");
                firstNode = Nodes.Find(x => x.Text.Equals(nodesText[0]));
                if (firstNode == null)
                {
                    firstNode = AddNode(nodesText[0]);
                    secondNode = AddNode(nodesText[1]);

                    if (secondNode.Level == 1 || secondNode.Level == 0)
                    {
                        secondNode.Parent = firstNode;
                        secondNode.Level = 2;
                        firstNode.AddChild(secondNode);
                    }

                    firstNode.Level = secondNode.Level - 1;
                    AddEdge(firstNode, secondNode);
                }
            }
        }

        static Node AddNode(string text)
        {
            var node = Nodes.Find(x => x.Text.Equals(text));
            if (node == null)
            {
                Nodes.Add(new Node(Nodes.Count + 1, text));
                node = Nodes[Nodes.Count - 1];
            }

            return node;
        }

        static void AddEdge(Node firstNode, Node secondNode)
        {
            Edges.Add(new Edge(firstNode, secondNode));
        }

        static void Main(string[] args)
        {
            string[] commands;
            try
            {
                commands = File.ReadAllLines(args[0]);
            }
            catch (IOException)
            {
                Console.WriteLine("File path does not exist or it is incorrect!");
                return;
            }

            // Initialising bitmap and styling class instance
            using Bitmap bmp = new Bitmap(1920, 1080);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            var styling = new Styling();
            styling.Graphics = g;

            AddFlowchartElements(commands);

            Nodes[0].Build(g, 50, 50);
            Nodes[1].Build(g, 50, 150);

            DrawSimpleRectangle(Nodes[0].Text, Nodes[0].Rectangle, styling);
            DrawSimpleRectangle(Nodes[1].Text, Nodes[1].Rectangle, styling);
            DrawLink(Nodes[0].Rectangle, Nodes[1].Rectangle, styling);

            bmp.Save(args[1], System.Drawing.Imaging.ImageFormat.Png);
        }

        static void DrawSimpleRectangle(string text, RectangleF rectangle, Styling styling)
        {
            SizeF stringSize = styling.Graphics.MeasureString(text, styling.Font);

            const float widthAdjustment = 30;
            const float heightAdjustment = 10;
            styling.Graphics.FillRectangle(styling.ShapeBrush, rectangle);
            styling.Graphics.DrawRectangle(styling.ShapePen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            styling.Graphics.DrawString(
                text,
                styling.Font,
                styling.TextBrush,
                rectangle.X + (stringSize.Width + widthAdjustment) / 2,
                rectangle.Y + (stringSize.Height + heightAdjustment) / 2,
                styling.Format);
        }

        static void DrawLink(RectangleF rect1, RectangleF rect2, Styling styling)
        {
            styling.Graphics.DrawLine(styling.ShapePen, rect1.Left + rect1.Width / 2, rect1.Bottom, rect2.Left + rect2.Width / 2, rect2.Top);
        }
    }
}
