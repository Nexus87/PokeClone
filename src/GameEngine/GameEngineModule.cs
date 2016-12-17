using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework.Content;
using System;
using System.Reflection;
using GameEngine.GameEngineComponents;
using GameEngine.Graphics.GUI;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.GUI.Graphics.TableView;

namespace GameEngine
{
    internal class GameEngineModule : IModule
    {
        private GraphicResources resources;
        readonly PokeEngine engine;

        public GameEngineModule(GraphicResources resources, PokeEngine engine)
        {
            this.engine = engine;
            this.resources = resources;
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterType<ISpriteFont>(r => resources.DefaultFont);
            registry.RegisterType<ItemBox>(reg => new ItemBox(resources.DefaultArrowTexture, resources.DefaultFont));
            registry.RegisterType<GraphicResources>(r => resources);
            registry.RegisterType<Dialog>(reg => new Dialog(resources.DefaultBorderTexture));
            registry.RegisterType<Line>(reg => new Line(resources.Pixel, resources.Cup));
            registry.RegisterTypeAs(typeof(DefaultTableRenderer<>), typeof(ITableRenderer<>));
            registry.RegisterTypeAs(typeof(DefaultTableModel<>), typeof(ITableModel<>));
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterTypeAs<TableSingleSelectionModel, ITableSelectionModel>();
            registry.RegisterAsService<InputComponent, InputComponent>(reg => new InputComponent(resources.Configuration));
            registry.RegisterAsService<ContentManager, ContentManager>(reg => resources.ContentManager);
            registry.RegisterType<Pixel>(r => new Pixel(resources.Pixel));
            registry.RegisterType<IEngineInterface>(r => engine);
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(Assembly.GetAssembly(typeof(Line)));
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGameTypeRegistry)));
        }

        public string ModuleName
        {
            get { return "GameEngine"; }
        }

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
