using System;
using System.Collections.Generic;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Loader.ControllBuilder;
using GameEngine.GUI.Loader.PanelBuilder;
using GameEngine.GUI.Panels;

namespace GameEngine.Core
{
    public class GuiConfig
    {
        private readonly ContainerBuilder _builder;

        private readonly Dictionary<string, IBuilder> _builders = new Dictionary<string, IBuilder>();
        private IContainer _container;

        public GuiConfig()
        {
            CurrentSkin = ClassicSkin;
            _builder = new ContainerBuilder();
            InitDefaults();
        }

        public ClassicSkin ClassicSkin { get; } = new ClassicSkin();

        public ISkin CurrentSkin { get; set; }

        internal void InitDefaults()
        {
            //AddGuiElement("Window", new WindowBuilder(), skin => new Window(skin.GetRendererForComponent(typeof)));
            _builders["Window"] = new WindowBuilder();
            _builders["Grid"] = new GridBuilder();
            _builders["ScrollArea"] = new ScrollAreaBuilder();
            _builders["ListView"] = new ListViewBuilder();
            _builders["Panel"] = new PanelBuilder();
            _builders["Label"] = new LabelBuilder();
            _builders["Spacer"] = new SpacerBuilder();
        }


        public void AddGuiElement<T>(string componentName, IBuilder componentBuilder, Func<ISkin, T> componentFactory)
        {
            _builder.Register(x => componentFactory(CurrentSkin));
            _builders[componentName] = componentBuilder;
        }

        internal GuiFactory Init(ScreenConstants screenConstants)
        {
            _container = _builder.Build();
            var loader = new GuiLoader(screenConstants, _container, _builders);
            return new GuiFactory(loader, _container);
        }
    }
}