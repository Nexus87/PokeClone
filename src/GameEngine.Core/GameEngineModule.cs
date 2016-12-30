using System;
using System.Reflection;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Core.Registry;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    internal class GameEngineModule : IModule
    {
        private readonly GraphicResources _resources;
        private readonly PokeEngine _engine;

        public GameEngineModule(GraphicResources resources, PokeEngine engine)
        {
            _engine = engine;
            _resources = resources;
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterType(r => _resources.DefaultFont);
            registry.RegisterType(r => _resources);
            registry.RegisterType(reg => new Dialog(_resources.DefaultBorderTexture));
            registry.RegisterType(reg => new Line(_resources.Pixel, _resources.Cup));
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterAsService<InputComponent, InputComponent>(reg => new InputComponent(_resources.Configuration));
            registry.RegisterAsService<ContentManager, ContentManager>(reg => _resources.ContentManager);
            registry.RegisterType(r => new Pixel(_resources.Pixel));
            registry.RegisterType<IEngineInterface>(r => _engine);
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(Assembly.GetAssembly(typeof(Line)));
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGameTypeRegistry)));
            registry.RegisterAsService<ClassicButtonRenderer, IButtonRenderer>(r => new ClassicButtonRenderer(_resources.DefaultArrowTexture, _resources.DefaultFont));
            registry.RegisterAsService<ClassicWindowRenderer, IWindowRenderer>(r => new ClassicWindowRenderer(_resources.DefaultBorderTexture));
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
