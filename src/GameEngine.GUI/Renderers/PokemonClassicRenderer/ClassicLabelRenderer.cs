using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicLabelRenderer : LabelRenderer
    {
        private ISpriteFont _font;

        public override void Init(TextureProvider textureProvider)
        {
            _font = textureProvider.GetFont(ClassicSkin.DefaultFont);
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, Label label)
        {
            RenderText(spriteBatch, _font, label.Text, label.Area, label.TextHeight);
        }

        public override float GetPreferedWidth(Label label)
        {
            var scale = label.TextHeight / _font.MeasureString(" ").Y;
            return scale * _font.MeasureString(label.Text).X;
        }

        public override float GetPreferedHeight(Label label)
        {
            return label.TextHeight;
        }
    }
}