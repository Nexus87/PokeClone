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
    internal static class GameEngineTypes
    {
        public enum ResourceKeys
        {
            PixelTexture,
            CupTexture,
            ArrowTexture,
            BorderTexture
        }

        public static void Register(IGameTypeRegistry registry, GraphicResources factory, PokeEngine engine, Configuration config)
        {
            registry.RegisterParameter(ResourceKeys.PixelTexture, factory.Pixel);
            registry.RegisterParameter(ResourceKeys.CupTexture, factory.Cup);
            registry.RegisterParameter(ResourceKeys.ArrowTexture, factory.DefaultArrowTexture);
            registry.RegisterParameter(ResourceKeys.BorderTexture, factory.DefaultBorderTexture);

            registry.RegisterType<ISpriteFont>(r => factory.DefaultFont);
            registry.RegisterType<ItemBox>(reg => new ItemBox(factory.DefaultArrowTexture, factory.DefaultFont));
            registry.RegisterType<Dialog>(reg => new Dialog(factory.DefaultBorderTexture));
            registry.RegisterType<Line>(reg => new Line(factory.Pixel, factory.Cup));
            registry.RegisterType<TextureBox>();
            registry.RegisterType<MultlineTextBox>();
            registry.RegisterType<MessageBox>();
            registry.RegisterTypeAs(typeof(DefaultTableRenderer<>), typeof(ITableRenderer<>));
            registry.RegisterType(typeof(DefaultTableRenderer<>));
            registry.RegisterTypeAs(typeof(DefaultTableModel<>), typeof(ITableModel<>));
            registry.RegisterType<Container>();
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterType<TextBox>();
            registry.RegisterAsService<EventQueue, IEventQueue>();
            registry.RegisterTypeAs<TableSingleSelectionModel, ITableSelectionModel>();
            registry.RegisterAsService<InputComponent, InputComponent>(reg => new InputComponent(config));
            registry.RegisterType(typeof(TableView<>));
            registry.RegisterAsService<Screen, Screen>();
            registry.RegisterAsService<GUIManager, GUIManager>();
            registry.RegisterAsService<TextureProvider, TextureProvider>();
            registry.RegisterAsService<ContentManager, ContentManager>(reg => engine.Content);
        }
    }
}
