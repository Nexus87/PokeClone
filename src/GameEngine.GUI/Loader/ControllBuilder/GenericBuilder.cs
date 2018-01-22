using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class GenericBuilder<T>  : AbstractGuiComponentBuilder where T : IGuiComponent
    {
        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement,
            object controller)
        {
            var component = container.Resolve<T>();
            component.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, component, xElement);
            MapElementsToProperties(xElement, component);

            return component;
        }
    }
}