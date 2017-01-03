using System;
using Base.Data;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using GameEngine.GUI;

namespace BattleMode.Core.Components.GraphicComponents
{
    public interface IBattleGraphicController : IGuiComponent
    {
        event EventHandler ConditionSet;
        event EventHandler HpSet;
        event EventHandler PokemonSet;

        void SetHp(ClientIdentifier id, int hp);
        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetPokemonStatus(ClientIdentifier id, StatusCondition condition);
    }
}