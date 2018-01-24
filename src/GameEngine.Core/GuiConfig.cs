﻿using System;
using System.Collections.Generic;
using Autofac;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Controlls;
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

        public GuiConfig(string contentRoot)
        {
            CurrentSkin = ClassicSkin;
            _builder = new ContainerBuilder();
            SkinTextureConfigurationBuilder = new TextureConfigurationBuilder(contentRoot);
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

            _builders["Label"] = new GenericBuilder<Label>();
            _builder.RegisterType<Label>();

            _builders["Spacer"] = new GenericBuilder<Spacer>();
            _builder.RegisterType<Spacer>();

            _builders["Button"] = new GenericBuilder<Button>();
            _builder.RegisterType<Button>();

            _builders["MessageBox"] = new GenericBuilder<MessageBox>();
            _builder.RegisterType<MessageBox>();

            _builders["ImageBox"] = new GenericBuilder<ImageBox>();
            _builder.RegisterType<ImageBox>();
        }

        public TextureConfigurationBuilder SkinTextureConfigurationBuilder { get; }
        public void AddGuiElement<T>(string componentName, IBuilder componentBuilder, Func<ISkin, T> componentFactory)
        {
            _builder.Register(x => componentFactory(CurrentSkin));
            _builders[componentName] = componentBuilder;
        }

        internal GuiFactory Init(ScreenConstants screenConstants)
        {
            _container = _builder.Build();
            CurrentSkin.AddTextureConfigurations(SkinTextureConfigurationBuilder);
            var loader = new GuiLoader(screenConstants, _container, _builders);
            return new GuiFactory(loader, _container);
        }
    }
}