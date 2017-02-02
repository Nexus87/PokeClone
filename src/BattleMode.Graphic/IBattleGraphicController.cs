using BattleMode.Shared;
using GameEngine.Graphics;
using PokemonShared.Models;

namespace BattleMode.Graphic
{
    public interface IBattleGraphicController
    {
        void SetPokemon(ClientIdentifier id, PokemonEntity pokemon);
        void SetPokemonStatus(ClientIdentifier id, StatusCondition condition);

        Scene Scene { get; }
    }
}