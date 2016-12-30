using Base;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Shared
{
    [GameType]
    public class HpText : AbstractGraphicComponent
    {
        private readonly Label _text;
        private string _maxHp;

        public HpText(Label text)
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
                return _text.TextSize;
            }

            set{
                _text.TextSize = value;
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
