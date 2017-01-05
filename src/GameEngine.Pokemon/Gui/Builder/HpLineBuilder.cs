using System.Xml.Linq;
using GameEngine.Core;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Loader;
using GameEngine.TypeRegistry;

namespace GameEngine.Pokemon.Gui.Builder
{
    public class HpLineBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public HpLineBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var hpLine = _registry.ResolveType<HpLine>();
            SetUpController(controller, hpLine, xElement);
            hpLine.Area = ReadPosition(xElement);

            return hpLine;
        }
    }
}