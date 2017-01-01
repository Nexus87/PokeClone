using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Core.ModuleManager;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Configuration;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    internal class GameEngineModule : IModule
    {
        private readonly GameRunner _engine;
        private readonly Configuration _config;

        public GameEngineModule(GameRunner engine, Configuration config)
        {
            _engine = engine;
            _config = config;
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterAsService<InputComponent, InputComponent>(reg => new InputComponent(_config));
            registry.RegisterAsService<ContentManager, ContentManager>(reg => _engine.Content);
            registry.RegisterType<IEngineInterface>(r => _engine);
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGraphicComponent)));
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGameTypeRegistry)));

      
            registry.RegisterType<Panel>(r => new Panel(r.ResolveType<PanelRenderer>()) { BackgroundColor = r.ResolveType<ScreenConstants>().BackgroundColor});

        }

        public string ModuleName => "GameEngine";

        public void Start(IGameComponentManager componentManager, IGameTypeRegistry registry)
        {
            componentManager.AddGameComponent(registry.ResolveType<InputComponent>());
            componentManager.AddGameComponent(registry.ResolveType<IEventQueue>());
        }

        public void Stop(IGameComponentManager componentManager)
        {
            throw new NotImplementedException();
        }
    }
}
