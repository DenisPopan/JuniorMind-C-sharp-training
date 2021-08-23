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
            Program.AddFlowchartElements(commands);
            Assert.Equal(1, Program.Nodes[0].Id);
            Assert.Equal("A", Program.Nodes[0].Text);
            Assert.Equal(2, Program.Nodes[1].Id);
            Assert.Equal("B", Program.Nodes[1].Text);
        }

        [Fact]
        public void AnEdgeShouldBeCreatedWhenTwoNewNodesAreAdded()
        {
            string[] commands = { "Graph TD", "A --- B" };
            Program.AddFlowchartElements(commands);
            Assert.Single(Program.Edges);
            Assert.Equal("A", Program.Edges[0].FirstNode.Text);
            Assert.Equal("B", Program.Edges[0].SecondNode.Text);
        }

        [Fact]
        public void SecondNodesParentShouldBeTheFirstIfBothAreNew()
        {
            string[] commands = { "Graph TD", "A --- B" };
            Program.AddFlowchartElements(commands);
            Assert.Equal("A", Program.Nodes[1].Parent.Text);
        }

        [Fact]
        public void WhenTwoNewNodesAreAddedTheFirstShouldHaveLevel1AndTheSecondLevel2()
        {
            string[] commands = { "Graph TD", "A --- B" };
            Program.AddFlowchartElements(commands);
            Assert.Equal(1, Program.Nodes[0].Level);
            Assert.Equal(2, Program.Nodes[1].Level);
        }

        [Fact]
        public void FirstNodesChildShouldBeTheSecondNodeIfBothAreNew()
        {
            string[] commands = { "Graph TD", "A --- B" };
            Program.AddFlowchartElements(commands);
            Assert.Equal(2, Program.Nodes[0].GetChildren()[0].Id);
        }

        [Fact]
        public void WhenSecondNodeHasLevel1ItShouldSwitchToLevel2()
        {
            string[] commands = { "Graph TD", "A --- B", "C --- A" };
            Program.AddFlowchartElements(commands);
            Assert.Equal(2, Program.Nodes[0].Level);
        }

        [Fact]
        public void WhenANodeChangesLevelAllItsChildrenShouldDoSoToo()
        {
            string[] commands = { "Graph TD", "A --- B", "C --- A" };
            Program.AddFlowchartElements(commands);
            Assert.Equal(2, Program.Nodes[0].Level);
            Assert.Equal(3, Program.Nodes[1].Level);
            Assert.Equal(1, Program.Nodes[2].Level);
        }
    }
}
