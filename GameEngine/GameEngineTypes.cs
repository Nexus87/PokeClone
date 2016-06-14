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
        public static void Register(IGameRegistry registry)
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
            registry.RegisterGraphicComponentType<Line>();
            registry.RegisterGraphicComponentType<MultiLayeredComponent>();
            registry.RegisterGraphicComponentType<MultlineTextBox>();
            registry.RegisterGraphicComponentAsType(typeof(TableView<>), typeof(ITableView<>));
            registry.RegisterGraphicComponentAsType<TextBox, ITextGraphicComponent>();
            registry.RegisterTypeAs<TextGraphic, IGraphicalText>();
            registry.RegisterGraphicComponentType<TextureBox>();
        }
    }
}
