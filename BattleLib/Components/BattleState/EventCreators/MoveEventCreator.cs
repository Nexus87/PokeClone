using Base;
using BattleLib.Components.BattleState.Commands;
using GameEngine.EventComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState.EventCreators
{
    class MoveEventCreator : IEventCreator
    {
        class MoveInfo
        {
            public List<int> damages = new List<int>();
            public List<PokemonWrapper> targets = new List<PokemonWrapper>();
            public bool success = false;

            public List<string> changedStates = new List<string>();
            public bool conditionChanged = false;
            public StatusCondition newCondition;
            public StatusCondition oldCondition;
        }

        private MoveInfo info;
        private Move move;
        private ClientIdentifier source;


        public void StartCommand(ICommand command)
        {
            source = command.Source;
            info = new MoveInfo();
        }

        public void ItemUsed(ItemUsedArgs e)
        {
            throw new NotImplementedException();
        }

        public void MoveUsed(MoveUsedArgs e)
        {
            move = e.move;
        }

        public void ConditionChangedHandler(OnConditionChangedArgs e)
        {
            info.conditionChanged = true;
            info.newCondition = e.condition;
            info.oldCondition = e.oldCondition;
        }

        public void ActionFailedHandler()
        {
            info.success = false;
        }

        public void EndCommand()
        {
            throw new NotImplementedException();
        }
    }
}
