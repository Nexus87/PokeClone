using BattleMode.Entities.BattleState;

namespace BattleMode.Gui
{
    public interface IPokemonDataView
    {
        void SetHp(int newHp);
        void SetPokemon(PokemonWrapper pokemon);
        void Show();
    }
}