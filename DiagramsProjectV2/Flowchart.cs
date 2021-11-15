using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DiagramsProjectV2
{
    public class Flowchart
    {
        float currentLevelHeightEndPoint;
        int levels;

        public Flowchart(string[] commands)
        {
            ProjectUtils.EnsureIsNotNull(commands, nameof(commands));
            AddFlowchartElements(commands);
            currentLevelHeightEndPoint = 0;
        }

        public List<Node> Nodes { get; private set; } = new List<Node>();

        public List<Edge> Edges { get; } = new List<Edge>();

        public void DrawFlowchart(string location)
        {
            ////in phase 3 we need to treat the case related to phase 1
            ////cazul cand nodurile au root diferit(aici s-ar putea sa fie rezolvat deja)

            FixNodesListOrder();

            FindChildrenWidth();

            SetCoordinatesForEachLevel();

            TreatSpecialCases();

            Canva.UpdateBitmapSize(FindFurthestNodeRectangleRight() + 50, (int)currentLevelHeightEndPoint + 50);

            DrawNodes();

            DrawEdges();

            Canva.SaveDrawing(location);
        }

        void AddFlowchartElements(string[] commands)
        {
            Canva.InitialiseDrawing();
            string[] nodesText;
            Node firstNode;
            Node secondNode;

            for (int i = 1; i < commands.Length; i++)
            {
                nodesText = commands[i].Split(" --- ");
                firstNode = AddNode(nodesText[0]);
                secondNode = AddNode(nodesText[1]);
                if (firstNode.Level == 0)
                {
                    if (secondNode.Level == 0 || secondNode.Level == 1 || secondNode.Level == 2)
                    {
                        firstNode.Level = 1;
                        SetParentChildRelationship(firstNode, secondNode);
                    }
                }
                else
                {
                    if (secondNode.Level == 0)
                    {
                        SetParentChildRelationship(firstNode, secondNode);
                    }

                    if (firstNode.Level >= secondNode.Level)
                    {
                        SetParentChildRelationship(firstNode, secondNode);
                    }
                }

                UpdateLevelsNumber(secondNode);

                AddEdge(firstNode, secondNode);
            }
        }

        Node AddNode(string text)
        {
            var node = Nodes.Find(x => x.Text.Equals(text));
            if (node == null)
            {
                Nodes.Add(new Node(text, text, this));
                node = Nodes[^1];
            }

            return node;
        }

        void AddEdge(Node firstNode, Node secondNode)
        {
            Edges.Add(new Edge(firstNode, secondNode));
        }

        void SetParentChildRelationship(Node firstNode, Node secondNode)
        {
            secondNode.Parent = firstNode;
            secondNode.Level = firstNode.Level + 1;
        }

        private void UpdateLevelsNumber(Node secondNode)
        {
            if (levels >= secondNode.Level)
            {
                return;
            }

            levels = secondNode.Level;
        }

        private void FixNodesListOrder()
        {
            List<Node> tempList = new ();
            tempList.AddRange(Nodes.Where(x => x.Level == 1));
            for (int i = 1; i < levels; i++)
            {
                foreach (var node in tempList.Where(x => x.Level == i).ToList())
                {
                    tempList.AddRange(node.GetChildren());
                }
            }

            Nodes = new List<Node>(tempList);
        }

        private void FindChildrenWidth()
        {
            foreach (var node in Nodes.OrderByDescending(x => x.Level))
            {
                var children = Nodes.Where(x => x.Level > 1 && x.Parent.Equals(node));
                node.ChildrenWidth = CalculateChildrenWidth(children);
            }
        }

        private float CalculateChildrenWidth(IEnumerable<Node> children)
        {
            if (!children.Any())
            {
                return 0;
            }

            float width = -100;
            foreach (var child in children)
            {
                width = width + Math.Max(child.Width, child.ChildrenWidth) + 100;
            }

            return width;
        }

        private void SetCoordinatesForEachLevel()
        {
            currentLevelHeightEndPoint = 50;
            float startY = currentLevelHeightEndPoint;
            foreach (var levelGroup in Nodes.GroupBy(x => x.Level))
            {
                if (levelGroup.Key == 1)
                {
                    SetNodesCoordinates(50, 50, levelGroup);
                }
                else
                {
                    foreach (var groupedByParent in levelGroup.GroupBy(x => x.Parent))
                    {
                        SetNodesCoordinates(
                            groupedByParent.Key.Rectangle.Right - groupedByParent.Key.Width / 2 - groupedByParent.Key.ChildrenWidth / 2,
                            startY,
                            groupedByParent);
                    }
                }

                startY = currentLevelHeightEndPoint + 130;
            }
        }

        private void SetNodesCoordinates(float startX, float startY, IEnumerable<Node> children)
        {
            foreach (var node in children)
            {
                if (node.Width > node.ChildrenWidth)
                {
                    node.Rectangle = new RectangleF(startX, startY, node.Width, node.Height);
                    startX = node.Rectangle.Right + 100;
                }
                else
                {
                    node.Rectangle = new RectangleF(startX + node.ChildrenWidth / 2 - node.Width / 2, startY, node.Width, node.Height);
                    startX = startX + node.ChildrenWidth + 100;
                }

                UpdateLevelHeightEndPoint(node);
            }
        }

        private void UpdateLevelHeightEndPoint(Node node)
        {
            if (node.Rectangle.Bottom <= currentLevelHeightEndPoint)
            {
                return;
            }

            currentLevelHeightEndPoint = node.Rectangle.Bottom;
        }

        private void TreatSpecialCases()
        {
            int leftPillarListPosition;
            int rightPillarListPosition;

            foreach (var edgesGroupedBySecondNode in Edges.OrderBy(x => x.SecondNode.Level).GroupBy(x => x.SecondNode).Where(x => NodeHasMoreThanOneEdge(x)))
            {
                leftPillarListPosition = Nodes.Count + 1;
                rightPillarListPosition = 0;

                FindPillarsListPosition(ref leftPillarListPosition, ref rightPillarListPosition, edgesGroupedBySecondNode.ToList());

                float midPillarsPoint = (Nodes[leftPillarListPosition].Rectangle.Right + Nodes[rightPillarListPosition].Rectangle.Left) / 2;

                if (AreNeighboursAndHaveSimilarChildrenNumber(leftPillarListPosition, rightPillarListPosition))
                {
                    MoveNodeToClosestNeighbourToPillarsMidPointPosition(edgesGroupedBySecondNode.Key, midPillarsPoint, edgesGroupedBySecondNode.Key.Parent);
                    SetCoordinatesForEachLevel();
                    continue;
                }

                var (closestParentToPillarsMidPoint, closestParentDistanceToPillarsMidPoint) =
                    FindClosestNodeAndItsDistanceToMidPillarsPoint(
                        midPillarsPoint,
                        Nodes.Where(x => x.ListPosition >= leftPillarListPosition && x.ListPosition <= rightPillarListPosition));
                var (closestNeighbourToPillarsMidPoint, closestNeighbourDistanceToPillarsMidPoint) = FindClosestNodeAndItsDistanceToMidPillarsPoint(
                    midPillarsPoint,
                    Nodes.Where(x => x.Level > 1 && x.Parent.ListPosition >= leftPillarListPosition && x.Parent.ListPosition <= rightPillarListPosition));

                if (closestParentDistanceToPillarsMidPoint > closestNeighbourDistanceToPillarsMidPoint)
                {
                    edgesGroupedBySecondNode.Key.Parent = closestNeighbourToPillarsMidPoint.Parent;
                    MoveNodeBasedOnListPosition(edgesGroupedBySecondNode.Key, midPillarsPoint, closestNeighbourToPillarsMidPoint);
                }
                else
                {
                    MoveNodeToClosestNeighbourToPillarsMidPointPosition(edgesGroupedBySecondNode.Key, midPillarsPoint, closestParentToPillarsMidPoint);
                }

                ////Canva.Graphics.DrawLine(BasicStyling.EdgePen, midPillarsDistance, 300, midPillarsDistance, 800);
                FixNodesListOrder();
                FindChildrenWidth();
                SetCoordinatesForEachLevel();
            }
        }

        private bool AreNeighboursAndHaveSimilarChildrenNumber(int leftPillarListPosition, int rightPillarListPosition)
        {
            return leftPillarListPosition == rightPillarListPosition - 1 &&
                Math.Abs(Nodes[leftPillarListPosition].GetChildrenCount() - Nodes[rightPillarListPosition].GetChildrenCount()) < 2;
        }

        private bool NodeHasMoreThanOneEdge(IEnumerable<Edge> upperEdges)
        {
            return upperEdges.Count() > 1;
        }

        private void FindPillarsListPosition(ref int leftPillarListPosition, ref int rightPillarListPosition, List<Edge> upperEdges)
        {
            foreach (var upperEdge in upperEdges)
            {
                var firstNodeListPosition = upperEdge.FirstNode.ListPosition;
                if (firstNodeListPosition < leftPillarListPosition)
                {
                    leftPillarListPosition = firstNodeListPosition;
                }

                if (firstNodeListPosition > rightPillarListPosition)
                {
                    rightPillarListPosition = firstNodeListPosition;
                }
            }
        }

        private (Node, float) FindClosestNodeAndItsDistanceToMidPillarsPoint(float midPoint, IEnumerable<Node> nodes)
        {
            Node nodeToReturn = nodes.First();
            float minimumDistance = 9999999;
            foreach (var node in nodes)
            {
                if (node.Rectangle.X < midPoint)
                {
                    var distance = Math.Abs(midPoint - node.Rectangle.Right);
                    if (distance < minimumDistance)
                    {
                        minimumDistance = distance;
                        nodeToReturn = node;
                    }
                }
                else
                {
                    var distance = Math.Abs(node.Rectangle.Left - midPoint);
                    if (distance < minimumDistance)
                    {
                        minimumDistance = distance;
                        nodeToReturn = node;
                    }
                }
            }

            return (nodeToReturn, minimumDistance);
        }

        private void MoveNodeToClosestNeighbourToPillarsMidPointPosition(Node nodeToMove, float midPillarsPoint, Node closestParentNodeToMidPillarsPoint)
        {
            if (!HasChildren(closestParentNodeToMidPillarsPoint))
            {
                nodeToMove.Parent = closestParentNodeToMidPillarsPoint;
            }
            else
            {
                nodeToMove.Parent = closestParentNodeToMidPillarsPoint;
                var closestChildToMidPoint = FindClosestNodeAndItsDistanceToMidPillarsPoint(midPillarsPoint, closestParentNodeToMidPillarsPoint.GetChildren()).Item1;
                MoveNodeBasedOnListPosition(nodeToMove, midPillarsPoint, closestChildToMidPoint);
            }
        }

        private void MoveNodeBasedOnListPosition(Node nodeToMove, float midPillarsPoint, Node closestChildToMidPoint)
        {
            if (nodeToMove.ListPosition < closestChildToMidPoint.ListPosition)
            {
                MoveNodeTo(nodeToMove, closestChildToMidPoint.Rectangle.X < midPillarsPoint ? closestChildToMidPoint.ListPosition : closestChildToMidPoint.ListPosition - 1);
            }
            else
            {
                MoveNodeTo(nodeToMove, closestChildToMidPoint.Rectangle.X < midPillarsPoint ? closestChildToMidPoint.ListPosition + 1 : closestChildToMidPoint.ListPosition);
            }
        }

        void MoveNodeTo(Node node, int index)
        {
            var item = node;
            Nodes.Remove(node);
            Nodes.Insert(index, item);
        }

        private bool HasChildren(Node node)
        {
            return node.GetChildrenCount() > 0;
        }

        int FindFurthestNodeRectangleRight()
        {
            float furthestNodeRight = -1;
            foreach (var nodesGroup in Nodes.GroupBy(x => x.Level))
            {
                var lastNodeFromLevel = nodesGroup.Last();
                if (lastNodeFromLevel.Rectangle.Right > furthestNodeRight)
                {
                    furthestNodeRight = lastNodeFromLevel.Rectangle.Right;
                }
            }

            return (int)furthestNodeRight;
        }

        void DrawNodes()
        {
            foreach (var node in Nodes)
            {
                Program.DrawSimpleRectangle(node.Text, node.Rectangle);
            }
        }

        private void DrawEdges()
        {
            foreach (var edge in Edges)
            {
                Program.DrawLink(edge.FirstNode.Rectangle, edge.SecondNode.Rectangle);
            }
        }
    }
}