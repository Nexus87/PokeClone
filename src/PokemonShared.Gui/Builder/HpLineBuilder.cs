using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;

namespace PokemonShared.Gui.Builder
{
    public class HpLineBuilder : AbstractGuiComponentBuilder
    {

        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var hpLine = container.Resolve<HpLine>();
            SetUpController(controller, hpLine, xElement);
            hpLine.Area = ReadPosition(screenConstants, xElement);

            return hpLine;
        }
    }
}