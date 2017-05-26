using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class PanelBuilder : GuiComponentBuilder
    {
        private readonly IContainer _container;

        public PanelBuilder(IContainer container, ScreenConstants screenConstants) : base(screenConstants)
        {
            _container = container;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var panel = _container.Resolve<Panel>();

            panel.Area = ReadPosition(xElement);
            SetUpController(controller, panel, xElement);

            var content = xElement.Elements().FirstOrDefault();
            if (content != null)
                panel.SetContent(GuiLoader.Builders[content.Name.LocalName].Build(content, controller));

            return panel;
        }
    }
}