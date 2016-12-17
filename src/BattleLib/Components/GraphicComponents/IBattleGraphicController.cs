using System;
using Base.Data;
using BattleLib.Components.BattleState;
using GameEngine.GUI.Graphics;

namespace BattleLib.Components.GraphicComponents
{
    public interface IBattleGraphicController : IGraphicComponent
    {
        event EventHandler ConditionSet;
        event EventHandler HpSet;
        event EventHandler PokemonSet;

        void SetHp(ClientIdentifier id, int hp);
        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetPokemonStatus(ClientIdentifier id, StatusCondition condition);
    }
}