using System.Xml.Linq;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;
using GameEngine.TypeRegistry;

namespace PokemonShared.Gui.Builder
{
    public class HpTextBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public HpTextBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var text = _registry.ResolveType<HpText>();
            text.Area = ReadPosition(xElement);
            SetUpController(controller, text, xElement);

            var textHeight = xElement.Attribute("TextHeight");
            if (textHeight != null)
                text.PreferredTextHeight = int.Parse(textHeight.Value);

            return text;
        }
    }
}