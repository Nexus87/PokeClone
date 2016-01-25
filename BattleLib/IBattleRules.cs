using Base;
using BattleLib.Components.BattleState;
using System;

namespace BattleLib
{
    public class OnDamageTakenArgs : EventArgs
    {
        public bool hit;
        public bool critical;
        public bool effective;
        public Pokemon pkmn;
    }

    public class OnStatsChangedArgs : EventArgs
    {
        public State state;
        public bool lowered;
        public Pokemon pkmn;
    }

    public class OnConditionChangedArgs : EventArgs
    {
        public StatusCondition condition;
        public Pokemon pkmn;
    }

    public interface IBattleRules
    {
        event EventHandler<OnDamageTakenArgs> OnDamageTaken;
        event EventHandler<OnStatsChangedArgs> OnStatsChanged;
        event EventHandler<OnConditionChangedArgs> OnConditionChanged;

        bool CanEscape();
        bool CanChange();

        void ExecMove(PokemonWrapper source, Move move, PokemonWrapper target);
        bool TryChange(PokemonWrapper oldPkmn, Pokemon newPkmn);
        bool UseItem(PokemonWrapper target, Item item);
    }
}
