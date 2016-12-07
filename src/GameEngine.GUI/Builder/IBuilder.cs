using System.Xml.Linq;

namespace GameEngine.GUI.Builder
{
    public interface IBuilder
    {
        IGuiComponent BuildFromNode(XElement element, object controller);
    }
}