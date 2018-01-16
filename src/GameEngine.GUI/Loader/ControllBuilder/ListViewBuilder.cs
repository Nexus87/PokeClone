using System;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class ListViewBuilder : AbstractGuiComponentBuilder
    {
        private readonly IContainer _container;

        public ListViewBuilder(IContainer container) 
        {
            _container = container;
        }

        public override IGuiComponent Build(ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var modelTypeValue = xElement.Attribute("ModelType")?.Value;
            if (modelTypeValue == null)
            {
                throw new InvalidOperationException("Attribute ModelType must be defined");
            }

            var modelType = Type.GetType(modelTypeValue);
            var listView = (IGuiComponent) _container.Resolve(typeof(ListView<>).MakeGenericType(modelType));

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