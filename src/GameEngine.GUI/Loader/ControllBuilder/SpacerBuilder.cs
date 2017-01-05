using System.Xml.Linq;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class SpacerBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public SpacerBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var spacer = _registry.ResolveType<Spacer>();
            spacer.Area = ReadPosition(xElement);
            SetUpController(controller, spacer, xElement);

            return spacer;
        }
    }
}