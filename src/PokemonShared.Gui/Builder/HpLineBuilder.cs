using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;

namespace PokemonShared.Gui.Builder
{
    public class HpLineBuilder : AbstractGuiComponentBuilder
    {
        private readonly IContainer _container;

        public HpLineBuilder(IContainer container)
        {
            _container = container;
        }

        public override IGuiComponent Build(ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var hpLine = _container.Resolve<HpLine>();
            SetUpController(controller, hpLine, xElement);
            hpLine.Area = ReadPosition(screenConstants, xElement);

            return hpLine;
        }
    }
}