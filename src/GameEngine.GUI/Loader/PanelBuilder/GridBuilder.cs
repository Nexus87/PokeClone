using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GameEngine.Core;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using ValueType = GameEngine.GUI.Panels.ValueType;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class GridBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public GridBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants)
            : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var grid = _registry.ResolveType<Grid>();
            grid.Area = ReadPosition(xElement);
            SetUpController(controller, grid, xElement);
            var rowDefinitions = xElement.Element("Grid.RowDefinitions");
            var columnDefinitions = xElement.Element("Grid.ColumnDefinitions");

            if (rowDefinitions != null)
            {
                grid.AddAllRows(
                    rowDefinitions
                        .Elements("RowDefinition")
                        .Select(ReadProperty)
                        .Select(x => new RowProperty {Type = x.Item1, Share = x.Item2, Height = x.Item3})
                );
            }

            if (columnDefinitions != null)
            {
                grid.AddAllColumns(
                    columnDefinitions
                        .Elements("ColumnDefinition")
                        .Select(ReadProperty)
                        .Select(x => new ColumnProperty {Type = x.Item1, Share = x.Item2, Width = x.Item3})
                );
            }

            return grid;
        }


        private static Tuple<ValueType, int, float> ReadProperty(XElement arg)
        {
            var attribute = arg.Attribute("Height");

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