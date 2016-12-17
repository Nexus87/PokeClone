using Base;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class HpText : AbstractGraphicComponent
    {
        private readonly TextBox _text;
        private string _maxHp;

        public HpText(TextBox text)
        {
            _text = text;
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
                return _text.PreferredTextHeight;
            }

            set{
                _text.PreferredTextHeight = value;
            }
        }
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _text.Draw(time, batch);
        }


        public void SetPokemon(Pokemon pokemon)
        {
            _maxHp = "/" + pokemon.MaxHP.ToString().PadLeft(3, ' ');
            SetHp(pokemon.HP);
            
        }

        protected override void Update()
        {
            _text.SetCoordinates(this);
        }
        public void SetHp(int hp)
        {
            _text.Text = hp.ToString().PadLeft(3, ' ') + _maxHp;
        }
        public override void Setup()
        {
            _text.Setup();
        }
    }
}
