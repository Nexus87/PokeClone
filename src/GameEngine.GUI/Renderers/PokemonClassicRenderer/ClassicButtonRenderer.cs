using GameEngine.Graphics.General;
using GameEngine.GUI.Controlls;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicButtonRenderer : AbstractRenderer<Button>, IButtonRenderer
    {
        private ITexture2D _arrow;
        private ISpriteFont _font;

        public ClassicButtonRenderer(ITexture2D arrow, ISpriteFont font)
        {
            _arrow = arrow;
            _font = font;
        }

        public override void Render(ISpriteBatch spriteBatch, Button component)
        {
            spriteBatch.GraphicsDevice.ScissorRectangle = component.ScissorArea;
            if (component.IsSelected)
                DrawArrow(spriteBatch, component.Constraints, component);
            DrawText(spriteBatch, component.Constraints, component);
        }

        private void DrawText(ISpriteBatch spriteBatch, Rectangle position, Button button)
        {
            // Position + arrow width
            position.X += (int) button.TextHeight;

            RenderText(spriteBatch, _font, button.Text, position, button.TextHeight);
        }

        private void DrawArrow(ISpriteBatch spriteBatch, Rectangle position, Button button)
        {
            position.Height = (int) button.TextHeight;
            position.Width = (int) button.TextHeight;

            RenderImage(spriteBatch, _arrow, position);
        }

        public float GetPreferedWidth(Button button)
        {
            var scale = button.TextHeight / _font.MeasureString(" ").Y;
            // Text + Arrow
            return scale * _font.MeasureString(button.Text).X + button.TextHeight;
        }

        public float GetPreferedHeight(Button button)
        {
            return button.TextHeight;
        }
    }
}