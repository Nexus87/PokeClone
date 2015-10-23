using Base;
using BattleLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleStateComponent
{
    public interface IBattleState
    {
        bool IsDone();
        void Update(BattleData data);
        void SetCharacter(ClientIdentifier id, Pokemon pkmn);
        void SetMove(ClientIdentifier id, Move move);
        void SetItem(ClientIdentifier id, Item item);
    }
}
