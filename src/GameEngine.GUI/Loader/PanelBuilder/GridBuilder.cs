using System;
using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;
using ValueType = GameEngine.GUI.Panels.ValueType;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class GridBuilder : AbstractGuiComponentBuilder
    {
        private readonly IContainer _container;

        public GridBuilder(IContainer container)
        {
            _container = container;
        }

        public override IGuiComponent Build(ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var grid = _container.Resolve<Grid>();
            grid.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, grid, xElement);
            var rowDefinitions = xElement.Element("Grid.RowDefinitions");
            var columnDefinitions = xElement.Element("Grid.ColumnDefinitions");

            if (rowDefinitions != null)
            {
                grid.AddAllRows(
                    rowDefinitions
                        .Elements("RowDefinition")
                        .Select(x => ReadProperty(x, "Height"))
                        .Select(x => new RowProperty {Type = x.Item1, Share = x.Item2, Height = x.Item3})
                );
            }

            if (columnDefinitions != null)
            {
                grid.AddAllColumns(
                    columnDefinitions
                        .Elements("ColumnDefinition")
                        .Select(x => ReadProperty(x, "Width"))
                        .Select(x => new ColumnProperty {Type = x.Item1, Share = x.Item2, Width = x.Item3})
                );
            }

            var contentElements = xElement.Elements()
                .Where(x => x.Name.LocalName != "Grid.RowDefinitions")
                .Where(x => x.Name.LocalName != "Grid.ColumnDefinitions");
            foreach (var content in contentElements)
            {
                AddContent(screenConstants, grid, content, controller);
            }
            return grid;
        }

        private static void AddContent(ScreenConstants screenConstants, Grid grid, XElement content, object controller)
        {
            var row = content.Attribute("Grid.Row")?.Value ?? "0";
            var column = content.Attribute("Grid.Column")?.Value ?? "0";
            var rowSpan = content.Attribute("Grid.RowSpan")?.Value ?? "1";
            var columnSpan = content.Attribute("Grid.ColumnSpan")?.Value ?? "1";

            var component = GuiLoader.Builders[content.Name.LocalName].Build(screenConstants, content, controller);

            grid.SetComponent(component, int.Parse(row), int.Parse(column), int.Parse(rowSpan), int.Parse(columnSpan));
        }


        private static Tuple<ValueType, int, float> ReadProperty(XElement arg, string propertyName)
        {
            var attribute = arg.Attribute(propertyName);

            if (attribute == null)
                return Tuple.Create(ValueType.Percent, 1, 0f);

            if (attribute.Value == "Auto")
                return Tuple.Create(ValueType.Auto, 0, 0f);

            if (attribute.Value.EndsWith("*"))
            {
                var valueString = attribute.Value.Replace("*", "").Trim();
                return Tuple.Create
                (
                    ValueType.Percent,
                    valueString.Length == 0 ? 1 : int.Parse(valueString),
                    0f
                );
            }

            return Tuple.Create(
                ValueType.Absolute,
                0,
                float.Parse(attribute.Value.Trim())
            );
        }
    }
}