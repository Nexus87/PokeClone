using System.Xml.Linq;

namespace GameEngine.GUI.Builder
{
    public interface IBuilder
    {
        IGraphicComponent BuildFromNode(XElement element);
    }
}