using System.Xml.Linq;

namespace GameEngine.GUI.Loader
{
    public interface IBuilder
    {
        IGuiComponent Build(XElement xElement, object controller);
    }
}