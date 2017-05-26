using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class SpacerBuilder : GuiComponentBuilder
    {
        private readonly IContainer _container;

        public SpacerBuilder(IContainer container, ScreenConstants screenConstants) : base(screenConstants)
        {
            _container = container;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var spacer = _container.Resolve<Spacer>();
            spacer.Area = ReadPosition(xElement);
            SetUpController(controller, spacer, xElement);

            return spacer;
        }
    }
}