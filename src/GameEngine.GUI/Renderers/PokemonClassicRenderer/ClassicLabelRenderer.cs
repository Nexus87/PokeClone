using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicLabelRenderer : LabelRenderer
    {
        private readonly ISpriteFont _font;

        public ClassicLabelRenderer(ISpriteFont font)
        {
            _font = font;
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, Label label)
        {
            RenderText(spriteBatch, _font, label.Text, label.Area, label.TextSize);
        }

        public override float GetPreferedWidth(Label label)
        {
            var scale = label.TextSize / _font.MeasureString(" ").Y;
            return scale * _font.MeasureString(label.Text).X;
        }

        public override float GetPreferedHeight(Label label)
        {
            return label.TextSize;
        }
    }
}