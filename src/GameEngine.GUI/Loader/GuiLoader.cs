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
        private readonly ScreenConstants _screenConstants;

        internal static void InitLoaderResources(IContainer container)
        {
            var screenConstants = container.Resolve<ScreenConstants>();
            Builders["Window"] = new WindowBuilder(container);
            Builders["Grid"] = new GridBuilder(container);
            Builders["ScrollArea"] = new ScrollAreaBuilder(container);
            Builders["ListView"] = new ListViewBuilder(container);
            Builders["Panel"] = new PanelBuilder.PanelBuilder(container);
            Builders["Label"] = new LabelBuilder(container, screenConstants);
            Builders["Spacer"] = new SpacerBuilder(container);

            foreach (var builder in AdditionalBuilders)
            {
                Builders[builder.Key] = builder.Value(container, screenConstants);
            }
        }

        public static void AddBuilder(string componentName, Func<IContainer, ScreenConstants, IBuilder> factory)
        {
            AdditionalBuilders[componentName] = factory;
        }

        internal static readonly Dictionary<string, IBuilder> Builders = new Dictionary<string, IBuilder>();
        private static readonly Dictionary<string, Func<IContainer, ScreenConstants, IBuilder>> AdditionalBuilders = new Dictionary<string, Func<IContainer, ScreenConstants, IBuilder>>();

        public GuiLoader(ScreenConstants screenConstants)
        {
            _screenConstants = screenConstants;
        }

        public IGuiComponent Load(string path, object controller)
        {
            path = @"Content\" + path;

            var xmlDoc = XDocument.Load(path);
            Debug.Assert(xmlDoc.Root != null, "xmlDoc.Root != null");
            var firstComponent = xmlDoc.Root.Name.LocalName;

            return Builders[firstComponent].Build(_screenConstants, xmlDoc.Root, controller);

        }
    }
}