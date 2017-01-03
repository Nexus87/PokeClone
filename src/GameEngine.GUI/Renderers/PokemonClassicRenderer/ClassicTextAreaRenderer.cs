using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicTextAreaRenderer : TextAreaRenderer
    {
        private readonly ISpriteFont _font;

        public ClassicTextAreaRenderer(ISpriteFont font)
        {
            _font = font;
            LineSpacing = 10;
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, TextArea component)
        {
            foreach (var line in component.Lines)
            {
                RenderText(spriteBatch, _font, line.Text, line.Position, component.TextHeight);
            }
        }

        public override int CharsPerLine(TextArea textArea)
        {
            return textArea.Area.Width / textArea.TextHeight;
        }

        public override int GetLineHeight(TextArea textArea)
        {
            return textArea.TextHeight + LineSpacing;
        }
    }
}