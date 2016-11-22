using System.Xml.Linq;

namespace GameEngine.GUI.Builder
{
    public interface IBuilderFactory
    {
        IBuilder GetBuilder(XElement element);
    }
}