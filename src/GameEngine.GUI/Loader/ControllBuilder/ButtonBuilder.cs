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
            var label = container.Resolve<Button>();
            label.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, label, xElement);

            var text = xElement.Attribute(nameof(Button.Text));
            var textSize = xElement.Attribute(nameof(Button.TextHeight));
            if (text != null)
                label.Text = text.Value;
            if (textSize != null)
                label.TextHeight = int.Parse(textSize.Value);

            return label;
        }
    }
}