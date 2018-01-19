using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Components;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class MessageBoxBuilder : AbstractGuiComponentBuilder
    {
        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var messageBox = container.Resolve<MessageBox>();
            messageBox.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, messageBox, xElement);
            return messageBox;
        }
    }
}