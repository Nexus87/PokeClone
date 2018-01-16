using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class PanelBuilder : AbstractGuiComponentBuilder
    {
        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var panel = container.Resolve<Panel>();

            panel.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, panel, xElement);

            var content = xElement.Elements().FirstOrDefault();
            if (content != null)
                panel.SetContent(loader.LoadFromXml(content, controller));

            return panel;
        }
    }
}