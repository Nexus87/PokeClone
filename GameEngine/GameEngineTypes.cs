using GameEngine.Graphics;
using GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal static class GameEngineTypes
    {
        public static void Register(IGameRegistry registry, GraphicComponentFactory factory)
        {
            registry.RegisterGameComponentAsType<EventQueue, IEventQueue>();
            registry.RegisterGameComponentType<InputComponent>();
            registry.RegisterTypeAs<KeyboardManager, IKeyboardManager>();
            registry.RegisterTypeAs<XNASpriteBatch, ISpriteBatch>();
            registry.RegisterTypeAs<XNASpriteFont, ISpriteFont>();
            registry.RegisterTypeAs<XNATexture2D, ITexture2D>();
            registry.RegisterTypeAs<DefaultTextSplitter, ITextSplitter>();

            registry.RegisterGraphicComponentType<AnimatedTextureBox>();
            registry.RegisterGraphicComponentType<ItemBox>();
            var lineParameters = new Dictionary<string, object>{
                {"pixel", factory.Pixel},
                {"cupTexture", factory.Cup}
            };
            registry.RegisterGraphicComponentType<Line>(namedParameters: lineParameters);
            registry.RegisterGraphicComponentType<MultiLayeredComponent>();
            registry.RegisterGraphicComponentType<MultlineTextBox>();
            registry.RegisterGraphicComponentAsType(typeof(TableView<>), typeof(ITableView<>));
            registry.RegisterGraphicComponentAsType<TextBox, ITextGraphicComponent>();

            var textGraphicParameters = new Dictionary<Type, object>
            {
                {typeof(ISpriteFont), factory.DefaultBorderTexture}
            };
            registry.RegisterTypeAs<TextGraphic, IGraphicalText>(typedParameters: textGraphicParameters);
            registry.RegisterGraphicComponentType<TextureBox>();
        }
    }
}
