using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class LabelBuilder : AbstractGuiComponentBuilder
    {

        public override IGuiComponent Build(IContainer container, GuiLoader loader,  ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var label = container.Resolve<Label>();
            label.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, label, xElement);
            MapElementsToProperties(xElement, label);

            return label;
        }
    }
}