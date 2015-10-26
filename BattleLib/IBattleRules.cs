using Base;
using System;

namespace BattleLib
{
    public class OnDamageTakenArgs : EventArgs
    {
        public bool critical;
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
        
        bool ExecMove(Pokemon source, Move move, Pokemon target);
        bool ExecChange(Pokemon oldPkmn, Pokemon newPkmn);
        bool UseItem(Pokemon target, Item item);
    }
}
