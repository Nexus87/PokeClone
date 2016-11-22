﻿using System.Xml.Linq;
using GameEngine.GUI.Builder;
using GameEngine.GUI.Builder.Panels;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace GameEngine.GUI.Test.Builder.Panels
{
    [TestFixture]
    public class GridBuilderTest
    {
        private const string EmptyGridXml =
            "<Grid>" +
            "    <Grid.RowDefinitions>" +
            "        <RowDefinition Height=\"1*\"/>" +
            "        <RowDefinition Height=\"1*\"/>" +
            "    </Grid.RowDefinitions>" +
            "    <Grid.ColumnDefinitions>" +
            "        <ColumnDefinition Width=\"400\"/>" +
            "        <ColumnDefinition Width=\"300\"/>" +
            "    </Grid.ColumnDefinitions>" +
            "</Grid>";

        [TestCase(EmptyGridXml, 2, 2)]
        public void BuildGridFromNode_EmptyGridWithRowsAndColumns_AsExpected(string xml, int rows, int columns)
        {
            var xmlDocument = XDocument.Parse(xml);
            var builder = CreateBuilder();

            var grid = builder.BuildGridFromNode(xmlDocument.Root);

            Assert.AreEqual(rows, grid.Rows);
            Assert.AreEqual(columns, grid.Columns);
        }

        private const string FullGridXml =
            "<Grid>" +
            "    <Grid.RowDefinitions>" +
            "        <RowDefinition Height=\"1*\"/>" +
            "        <RowDefinition Height=\"1*\"/>" +
            "    </Grid.RowDefinitions>" +
            "    <Grid.ColumnDefinitions>" +
            "        <ColumnDefinition Width=\"400\"/>" +
            "        <ColumnDefinition Width=\"300\"/>" +
            "    </Grid.ColumnDefinitions>" +
            "    <DummyLabel Text=\"T00\" Grid.Row = \"0\" Grid.Column = \"0\"/>" +
            "    <DummyLabel Text=\"T10\" Grid.Row = \"1\" Grid.Column = \"0\"/>" +
            "    <DummyLabel Text=\"T01\" Grid.Row = \"0\" Grid.Column = \"1\"/>" +
            "    <DummyLabel Text=\"T11\" Grid.Row = \"1\" Grid.Column = \"1\"/>" +
            "</Grid>";

        private const string FullGridDefaultRowColumnValuesXml =
            "<Grid>" +
            "    <Grid.RowDefinitions>" +
            "        <RowDefinition Height=\"1*\"/>" +
            "        <RowDefinition Height=\"1*\"/>" +
            "    </Grid.RowDefinitions>" +
            "    <Grid.ColumnDefinitions>" +
            "        <ColumnDefinition Width=\"400\"/>" +
            "        <ColumnDefinition Width=\"300\"/>" +
            "    </Grid.ColumnDefinitions>" +
            "    <DummyLabel Text=\"T00\"/>" +
            "    <DummyLabel Text=\"T10\" Grid.Row = \"1\"/>" +
            "    <DummyLabel Text=\"T01\" Grid.Column = \"1\"/>" +
            "    <DummyLabel Text=\"T11\" Grid.Row = \"1\" Grid.Column = \"1\"/>" +
            "</Grid>";

        [TestCase(FullGridXml, 2, 2)]
        [TestCase(FullGridDefaultRowColumnValuesXml, 2, 2)]
        public void BuildGridFromNode_FullGrid_AsExpected(string xml, int rows, int columns)
        {
            var xmlDocument = XDocument.Parse(xml);
            var builder = CreateBuilder();

            var grid = builder.BuildGridFromNode(xmlDocument.Root);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    Assert.IsInstanceOf<DummyLabel>(grid.GetComponent(i, j));
                    Assert.AreEqual(("T" + i) + j, ((DummyLabel) grid.GetComponent(i, j)).Text);
                }
            }
        }

        private const string GridWithSize =
            "<Grid X =\"10\" Y=\"20\" Width=\"500\" Height=\"200\">" +
            "    <Grid.RowDefinitions>" +
            "        <RowDefinition Height=\"1*\"/>" +
            "        <RowDefinition Height=\"1*\"/>" +
            "    </Grid.RowDefinitions>" +
            "    <Grid.ColumnDefinitions>" +
            "        <ColumnDefinition Width=\"400\"/>" +
            "        <ColumnDefinition Width=\"300\"/>" +
            "    </Grid.ColumnDefinitions>" +
            "</Grid>";

        [TestCase(GridWithSize, 10, 20, 500, 200)]
        public void Constraint_GridWithSize_AsExpected(string xml, int x, int y, int width, int height)
        {
            var expectedContraint = new Rectangle(x, y, width, height);
            var xmlDocument = XDocument.Parse(xml);
            var builder = CreateBuilder();

            var grid = builder.BuildGridFromNode(xmlDocument.Root);

            Assert.AreEqual(expectedContraint, grid.Constraints);
        }

        private static GridBuilder CreateBuilder()
        {
            var builderFactory = new Mock<IBuilderFactory>();
            var builder = new Mock<IBuilder>();

            builder.Setup(o => o.BuildFromNode(It.IsAny<XElement>()))
                .Returns<XElement>(element => new DummyLabel{Text = element.Attribute("Text")?.Value});
            builderFactory.Setup(o => o.GetBuilder(It.IsAny<XElement>())).Returns(builder.Object);

            return new GridBuilder(builderFactory.Object);
        }
    }

    public class DummyLabel : AbstractGraphicComponent
    {
        public string Text { get; set; }
        public override void HandleKeyInput(CommandKeys key)
        {
            throw new System.NotImplementedException();
        }
    }
}