using System;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class ListViewBuilder : AbstractGuiComponentBuilder
    {
        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var modelTypeValue = xElement.Attribute("ModelType")?.Value;
            if (modelTypeValue == null)
            {
                throw new InvalidOperationException("Attribute ModelType must be defined");
            }

            var modelType = Type.GetType(modelTypeValue);
            var listView = (IGuiComponent) container.Resolve(typeof(ListView<>).MakeGenericType(modelType));

            listView.Area = ReadPosition(screenConstants, xElement);
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