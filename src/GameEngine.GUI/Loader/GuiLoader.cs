using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;

namespace GameEngine.GUI.Loader
{
    public class GuiLoader
    {
        private readonly ScreenConstants _screenConstants;
        private readonly IContainer _container;
        private readonly Dictionary<string, IBuilder> _builders;


        public GuiLoader(ScreenConstants screenConstants, IContainer container, Dictionary<string, IBuilder> builders)
        {
            _screenConstants = screenConstants;
            _container = container;
            _builders = builders;
        }

        public IGuiComponent Load(string path, object controller)
        {
            path = @"Content\" + path;

            var xmlDoc = XDocument.Load(path);
            var xElement = xmlDoc.Root;
            Debug.Assert(xElement != null, "xmlDoc.Root != null");

            return LoadFromXml(xElement, controller);
        }

        public IGuiComponent LoadFromXml(XElement xElement, object controller)
        {
            var firstComponent = xElement.Name.LocalName;
            return _builders[firstComponent].Build(_container, this, _screenConstants, xElement, controller);
        }
    }
}