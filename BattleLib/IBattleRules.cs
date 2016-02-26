using Base;
using BattleLib.Components.BattleState;
using System;
using System.Collections.Generic;

namespace BattleLib
{

    public enum ChangeFailedReasons
    {
        blocked
    }

    public enum MoveFailedReasons
    {
        noEffect,
        missed,

    }

    public enum MoveEfficency
    {
        notEffective,
        normal,
        veryEffective
    }

    public class ChangeUsedArgs : EventArgs
    {
        bool success;
        public PokemonWrapper newPokemon;
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
        public bool success;
    }

    public class OnActionFailedArgs : EventArgs
    {
        public bool HasMissed;
        public bool HasResistance;
    }


    public class MoveUsedArgs : EventArgs
    {
        public Move move;
        public ClientIdentifier source;
    }

    public interface IBattleRules
    {
        event EventHandler<ChangeUsedArgs> ChangeUsed;
        event EventHandler<ItemUsedArgs> ItemUsed;
        event EventHandler<MoveUsedArgs> MoveUsed;

        event EventHandler<HPReductionArgs> HPReduction;
        event EventHandler<ConditionChangeArgs> ConditionChange;
        event EventHandler<StateChangeArgs> StateChange;

        bool CanEscape();
        bool CanChange();

        void ExecMove(PokemonWrapper source, Move move, PokemonWrapper target);
        bool TryChange(PokemonWrapper oldPkmn, Pokemon newPkmn);
        bool UseItem(PokemonWrapper target, Item item);
    }

    public class HPReductionArgs : EventArgs
    {
        public ClientIdentifier target;
        public int oldHP;
        public int newHP;

        public bool success;
        public MoveFailedReasons resason;

        public bool critical;
        public MoveEfficency effective;

    }

    class ConditionChangeArgs : EventArgs
    {
        public ClientIdentifier target;
        public bool success;
        public MoveFailedReasons resason;

        public StatusCondition oldCondition;
        public StatusCondition newCondition;
    }

    class StateChangeArgs : EventArgs
    {
        public ClientIdentifier target;
        public bool success;
        public MoveFailedReasons resason;

        public State state;
        public int oldState;
        public int newState;
    }
}
