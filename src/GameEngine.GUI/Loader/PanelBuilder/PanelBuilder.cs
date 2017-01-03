using System.Linq;
using System.Xml.Linq;
using GameEngine.Core;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class PanelBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public PanelBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var panel = _registry.ResolveType<Panel>();

            panel.Area = ReadPosition(xElement);
            SetUpController(controller, panel, xElement);

            var content = xElement.Elements().FirstOrDefault();
            if (content != null)
                panel.SetContent(GuiLoader.Builders[content.Name.LocalName].Build(content, controller));

            return panel;
        }
    }
}