using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicTextAreaRenderer : AbstractRenderer<TextArea>, ITextAreaRenderer
    {
        private readonly ISpriteFont _font;

        public ClassicTextAreaRenderer(ISpriteFont font)
        {
            _font = font;
        }

        public override void Render(ISpriteBatch spriteBatch, TextArea component)
        {
            foreach (var line in component.Lines)
            {
                RenderText(spriteBatch, _font, line.Text, line.Position, component.TextHeight);
            }
        }

        public int CharsPerLine(TextArea textArea)
        {
            return textArea.Area.Width / textArea.TextHeight;
        }

        public int LineSpacing { get; } = 10;

        public int GetLineHeight(TextArea textArea)
        {
            return textArea.TextHeight + LineSpacing;
        }
    }
}