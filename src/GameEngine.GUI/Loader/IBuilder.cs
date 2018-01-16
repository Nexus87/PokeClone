using System.Xml.Linq;
using GameEngine.Globals;

namespace GameEngine.GUI.Loader
{
    public interface IBuilder
    {
        IGuiComponent Build(ScreenConstants screenConstants, XElement xElement, object controller);
    }
}