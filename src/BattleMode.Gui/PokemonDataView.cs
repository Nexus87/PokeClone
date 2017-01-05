using BattleMode.Entities.BattleState;

namespace BattleMode.Gui
{
    public interface IPokemonDataView
    {
        int CurrentHp { get; }
        void SetHp(int newHp);
        void SetPokemon(PokemonWrapper pokemon);
        void Show();
    }
}