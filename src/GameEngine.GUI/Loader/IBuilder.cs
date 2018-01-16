using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;

namespace GameEngine.GUI.Loader
{
    public interface IBuilder
    {
        IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement, object controller);
    }
}