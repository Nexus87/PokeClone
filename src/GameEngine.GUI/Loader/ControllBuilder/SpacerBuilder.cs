using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class SpacerBuilder : AbstractGuiComponentBuilder
    {
        private readonly IContainer _container;

        public SpacerBuilder(IContainer container)
        {
            _container = container;
        }

        public override IGuiComponent Build(ScreenConstants screenConstants,  XElement xElement, object controller)
        {
            var spacer = _container.Resolve<Spacer>();
            spacer.Area = ReadPosition(screenConstants, xElement);
            SetUpController(controller, spacer, xElement);

            return spacer;
        }
    }
}