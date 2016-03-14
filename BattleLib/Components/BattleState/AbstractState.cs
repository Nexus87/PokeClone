using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    public abstract class AbstractState : IBattleState
    {
        public abstract BattleStates State { get; }
        public virtual void Init() { }
        public abstract IBattleState Update(BattleData data);

        public virtual void SetCharacter(ClientIdentifier id, Pokemon pkmn) { throw new InvalidOperationException("Wong operation"); }
        public virtual void SetMove(ClientIdentifier id, ClientIdentifier target, Move move) { throw new InvalidOperationException("Wong operation"); }
        public virtual void SetItem(ClientIdentifier id, ClientIdentifier target, Item item) { throw new InvalidOperationException("Wong operation"); }
    }
}
