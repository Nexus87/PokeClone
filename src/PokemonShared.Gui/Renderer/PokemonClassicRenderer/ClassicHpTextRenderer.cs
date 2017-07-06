using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;

namespace PokemonShared.Gui.Renderer.PokemonClassicRenderer
{
    public class ClassicHpTextRenderer : HpTextRenderer
    {
        private ISpriteFont _font;

        public override void Init(TextureProvider textureProvider)
        {
            _font = textureProvider.GetFont(ClassicSkin.DefaultFont);
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, HpText component)
        {
            RenderText(spriteBatch, _font, DrawnText(component), component.Area, component.PreferredTextHeight);
        }

        public override float GetPreferredWidth(HpText hpText)
        {
            var text = DrawnText(hpText);
            return _font.MeasureString(text).X;
        }

        private static string DrawnText(HpText hpText)
        {
            return hpText.CurrentHp.ToString().PadLeft(3) + "/" + hpText.MaxHp.ToString().PadLeft(3);
        }

        public override float GetPreferredHeight(HpText hpText)
        {
            return hpText.PreferredTextHeight;
        }
    }
}