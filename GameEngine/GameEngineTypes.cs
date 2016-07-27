using GameEngine.Graphics;
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
            registry.RegisterType<ISpriteFont>(r => factory.DefaultFont);
            registry.RegisterType<ItemBox>(reg => new ItemBox(factory.DefaultArrowTexture, factory.DefaultFont));
            registry.RegisterType<Dialog>(reg => new Dialog(factory.DefaultBorderTexture));
            registry.RegisterType<Line>(reg => new Line(factory.Pixel, factory.Cup));
            registry.RegisterTypeAs(typeof(DefaultTableRenderer<>), typeof(ITableRenderer<>));
            registry.RegisterTypeAs(typeof(DefaultTableModel<>), typeof(ITableModel<>));
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();
            registry.RegisterTypeAs<TableSingleSelectionModel, ITableSelectionModel>();
            registry.RegisterAsService<InputComponent, InputComponent>(reg => new InputComponent(config));
            registry.RegisterAsService<ContentManager, ContentManager>(reg => engine.Content);

            registry.ScanAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
