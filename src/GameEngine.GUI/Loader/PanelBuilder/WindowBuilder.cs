using System.Linq;
using System.Xml.Linq;
using GameEngine.Core;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class WindowBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;



        public WindowBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) :
            base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var window = _registry.ResolveType<Window>();
            window.Area = ReadPosition(xElement);
            SetUpController(controller, window, xElement);

            var contentXElement = xElement.Elements().SingleOrDefault();
            if(contentXElement != null)
                window.SetContent(GuiLoader.Builders[contentXElement.Name.LocalName].Build(contentXElement, controller));

            return window;
        }
    }
}