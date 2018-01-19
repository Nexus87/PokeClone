using GameEngine.GUI;
using PokemonShared.Gui.Renderer;
using PokemonShared.Models;

namespace PokemonShared.Gui
{
    public class HpText : AbstractGuiComponent
    {
        public float PreferredTextHeight { get; set; }


        public void SetPokemon(Pokemon pokemon)
        {
            MaxHp = pokemon.MaxHp;
            CurrentHp = pokemon.Hp;
        }

        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }

    }
}