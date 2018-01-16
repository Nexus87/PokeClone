using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class ScrollAreaBuilder : AbstractGuiComponentBuilder
    {
        private readonly IContainer _container;

        public ScrollAreaBuilder(IContainer container)
        {
            _container = container;
        }

        public override IGuiComponent Build(ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var scrollArea = _container.Resolve<ScrollArea>();
            scrollArea.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, scrollArea, xElement);

            var content = xElement.Elements().SingleOrDefault();
            if (content != null)
                scrollArea.Content = GuiLoader.Builders[content.Name.LocalName].Build(screenConstants, content, controller);

            return scrollArea;
        }
    }
}