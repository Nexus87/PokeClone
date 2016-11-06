using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicButtonRenderer : AbstractRenderer, IButtonRenderer
    {
        public float PreferedWidth => TextHeight + _font.MeasureString(Text).Y;
        public float PreferedHeight => TextHeight;

        public bool IsSelected { get; set; }
        public string Text { get; set; }
        public float TextHeight { get; set; }

        private ITexture2D _arrow;
        private ISpriteFont _font;

        public override void Render(IArea area)
        {
            SpriteBatch.GraphicsDevice.ScissorRectangle = area.ScissorArea;
            if (IsSelected)
                DrawArrow(area.Constraints);
            DrawText(area.Constraints);
        }

        private void DrawText(Rectangle position)
        {
            // Position + arrow width
            position.X += (int) TextHeight;

            RenderText(_font, Text, position, TextHeight);
        }

        private void DrawArrow(Rectangle position)
        {
            position.Height = (int) TextHeight;
            position.Width = (int) TextHeight;

            RenderImage(_arrow, position);
        }
    }
}