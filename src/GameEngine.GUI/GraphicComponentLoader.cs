using System.Xml;
using System.Xml.Linq;
using GameEngine.GUI.Builder;

namespace GameEngine.GUI
{
    public class GraphicComponentLoader
    {
        private readonly IBuilderFactory _factory;

        public GraphicComponentLoader(IBuilderFactory factory)
        {
            _factory = factory;
        }

        public string FilePath { get; set; }

        public IGraphicComponent Load()
        {
            var rootElement = XDocument.Load(FilePath).Root;
            var builder = _factory.GetBuilder(rootElement);

            return builder.BuildFromNode(rootElement);
        }
    }
}