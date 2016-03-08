using Base;
using BattleLib.Components.BattleState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents
{
    public interface IBattleGraphicService
    {
        event EventHandler OnHPSet;
        event EventHandler OnPokemonSet;
        event EventHandler ConditionSet;

        void SetHP(ClientIdentifier id, int hp);
        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetPokemonStatus(ClientIdentifier id, StatusCondition condition);
    }
}
