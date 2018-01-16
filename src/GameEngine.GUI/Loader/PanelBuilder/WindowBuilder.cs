using System.Linq;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Loader.PanelBuilder
{
    public class WindowBuilder : AbstractGuiComponentBuilder
    {
        private readonly IContainer _container;



        public WindowBuilder(IContainer container)
        {
            _container = container;
        }

        public override IGuiComponent Build(ScreenConstants screenConstants, XElement xElement, object controller)
        {
            var window = _container.Resolve<Window>();
            window.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, window, xElement);

            var contentXElement = xElement.Elements().SingleOrDefault();
            if(contentXElement != null)
                window.SetContent(GuiLoader.Builders[contentXElement.Name.LocalName].Build(screenConstants, contentXElement, controller));

            return window;
        }
    }
}