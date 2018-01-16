using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;

namespace PokemonShared.Gui.Builder
{
    public class HpTextBuilder : AbstractGuiComponentBuilder
    {
        private readonly IContainer _container;

        public HpTextBuilder(IContainer container)
        {
            _container = container;
        }

        public override IGuiComponent Build(ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var text = _container.Resolve<HpText>();
            text.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, text, xElement);

            var textHeight = xElement.Attribute("TextHeight");
            if (textHeight != null)
                text.PreferredTextHeight = int.Parse(textHeight.Value);

            return text;
        }
    }
}