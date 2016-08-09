﻿using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class GameEngineModule : IModule
    {
        private GraphicResources resources;

        public GameEngineModule(GraphicResources resources)
        {
            this.resources = resources;
        }
        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterType<ISpriteFont>(r => resources.DefaultFont);
            registry.RegisterType<ItemBox>(reg => new ItemBox(resources.DefaultArrowTexture, resources.DefaultFont));
            registry.RegisterType<Dialog>(reg => new Dialog(resources.DefaultBorderTexture));
            registry.RegisterType<Line>(reg => new Line(resources.Pixel, resources.Cup));
            registry.RegisterTypeAs(typeof(DefaultTableRenderer<>), typeof(ITableRenderer<>));
            registry.RegisterTypeAs(typeof(DefaultTableModel<>), typeof(ITableModel<>));
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterTypeAs<TableSingleSelectionModel, ITableSelectionModel>();
            registry.RegisterAsService<InputComponent, InputComponent>(reg => new InputComponent(resources.Configuration));
            registry.RegisterAsService<ContentManager, ContentManager>(reg => resources.ContentManager);
            
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
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