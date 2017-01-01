using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Core.ModuleManager;
using GameEngine.Graphics;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Configuration;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    internal class GameEngineModule : IModule
    {
        private readonly GameRunner _engine;
        private readonly Graphics.TextureProvider _textureProvider;
        private readonly InputComponent _inputComponent;
        private readonly ScreenConstants _screenConstants;
        private readonly GameComponentManager _gameComponentManager;

        public static object Key = new object();
        public GameEngineModule(GameRunner engine, Graphics.TextureProvider textureProvider, Configuration config)
        {
            _engine = engine;
            _textureProvider = textureProvider;
            _inputComponent = new InputComponent(config);
            _screenConstants = new ScreenConstants();
            _gameComponentManager = new GameComponentManager(engine.Components, engine);
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterAsService<InputComponent, InputComponent>(reg => _inputComponent);
            registry.RegisterAsService<InputComponent, IInputHandlerManager>(reg => _inputComponent);
            registry.RegisterAsService<ContentManager, ContentManager>(reg => _engine.Content);
            registry.RegisterAsService<Graphics.TextureProvider, Graphics.TextureProvider>(r => _textureProvider);
            registry.RegisterAsService<GameComponentManager, IGameComponentManager>(r => _gameComponentManager);
            registry.RegisterAsService<Screen, Screen>();
            registry.RegisterType<IEngineInterface>(r => _engine);
            registry.RegisterType(r => _screenConstants);
            registry.RegisterType(r => _engine.GraphicsDeviceManager.GraphicsDevice);
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGraphicComponent)));
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGameTypeRegistry)));

            registry.RegisterType(r => new Panel(r.ResolveType<PanelRenderer>()) { BackgroundColor = r.ResolveType<ScreenConstants>().BackgroundColor});

        }

        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            var textures = new List<TextureItem>
            {
                new TextureItem("charmander-front", TextureType.SingleTexture, null, "charmander-front", true),
                new TextureItem("charmander-back", TextureType.SingleTexture, null, "charmander-back", true)
            };

            builder.AddTextureConfig(Key, textures);
        }

        public static string Name => "GameEngine";
        public string ModuleName => Name;

        public void Start(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            componentManager.AddGameComponent(registry.ResolveType<InputComponent>());
            componentManager.AddGameComponent(registry.ResolveType<IEventQueue>());

            _engine.GuiManager = registry.ResolveType<GuiManager>();
            _engine.Screen = registry.ResolveType<Screen>();
            _engine.TextureProvider = registry.ResolveType<TextureProvider>();
        }

        public void Stop(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager)
        {
            throw new NotImplementedException();
        }
    }
}
