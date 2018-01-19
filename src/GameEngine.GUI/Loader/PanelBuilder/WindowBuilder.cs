using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class WindowBuilder : AbstractGuiComponentBuilder
    {

        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var window = container.Resolve<Window>();
            window.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, window, xElement);

            var contentXElement = xElement.Elements().SingleOrDefault();
            if (contentXElement != null)
                window.Content = loader.LoadFromXml(contentXElement, controller);

            return window;
        }
    }
}