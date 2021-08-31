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
            Assert.Equal(2, fl.Nodes[0].GetChildren()[0].Id);
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
            Assert.Equal(2, children[0].Id);
            Assert.Equal(3, children[1].Id);
            Assert.Equal(1, children[0].Parent.Id);
            Assert.Equal(1, children[1].Parent.Id);
        }

        [Fact]
        public void Test()
        {
            string[] commands =
            {
                "Graph TD",
                "Christmas --- Go shopping",
                "Go shopping --- Let me think",
                "Let me think --- Laptop",
                "Go shopping --- G",
                "Go shopping --- H",
                "H --- M",
                "H --- z",
                "H --- N",
                "Christmas --- bsd",
                "bsd --- Let me think",
                "bsd --- G",
                "bsd --- H",
                "bsd --- nn",
                "bsd --- mna",
                "Go shopping --- asd",
                "Let me think --- N",
                "Christmas --- BGH",
                "Christmas --- HGN"
            };
            var fl = new Flowchart(commands);
        }
    }
}
