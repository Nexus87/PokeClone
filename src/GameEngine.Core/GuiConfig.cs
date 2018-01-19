using System;
using System.Collections.Generic;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Loader.ControllBuilder;
using GameEngine.GUI.Loader.PanelBuilder;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;

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
            _builders["Window"] = new WindowBuilder();
            _builder.RegisterType<Window>();

            _builders["Grid"] = new GridBuilder();
            _builder.RegisterType<Grid>();

            _builders["ScrollArea"] = new ScrollAreaBuilder();
            _builder.RegisterType<ScrollArea>();

            _builders["ListView"] = new ListViewBuilder();
            _builder.RegisterGeneric(typeof(ListView<>));

            _builders["Panel"] = new PanelBuilder();
            _builder.RegisterType<Panel>();

            _builders["Label"] = new LabelBuilder();
            _builder.RegisterType<Label>();

            _builders["Spacer"] = new SpacerBuilder();
            _builder.RegisterType<Spacer>();

            _builders["Button"] = new ButtonBuilder();
            _builder.RegisterType<Button>();
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