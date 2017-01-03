﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using GameEngine.Core;
using GameEngine.GUI.Loader.PanelBuilder;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI.Loader
{
    public class GuiLoader
    {
        internal static void InitLoaderResources(IGameTypeRegistry registry)
        {
            var screenConstants = registry.ResolveType<ScreenConstants>();
            Builders["Window"] = new WindowBuilder(registry, screenConstants);
            Builders["Grid"] = new GridBuilder(registry, screenConstants);
            Builders["ScrollArea"] = new ScrollAreaBuilder(registry, screenConstants);
            Builders["ListView"] = new ListViewBuilder(registry, screenConstants);
            Builders["Panel"] = new PanelBuilder.PanelBuilder(registry, screenConstants);
        }

        private readonly string _path;
        internal static readonly Dictionary<string, IBuilder> Builders = new Dictionary<string, IBuilder>();
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