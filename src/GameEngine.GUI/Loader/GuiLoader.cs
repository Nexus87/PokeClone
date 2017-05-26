using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI.Loader.ControllBuilder;
using GameEngine.GUI.Loader.PanelBuilder;

namespace GameEngine.GUI.Loader
{
    public class GuiLoader
    {
        internal static void InitLoaderResources(IContainer container)
        {
            var screenConstants = container.Resolve<ScreenConstants>();
            Builders["Window"] = new WindowBuilder(container, screenConstants);
            Builders["Grid"] = new GridBuilder(container, screenConstants);
            Builders["ScrollArea"] = new ScrollAreaBuilder(container, screenConstants);
            Builders["ListView"] = new ListViewBuilder(container, screenConstants);
            Builders["Panel"] = new PanelBuilder.PanelBuilder(container, screenConstants);
            Builders["Label"] = new LabelBuilder(container, screenConstants);
            Builders["Spacer"] = new SpacerBuilder(container, screenConstants);

            foreach (var builder in AdditionalBuilders)
            {
                Builders[builder.Key] = builder.Value(container, screenConstants);
            }
        }

        public static void AddBuilder(string componentName, Func<IContainer, ScreenConstants, IBuilder> factory)
        {
            AdditionalBuilders[componentName] = factory;
        }

        private readonly string _path;
        internal static readonly Dictionary<string, IBuilder> Builders = new Dictionary<string, IBuilder>();
        private static readonly Dictionary<string, Func<IContainer, ScreenConstants, IBuilder>> AdditionalBuilders = new Dictionary<string, Func<IContainer, ScreenConstants, IBuilder>>();

        public GuiLoader(string path)
        {
            _path = @"Content\" + path;
        }

        public object Controller { get; set; }

        public IGuiComponent Load()
        {
            var xmlDoc = XDocument.Load(_path);
            Debug.Assert(xmlDoc.Root != null, "xmlDoc.Root != null");
            var firstComponent = xmlDoc.Root.Name.LocalName;

            return Builders[firstComponent].Build(xmlDoc.Root, Controller);

        }
    }
}