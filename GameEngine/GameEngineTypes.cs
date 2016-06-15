using GameEngine.Graphics;
using GameEngine.Utils;
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
        public static void Register(IGameRegistry registry, GraphicComponentFactory factory)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.RegisterTypeAs<EventQueue, IEventQueue>();
            registry.RegisterType<InputComponent>();
            registry.RegisterTypeAs<XNASpriteBatch, ISpriteBatch>();
            registry.RegisterTypeAs<XNASpriteFont, ISpriteFont>();
            registry.RegisterTypeAs<XNATexture2D, ITexture2D>();
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();

            registry.RegisterType<AnimatedTextureBox>();
            registry.RegisterType<ItemBox>();
            registry.RegisterType<Line>( r => new Line(factory.Pixel, factory.Cup));
            registry.RegisterType<MultiLayeredComponent>();
            registry.RegisterType<MultlineTextBox>();
            registry.RegisterGenericTypeAs(typeof(TableView<>), typeof(ITableView<>));
            registry.RegisterTypeAs<TextBox, ITextGraphicComponent>();

            registry.RegisterTypeAs<TextGraphic, IGraphicalText>(r => new TextGraphic(factory.DefaultFont));
            registry.RegisterType<TextureBox>();
        }
    }
}
