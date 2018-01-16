using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class SpacerBuilder : AbstractGuiComponentBuilder
    {
        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants,  XElement xElement, object controller)
        {
            var spacer = container.Resolve<Spacer>();
            spacer.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, spacer, xElement);

            return spacer;
        }
    }
}