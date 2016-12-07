using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Builder.Panels
{
    public class GridBuilder : IBuilder
    {
        private readonly IBuilderFactory _builderFactory;

        public GridBuilder(IBuilderFactory builderFactory)
        {
            _builderFactory = builderFactory;
        }

        [XmlRoot("Grid.RowDefinitions")]
        public class RowDefinitions
        {
            [XmlElement("RowDefinition")]
            public List<RowDefinition> List { get; set; } = new List<RowDefinition>();
        }

        [XmlRoot("Grid.ColumnDefinitions")]
        public class ColumnDefinitions
        {
            [XmlElement("ColumnDefinition")]
            public List<ColumnDefinition> List { get; set; } = new List<ColumnDefinition>();
        }

        public class RowDefinition
        {
            [XmlAttribute]
            public string Height { get; set; }
        }

        public class ColumnDefinition
        {
            [XmlAttribute]
            public string Width { get; set; }
        }

        [XmlRoot("Grid")]
        public class IntermediateGrid
        {
            public Rectangle Contraints => new Rectangle(X, Y, Width, Height);

            [XmlAttribute]
            public int X { get; set; }
            [XmlAttribute]
            public int Y { get; set; }
            [XmlAttribute]
            public int Width { get; set; }
            [XmlAttribute]
            public int Height { get; set; }

            [XmlElement("Grid.RowDefinitions")]
            public RowDefinitions RowDefinitions { get; set; }
            [XmlElement("Grid.ColumnDefinitions")]
            public ColumnDefinitions ColumnDefinitions { get; set; }
        }

        public Grid BuildGridFromNode(XElement element, object controller)
        {
            var grid = ReadColumnsRowsDefinition(element);
            ParseContenComponents(grid, element, controller);
            return grid;
        }

        private void ParseContenComponents(Grid grid, XElement element, object controller)
        {
            var componentElements = element.Elements().Where(x => x.Name != "Grid.RowDefinitions" && x.Name != "Grid.ColumnDefinitions");
            foreach (var componentElement in componentElements)
            {
                var row = componentElement.Attribute("Grid.Row")?.Value ?? "0";
                var column = componentElement.Attribute("Grid.Column")?.Value ?? "0";
                var builder = _builderFactory.GetBuilder(componentElement);
                var component = builder.BuildFromNode(componentElement, controller);

                grid.SetComponent(component, int.Parse(row), int.Parse(column));
            }
        }

        private static Grid ReadColumnsRowsDefinition(XElement element)
        {
            var serializer = new XmlSerializer(typeof(IntermediateGrid));
            var intermediateGrid = (IntermediateGrid) serializer.Deserialize(element.CreateReader());

            var grid = new Grid {Constraints = intermediateGrid.Contraints};

            var columns = intermediateGrid.ColumnDefinitions.List.Select(ToColumnProperty);
            var rows = intermediateGrid.RowDefinitions.List.Select(ToRowProperty);
            grid.AddAllColumns(columns);
            grid.AddAllRows(rows);
            return grid;
        }

        private static RowProperty ToRowProperty(RowDefinition rowDefinition)
        {
            var s = rowDefinition.Height.Trim();
            if(s.ToUpper().Equals("AUTO"))
                return new RowProperty{Type = ValueType.Auto};
            if (s.EndsWith("*"))
                return new RowProperty {Type = ValueType.Percent, Share = int.Parse(s.Replace("*", ""))};

            return new RowProperty {Type = ValueType.Absolute, Height = int.Parse(s)};
        }

        private static ColumnProperty ToColumnProperty(ColumnDefinition columnDefinition)
        {
            var s = columnDefinition.Width.Trim();
            if(s.ToUpper().Equals("AUTO"))
                return new ColumnProperty{Type = ValueType.Auto};
            if (s.EndsWith("*"))
                return new ColumnProperty {Type = ValueType.Percent, Share = int.Parse(s.Replace("*", ""))};

            return new ColumnProperty {Type = ValueType.Absolute, Width = int.Parse(s)};
        }

        public IGuiComponent BuildFromNode(XElement element, object controller)
        {
            return BuildGridFromNode(element, controller);
        }
    }
}