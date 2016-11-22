using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using GameEngine.Graphics.GUI;
using GameEngine.GUI.Builder.Controls;
using GameEngine.GUI.ComponentRegistry;
using GameEngine.Registry;

namespace GameEngine.GUI.Builder
{
    public class BuilderFactory : IBuilderFactory
    {
        private readonly GuiComponentRegistry _registry;

        public BuilderFactory(GuiComponentRegistry registry)
        {
            _registry = registry;
        }

        public IBuilder GetBuilder(XElement element)
        {
            return _registry.GetBuilder(element.Name);
        }
    }
}