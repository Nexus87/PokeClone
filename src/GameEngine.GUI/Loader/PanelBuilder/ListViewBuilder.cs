using System;
using System.Collections.Generic;
using System.Xml.Linq;
using GameEngine.Core;
using GameEngine.GUI.Controlls;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class ListViewBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public ListViewBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var modelTypeValue = xElement.Attribute("ModelType")?.Value;
            if (modelTypeValue == null)
            {
                throw new InvalidOperationException("Attribute ModelType must be defined");
            }

            var modelType = Type.GetType(modelTypeValue);
            var listView = (IGuiComponent) _registry.ResolveGenericType(typeof(ListView<>), modelType);

            listView.Area = ReadPosition(xElement);
            SetUpController(controller, listView, xElement);

            var cellHeightAttribute = xElement.Attribute("CellHeight");
            if (cellHeightAttribute != null)
            {
                listView
                    .GetType()
                    .GetProperty(nameof(ListView<object>.CellHeight))
                    .SetValue(listView, int.Parse(cellHeightAttribute.Value));
            }
            return listView;
        }
    }
}