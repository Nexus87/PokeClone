using Base;
using BattleLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleStateComponent
{
    internal interface IBattleState
    {
        void Update();
        void SetCharacter(Pokemon pkmn);
        void SetAction(IClientCommand command);
    }
}
