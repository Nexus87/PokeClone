using Base.Data;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using GameEngine.Graphics;

namespace BattleMode.Graphic
{
    public interface IBattleGraphicController
    {
        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetPokemonStatus(ClientIdentifier id, StatusCondition condition);

        Scene Scene { get; }
    }
}