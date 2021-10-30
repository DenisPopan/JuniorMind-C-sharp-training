using DiagramsProjectV2;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ProgramFacts
{
    public class ProgramTests
    {
        [Fact]
        public void NewFlowchartNodesShouldBeAddedToTheList()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            Assert.Equal("A", fl.Nodes[0].Id);
            Assert.Equal("A", fl.Nodes[0].Text);
            Assert.Equal("B", fl.Nodes[1].Id);
            Assert.Equal("B", fl.Nodes[1].Text);
        }

        [Fact]
        public void AnEdgeShouldBeCreatedWhenTwoNewNodesAreAdded()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            Assert.Single(fl.Edges);
            Assert.Equal("A", fl.Edges[0].FirstNode.Text);
            Assert.Equal("B", fl.Edges[0].SecondNode.Text);
        }

        [Fact]
        public void SecondNodesParentShouldBeTheFirstIfBothAreNew()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            Assert.Equal("A", fl.Nodes[1].Parent.Text);
        }

        [Fact]
        public void WhenTwoNewNodesAreAddedTheFirstShouldHaveLevel1AndTheSecondLevel2()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            Assert.Equal(1, fl.Nodes[0].Level);
            Assert.Equal(2, fl.Nodes[1].Level);
        }

        [Fact]
        public void FirstNodesChildShouldBeTheSecondNodeIfBothAreNew()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            var children = fl.Nodes[0].GetChildren();
            Assert.Equal("B", children[0].Id);
        }

        [Fact]
        public void WhenSecondNodeHasLevel1ItShouldSwitchToLevel2()
        {
            string[] commands = { "Graph TD", "A --- B", "C --- A" };
            var fl = new Flowchart(commands);
            Assert.Equal(2, fl.Nodes[0].Level);
        }

        [Fact]
        public void WhenANodeChangesLevelAllItsChildrenShouldDoSoToo()
        {
            string[] commands = { "Graph TD", "A --- B", "C --- A" };
            var fl = new Flowchart(commands);
            Assert.Equal(2, fl.Nodes[0].Level);
            Assert.Equal(3, fl.Nodes[1].Level);
            Assert.Equal(1, fl.Nodes[2].Level);
        }

        [Fact]
        public void IfOnlyFirstNodeExistsThenTheSecondShouldBeOneLevelBelowAndBecomeItsChild()
        {
            string[] commands = { "Graph TD", "A --- B", "A --- C" };
            var fl = new Flowchart(commands);
            Assert.Equal(1, fl.Nodes[0].Level);
            Assert.Equal(2, fl.Nodes[1].Level);
            Assert.Equal(2, fl.Nodes[2].Level);
            var children = fl.Nodes[0].GetChildren();
            Assert.Equal("B", children[0].Id);
            Assert.Equal("C", children[1].Id);
            Assert.Equal("A", children[0].Parent.Id);
            Assert.Equal("A", children[1].Parent.Id);
        }

        [Fact]
        public void IfSecondNodeHasAParentThenItShouldNoLongerBeItsChildIfTheFirstNodeIsHigherInLevel()
        {
            string[] commands = { "Graph TD", "A --- B", "A --- C", "B --- D", "D --- C" };
            var fl = new Flowchart(commands);
            Assert.Equal(1, fl.Nodes[0].Level);
            Assert.Equal(2, fl.Nodes[1].Level);
            Assert.Equal(4, fl.Nodes[2].Level);
            Assert.Equal(3, fl.Nodes[3].Level);
            var children = fl.Nodes[0].GetChildren();
            Assert.Equal("B", children[0].Id);
            Assert.Equal(1, fl.Nodes[0].GetChildrenCount());
            Assert.Equal(1, fl.Nodes[3].GetChildrenCount());
            var children1 = fl.Nodes[3].GetChildren();
            Assert.Equal("C", children1[0].Id);
            Assert.Equal("D", children1[0].Parent.Id);
        }

        [Fact]
        public void MoveToMethodShouldMoveAListElementToAGivenPositionInTheList()
        {
            string[] commands = { "Graph TD", "A --- B", "A --- C", "B --- D" };
            var fl = new Flowchart(commands);
            Assert.Equal(0, fl.Nodes.FindIndex(x => x.Id.Equals("A")));

            fl.MoveNodeTo(fl.Nodes.Find(x => x.Id.Equals("A")), 2);
            Assert.Equal(0, fl.Nodes.FindIndex(x => x.Id.Equals("B")));
            Assert.Equal(2, fl.Nodes.FindIndex(x => x.Id.Equals("A")));
            Assert.Equal(1, fl.Nodes.FindIndex(x => x.Id.Equals("C")));
            Assert.Equal(3, fl.Nodes.FindIndex(x => x.Id.Equals("D")));

            fl.MoveNodeTo(fl.Nodes.Find(x => x.Id.Equals("A")), 3);
            Assert.Equal(0, fl.Nodes.FindIndex(x => x.Id.Equals("B")));
            Assert.Equal(3, fl.Nodes.FindIndex(x => x.Id.Equals("A")));
            Assert.Equal(1, fl.Nodes.FindIndex(x => x.Id.Equals("C")));
            Assert.Equal(2, fl.Nodes.FindIndex(x => x.Id.Equals("D")));

            Assert.Throws<ArgumentOutOfRangeException>(() => fl.MoveNodeTo(fl.Nodes.Find(x => x.Id.Equals("A")), 4));
        }
    }
}
