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

        void ItemUsed(ItemUsedArgs e);
        void MoveUsed(MoveUsedArgs e);
        void ConditionChangedHandler(OnConditionChangedArgs e);
        void ActionFailedHandler();

        void EndCommand();
    }
}
