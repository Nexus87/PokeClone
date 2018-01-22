using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class PanelBuilder : AbstractGuiComponentBuilder
    {
        public override IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants,
            XElement xElement, object controller)
        {
            var panel = container.Resolve<Panel>();

            panel.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, panel, xElement);
            MapElementsToProperties(xElement, panel);

            var color = xElement.Attribute("BackgroundColor");
            if (color != null && color.Value == "Default")
                panel.BackgroundColor = screenConstants.BackgroundColor;

            var content = xElement.Elements().FirstOrDefault();
            if (content != null)
                panel.SetContent(loader.LoadFromXml(content, controller));

            return panel;
        }
    }
}