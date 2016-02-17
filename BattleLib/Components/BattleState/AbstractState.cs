using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    public abstract class AbstractState : IBattleState
    {
        public virtual void Init() { }
        public abstract IBattleState Update(BattleData data);

        public virtual void SetCharacter(ClientIdentifier id, Base.Pokemon pkmn) { }
        public virtual void SetMove(ClientIdentifier id, Base.Move move) { }
        public virtual void SetItem(ClientIdentifier id, Base.Item item) { }
    }
}
