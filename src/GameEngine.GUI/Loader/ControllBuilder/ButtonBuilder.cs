using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class ButtonBuilder : AbstractGuiComponentBuilder
    {
        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement,
            object controller)
        {
            var button = container.Resolve<Button>();
            button.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, button, xElement);
            MapElementsToProperties(xElement, button);

            return button;
        }
    }
}