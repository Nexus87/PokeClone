using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class WindowBuilder : GuiComponentBuilder
    {
        private readonly IContainer _container;



        public WindowBuilder(IContainer container, ScreenConstants screenConstants) :
            base(screenConstants)
        {
            _container = container;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var window = _container.Resolve<Window>();
            window.Area = ReadPosition(xElement);
            SetUpController(controller, window, xElement);

            var contentXElement = xElement.Elements().SingleOrDefault();
            if(contentXElement != null)
                window.SetContent(GuiLoader.Builders[contentXElement.Name.LocalName].Build(contentXElement, controller));

            return window;
        }
    }
}