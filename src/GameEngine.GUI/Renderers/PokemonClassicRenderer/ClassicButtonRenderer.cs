using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicButtonRenderer : ButtonRenderer
    {
        private ITexture2D _arrow;
        private ISpriteFont _font;

        public override void Init(ITextureProvider textureProvider)
        {
            _arrow = textureProvider.GetTexture(ClassicSkin.Arrow);
            _font = textureProvider.GetFont(ClassicSkin.DefaultFont);
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, Button component)
        {
            if (component.IsSelected)
                DrawArrow(spriteBatch, component.Area, component);
            DrawText(spriteBatch, component.Area, component);
        }

        private void DrawText(ISpriteBatch spriteBatch, Rectangle position, Button button)
        {
            // Position + arrow width + margin
            position.X += (int) button.TextHeight + 10;

            RenderText(spriteBatch, _font, button.Text, position, button.TextHeight);
        }

        private void DrawArrow(ISpriteBatch spriteBatch, Rectangle position, Button button)
        {
            position.Height = (int) button.TextHeight;
            position.Width = (int) button.TextHeight;

            RenderImage(spriteBatch, _arrow, position);
        }

        public override float GetPreferedWidth(Button button)
        {
            var scale = button.TextHeight / _font.MeasureString(" ").Y;
            // Text + Arrow
            return scale * _font.MeasureString(button.Text).X + button.TextHeight;
        }

        public override float GetPreferedHeight(Button button)
        {
            return button.TextHeight;
        }
    }
}