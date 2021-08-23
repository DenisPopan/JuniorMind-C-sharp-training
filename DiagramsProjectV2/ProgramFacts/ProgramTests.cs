using DiagramsProjectV2;
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
            fl.AddFlowchartElements(commands);
            Assert.Equal(1, fl.Nodes[0].Id);
            Assert.Equal("A", fl.Nodes[0].Text);
            Assert.Equal(2, fl.Nodes[1].Id);
            Assert.Equal("B", fl.Nodes[1].Text);
        }

        [Fact]
        public void AnEdgeShouldBeCreatedWhenTwoNewNodesAreAdded()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            fl.AddFlowchartElements(commands);
            Assert.Single(fl.Edges);
            Assert.Equal("A", fl.Edges[0].FirstNode.Text);
            Assert.Equal("B", fl.Edges[0].SecondNode.Text);
        }

        [Fact]
        public void SecondNodesParentShouldBeTheFirstIfBothAreNew()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            fl.AddFlowchartElements(commands);
            Assert.Equal("A", fl.Nodes[1].Parent.Text);
        }

        [Fact]
        public void WhenTwoNewNodesAreAddedTheFirstShouldHaveLevel1AndTheSecondLevel2()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            fl.AddFlowchartElements(commands);
            Assert.Equal(1, fl.Nodes[0].Level);
            Assert.Equal(2, fl.Nodes[1].Level);
        }

        [Fact]
        public void FirstNodesChildShouldBeTheSecondNodeIfBothAreNew()
        {
            string[] commands = { "Graph TD", "A --- B" };
            var fl = new Flowchart(commands);
            fl.AddFlowchartElements(commands);
            Assert.Equal(2, fl.Nodes[0].GetChildren()[0].Id);
        }

        [Fact]
        public void WhenSecondNodeHasLevel1ItShouldSwitchToLevel2()
        {
            string[] commands = { "Graph TD", "A --- B", "C --- A" };
            var fl = new Flowchart(commands);
            fl.AddFlowchartElements(commands);
            Assert.Equal(2, fl.Nodes[0].Level);
        }

        [Fact]
        public void WhenANodeChangesLevelAllItsChildrenShouldDoSoToo()
        {
            string[] commands = { "Graph TD", "A --- B", "C --- A" };
            var fl = new Flowchart(commands);
            fl.AddFlowchartElements(commands);
            Assert.Equal(2, fl.Nodes[0].Level);
            Assert.Equal(3, fl.Nodes[1].Level);
            Assert.Equal(1, fl.Nodes[2].Level);
        }
    }
}
