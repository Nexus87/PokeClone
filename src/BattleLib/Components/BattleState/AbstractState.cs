using System;
using Base;

namespace BattleMode.Core.Components.BattleState
{
    public abstract class AbstractState : IBattleState
    {
        public abstract BattleStates State { get; }
        public abstract void Update(BattleData data);

        public virtual void SetCharacter(ClientIdentifier id, Pokemon pkmn) { throw new InvalidOperationException("Wong operation"); }
        public virtual void SetMove(ClientIdentifier id, ClientIdentifier target, Move move) { throw new InvalidOperationException("Wong operation"); }
        public virtual void SetItem(ClientIdentifier id, ClientIdentifier target, Item item) { throw new InvalidOperationException("Wong operation"); }

        public virtual void Init(BattleData data)
        {
        }

        public bool IsDone { get; protected set; }
    }
}
