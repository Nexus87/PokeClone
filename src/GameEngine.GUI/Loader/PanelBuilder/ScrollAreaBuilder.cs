using System.Linq;
using System.Xml.Linq;
using GameEngine.Core;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class ScrollAreaBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public ScrollAreaBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var scrollArea = _registry.ResolveType<ScrollArea>();
            scrollArea.Area = ReadPosition(xElement);
            SetUpController(controller, scrollArea, xElement);

            var content = xElement.Elements().SingleOrDefault();
            if (content != null)
                scrollArea.Content = GuiLoader.Builders[content.Name.LocalName].Build(content, controller);

            return scrollArea;
        }
    }
}