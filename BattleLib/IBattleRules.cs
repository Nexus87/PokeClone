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
    }

    public class OnActionFailedArgs : EventArgs
    {
        public bool HasMissed;
        public bool HasResistance;
    }

    public class MoveEffect
    {
        public PokemonWrapper target;

        public bool damage;
        public bool critical;
        public MoveEfficency effective;

        public bool conditionChanged;
        public StatusCondition oldCondition;

        public bool stateChanged;
        public State state;
        public bool lowered;

    }
    public class MoveUsedArgs : EventArgs
    {
        public Move move;
        public ClientIdentifier source;
        public List<MoveEffect> effects = new List<MoveEffect>();
    }

    public interface IBattleRules
    {
        event EventHandler<ChangeUsedArgs> ChangeUsed;
        event EventHandler<ItemUsedArgs> ItemUsed;
        event EventHandler<MoveUsedArgs> MoveUsed;

        bool CanEscape();
        bool CanChange();

        void ExecMove(PokemonWrapper source, Move move, PokemonWrapper target);
        bool TryChange(PokemonWrapper oldPkmn, Pokemon newPkmn);
        bool UseItem(PokemonWrapper target, Item item);
    }
}
