using Base;
using BattleLib.Components.BattleState;
using System;

namespace BattleLib
{
    public class OnDamageTakenArgs : EventArgs
    {
        public enum Efficency
        {
            notEffective,
            normal,
            veryEffective
        };

        public bool hit;
        public bool critical;
        public Efficency effective;
        public int newHP;
        public PokemonWrapper pkmn;
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
        public StatusCondition oldCondition;
        public Pokemon pkmn;
    }

    public class ItemUsedArgs : EventArgs
    {
        public Item item;
    }

    public class OnActionFailedArgs : EventArgs
    {
        public bool HasMissed;
        public bool HasResistance;
    }

    public class MoveUsedArgs : EventArgs
    {
        public Move move;
    }
    public interface IBattleRules
    {
        event EventHandler OnActionFailed;
        event EventHandler<OnDamageTakenArgs> OnDamageTaken;
        event EventHandler<OnStatsChangedArgs> OnStatsChanged;
        event EventHandler<OnConditionChangedArgs> OnConditionChanged;
        event EventHandler<ItemUsedArgs> ItemUsed;
        event EventHandler<MoveUsedArgs> MoveUsed;

        bool CanEscape();
        bool CanChange();

        void ExecMove(PokemonWrapper source, Move move, PokemonWrapper target);
        bool TryChange(PokemonWrapper oldPkmn, Pokemon newPkmn);
        bool UseItem(PokemonWrapper target, Item item);
    }
}
