using Base;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using GameEngine.Graphics.General;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class HPText : AbstractGraphicComponent
    {
        private TextBox text;
        private string maxHP;

        public HPText(TextBox text)
        {
            this.text = text;
            text.PreferredSizeChanged += (obj, ev) => SetPreferredSize(ev);
        }

        private void SetPreferredSize(GraphicComponentSizeChangedEventArgs ev)
        {
            PreferredWidth = ev.Width;
            PreferredHeight = ev.Height;
        }

        public float PreferredTextHeight
        {
            get
            {
                return text.PreferredTextHeight;
            }

            set{
                text.PreferredTextHeight = value;
            }
        }
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            text.Draw(time, batch);
        }


        public void SetPokemon(Pokemon pokemon)
        {
            maxHP = "/" + pokemon.MaxHP.ToString().PadLeft(3, ' ');
            SetHP(pokemon.HP);
            
        }

        protected override void Update()
        {
            text.SetCoordinates(this);
        }
        public void SetHP(int hp)
        {
            text.Text = hp.ToString().PadLeft(3, ' ') + maxHP;
        }
        public override void Setup()
        {
            text.Setup();
        }
    }
}
