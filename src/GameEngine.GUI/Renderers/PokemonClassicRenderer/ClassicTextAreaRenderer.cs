using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicTextAreaRenderer : TextAreaRenderer
    {
        private ISpriteFont _font;

        public ClassicTextAreaRenderer()
        {
            LineSpacing = 10;
        }

        public override void Init(ITextureProvider textureProvider)
        {
            _font = textureProvider.GetFont(ClassicSkin.DefaultFont);
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