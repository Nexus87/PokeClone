using System;
using System.Reflection;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Core.Registry;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;
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
            registry.RegisterType(reg => new Dialog(reg.ResolveType<IImageBoxRenderer>(), _resources.DefaultBorderTexture));
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterAsService<InputComponent, InputComponent>(reg => new InputComponent(_resources.Configuration));
            registry.RegisterAsService<ContentManager, ContentManager>(reg => _resources.ContentManager);
            registry.RegisterType(r => new Pixel(_resources.Pixel, r.ResolveType<IImageBoxRenderer>()));
            registry.RegisterType<IEngineInterface>(r => _engine);
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGraphicComponent)));
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGameTypeRegistry)));
            registry.RegisterAsService<ClassicButtonRenderer, IButtonRenderer>(r => new ClassicButtonRenderer(_resources.DefaultArrowTexture, _resources.DefaultFont));
            registry.RegisterAsService<ClassicWindowRenderer, IWindowRenderer>(r => new ClassicWindowRenderer(_resources.DefaultBorderTexture));
            registry.RegisterAsService<ClassicLabelRenderer, ILabelRenderer>(r => new ClassicLabelRenderer(_resources.DefaultFont));
            registry.RegisterAsService<ClassicTextAreaRenderer, ITextAreaRenderer>(r => new ClassicTextAreaRenderer(_resources.DefaultFont));
            registry.RegisterAsService<ClassicLineRenderer, IHpLineRenderer>(r => new ClassicLineRenderer(_resources.Cup, _resources.Pixel, r.ResolveType<ScreenConstants>().BackgroundColor));
            registry.RegisterAsService<ClassicImageBoxRenderer, IImageBoxRenderer>();
            registry.RegisterAsService<ClassPanelRenderer, IPanelRenderer>(r => new ClassPanelRenderer(_resources.Pixel));
            registry.RegisterType<Panel>(r => new Panel(r.ResolveType<IPanelRenderer>()) { BackgroundColor = r.ResolveType<ScreenConstants>().BackgroundColor});
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
