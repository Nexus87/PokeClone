using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class ScrollAreaBuilder : AbstractGuiComponentBuilder
    {
        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var scrollArea = container.Resolve<ScrollArea>();
            scrollArea.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, scrollArea, xElement);

            var content = xElement.Elements().SingleOrDefault();
            if (content != null)
                scrollArea.Content = loader.LoadFromXml(content, controller);

            return scrollArea;
        }
    }
}