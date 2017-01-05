using System;
using Base.Data;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using GameEngine.GUI;

namespace BattleMode.Graphic
{
    public interface IBattleGraphicController : IGuiComponent
    {
        event EventHandler ConditionSet;
        event EventHandler PokemonSet;

        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetPokemonStatus(ClientIdentifier id, StatusCondition condition);
    }
}