using System;
using System.Collections.Generic;
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
using GameEngine.Pokemon.Gui;
using GameEngine.Pokemon.Gui.Builder;
using GameEngine.Pokemon.Gui.Renderer;
using GameEngine.Pokemon.Gui.Renderer.PokemonClassicRenderer;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    internal class GameEngineModule : Module
    {
        private readonly GameRunner _engine;
        private readonly TextureProvider _textureProvider;
        private readonly InputEntity _inputEntity;
        private readonly ScreenConstants _screenConstants;
        private readonly GameComponentManager _gameComponentManager;

        public static object Key = new object();
        public GameEngineModule(GameRunner engine, TextureProvider textureProvider, Configuration config) :
            base(Key, "GameEngine", "GameEngine")
        {
            _engine = engine;
            _textureProvider = textureProvider;
            _inputEntity = new InputEntity(config.KeyMap);
            _screenConstants = new ScreenConstants();
            _gameComponentManager = new GameComponentManager(engine.Components, engine);
        }

        public override void RegisterTypes(IGameTypeRegistry registry)
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
                typeof(HpLine).Assembly,
                typeof(TextureBuilder).Assembly
            });
            registry.RegisterType(r => new Panel(r.ResolveType<PanelRenderer>()) { BackgroundColor = r.ResolveType<ScreenConstants>().BackgroundColor});

        }

        public override void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            var platform = Type.GetType("Mono.Runtime") == null ? "Windows" : "Linux";

            var fullPath = TextureConfigurationBuilder.ContentRoot + "\\" + "GameEngine\\Textures" + "\\" + platform + "\\";
            var textures = new List<TextureItem>
            {
                new TextureItem(fullPath + "charmander-front", "charmander-front"),
                new TextureItem(fullPath + "charmander-back", "charmander-back")
            };

            builder.AddTextureConfig(Key, textures);

            ReadConfiguration(builder, resourceKey: ClassicSkin.Key);
        }

        public override void AddBuilderAndRenderer()
        {
            ClassicSkin.AddAdditionalRenderer<ClassicLineRenderer, HpLineRenderer>(
                t => new ClassicLineRenderer(t.GetTexture(ClassicSkin.Key, ClassicSkin.Circle), t.Pixel, ClassicSkin.BackgroundColor)
            );
            ClassicSkin.AddAdditionalRenderer<ClassicHpTextRenderer, HpTextRenderer>(
                t => new ClassicHpTextRenderer(t.GetFont(ClassicSkin.Key, ClassicSkin.DefaultFont))
            );

            GuiLoader.AddBuilder("HpLine", (r, c) => new HpLineBuilder(r, c));
            GuiLoader.AddBuilder("HpText", (r, c) => new HpTextBuilder(r, c));
        }

        public static string Name => "GameEngine";

        public override void Start(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            componentManager.Scene = new Scene(registry.ResolveType<ScreenConstants>());
            GuiLoader.InitLoaderResources(registry);

            componentManager.AddGameComponent(registry.ResolveType<InputEntity>());
            componentManager.AddGameComponent(registry.ResolveType<IEventQueue>());

            _engine.GuiManager = registry.ResolveType<GuiManager>();
            _engine.Screen = registry.ResolveType<Screen>();
            _engine.TextureProvider = registry.ResolveType<TextureProvider>();
        }

        public override void Stop(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager)
        {
            throw new NotImplementedException();
        }
    }
}
