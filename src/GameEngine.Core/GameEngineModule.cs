using GameEngine.Core.ModuleManager;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    internal class GameEngineModule : IModule
    {
        private readonly GameRunner _engine;
        private readonly TextureProvider _textureProvider;
        private readonly ScreenConstants _screenConstants;
        public string ModuleName => "GameEngine";

        public GameEngineModule(GameRunner engine, TextureProvider textureProvider, Configuration config)
        {
            _engine = engine;
            _textureProvider = textureProvider;
            _screenConstants = new ScreenConstants();
        }

        public virtual void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterAsService<ContentManager, ContentManager>(reg => _engine.Content);
            registry.RegisterAsService<TextureProvider, TextureProvider>(r => _textureProvider);
            registry.RegisterType<IEngineInterface>(r => _engine);
            registry.RegisterType(r => _screenConstants);
            registry.RegisterType(r => _engine.GraphicsDeviceManager);
            registry.ScanAssemblies(new[]
            {
                typeof(GameEngineModule).Assembly,
                typeof(IGuiComponent).Assembly,
                typeof(IGameTypeRegistry).Assembly,
                typeof(TextureBuilder).Assembly
            });
            registry.RegisterType(r => new Panel(r.ResolveType<PanelRenderer>()) { BackgroundColor = r.ResolveType<ScreenConstants>().BackgroundColor});

        }


        public static string Name => "GameEngine";


    }
}
