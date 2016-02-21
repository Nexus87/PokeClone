using BattleLib.Components.BattleState.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState.EventCreators
{
    interface IEventCreator
    {
        void StartCommand(ICommand command);

        void DamageTakenHandler(OnDamageTakenArgs e);
        void ConditionChangedHandler(OnConditionChangedArgs e);
        void StatsChangedHandler(OnStatsChangedArgs e);
        void ActionFailedHandler();

        void EndCommand();
    }
}
