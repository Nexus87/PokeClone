using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class ScrollAreaBuilder : GuiComponentBuilder
    {
        private readonly IContainer _container;

        public ScrollAreaBuilder(IContainer container, ScreenConstants screenConstants) : base(screenConstants)
        {
            _container = container;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var scrollArea = _container.Resolve<ScrollArea>();
            scrollArea.Area = ReadPosition(xElement);
            SetUpController(controller, scrollArea, xElement);

            var content = xElement.Elements().SingleOrDefault();
            if (content != null)
                scrollArea.Content = GuiLoader.Builders[content.Name.LocalName].Build(content, controller);

            return scrollArea;
        }
    }
}