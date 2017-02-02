using Pokemon.Services.Rules;

namespace BattleMode.Gui
{
    public interface IPokemonDataView
    {
        int CurrentHp { get; }
        void SetHp(int newHp);
        void SetPokemon(PokemonEntity pokemon);
        void Show();
    }
}