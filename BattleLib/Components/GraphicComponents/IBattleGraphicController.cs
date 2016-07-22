using Base.Data;
using BattleLib.Components.BattleState;
using GameEngine.Graphics;
using System;

namespace BattleLib.GraphicComponents
{
    public interface IBattleGraphicController : IGraphicComponent
    {
        event EventHandler ConditionSet;
        event EventHandler OnHPSet;
        event EventHandler OnPokemonSet;

        void SetHP(ClientIdentifier id, int hp);
        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetPokemonStatus(ClientIdentifier id, StatusCondition condition);
    }
}