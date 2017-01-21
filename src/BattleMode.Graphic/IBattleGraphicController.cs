using Base.Data;
using Base.Rules;
using BattleMode.Shared;
using GameEngine.Graphics;

namespace BattleMode.Graphic
{
    public interface IBattleGraphicController
    {
        void SetPokemon(ClientIdentifier id, PokemonEntity pokemon);
        void SetPokemonStatus(ClientIdentifier id, StatusCondition condition);

        Scene Scene { get; }
    }
}