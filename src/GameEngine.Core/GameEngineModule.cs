using System;
using GameEngine.Core.ModuleManager;
using GameEngine.Entities;
using GameEngine.Globals;
using GameEngine.Graphics;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    internal class GameEngineModule : IContentModule, IModule
    {
        private readonly GameRunner _engine;
        private readonly TextureProvider _textureProvider;
        private readonly InputEntity _inputEntity;
        private readonly ScreenConstants _screenConstants;
        private readonly GameComponentManager _gameComponentManager;
        public string ModuleName => "GameEngine";

        public GameEngineModule(GameRunner engine, TextureProvider textureProvider, Configuration config)
        {
            _engine = engine;
            _textureProvider = textureProvider;
            _inputEntity = new InputEntity(config.KeyMap);
            _screenConstants = new ScreenConstants();
            _gameComponentManager = new GameComponentManager(engine.Components, engine);
        }

        public virtual void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterAsService<InputEntity, InputEntity>(reg => _inputEntity);
            registry.RegisterAsService<InputEntity, IInputHandlerManager>(reg => _inputEntity);
            registry.RegisterAsService<ContentManager, ContentManager>(reg => _engine.Content);
            registry.RegisterAsService<TextureProvider, TextureProvider>(r => _textureProvider);
            registry.RegisterAsService<GameComponentManager, IGameComponentManager>(r => _gameComponentManager);
            registry.RegisterType<IEngineInterface>(r => _engine);
            registry.RegisterType(r => _screenConstants);
            registry.RegisterType(r => _engine.GraphicsDeviceManager);
            registry.RegisterType(r => _engine.Scene);
            registry.ScanAssemblies(new[]
            {
                typeof(GameEngineModule).Assembly,
                typeof(IGuiComponent).Assembly,
                typeof(IGameTypeRegistry).Assembly,
                typeof(IGameEntity).Assembly,
                typeof(TextureBuilder).Assembly
            });
            registry.RegisterType(r => new Panel(r.ResolveType<PanelRenderer>()) { BackgroundColor = r.ResolveType<ScreenConstants>().BackgroundColor});

        }

        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
        }


        public static string Name => "GameEngine";

        public virtual void Start(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            componentManager.Scene = new Scene(registry.ResolveType<ScreenConstants>());
            GuiLoader.InitLoaderResources(registry);

            componentManager.AddGameComponent(registry.ResolveType<InputEntity>());
            componentManager.AddGameComponent(registry.ResolveType<IEventQueue>());

            _engine.GuiManager = registry.ResolveType<GuiManager>();
            _engine.Screen = registry.ResolveType<Screen>();
        }

        public virtual void Stop(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager)
        {
            throw new NotImplementedException();
        }

    }
}
