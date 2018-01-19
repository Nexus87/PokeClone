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

            var text = xElement.Attribute(nameof(Button.Text));
            var textSize = xElement.Attribute(nameof(Button.TextHeight));
            if (text != null)
                button.Text = text.Value;
            if (textSize != null)
                button.TextHeight = int.Parse(textSize.Value);

            return button;
        }
    }
}