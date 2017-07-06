using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using PokemonShared.Gui.Renderer;
using PokemonShared.Models;

namespace PokemonShared.Gui
{
    public class HpText : AbstractGuiComponent
    {
        private readonly HpTextRenderer _renderer;

        public HpText(HpTextRenderer renderer)
        {
            _renderer = renderer;
        }

        public float PreferredTextHeight { get; set; }


        public void SetPokemon(Pokemon pokemon)
        {
            MaxHp = pokemon.MaxHp;
            CurrentHp = pokemon.Hp;
        }

        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }

        public override void Update()
        {
            PreferredHeight = _renderer.GetPreferredHeight(this);
            PreferredWidth = _renderer.GetPreferredWidth(this);
        }
    }
}