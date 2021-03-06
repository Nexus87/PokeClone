﻿using System.Xml.Linq;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;
using GameEngine.TypeRegistry;

namespace PokemonShared.Gui.Builder
{
    public class HpLineBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public HpLineBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var hpLine = _registry.ResolveType<HpLine>();
            SetUpController(controller, hpLine, xElement);
            hpLine.Area = ReadPosition(xElement);

            return hpLine;
        }
    }
}