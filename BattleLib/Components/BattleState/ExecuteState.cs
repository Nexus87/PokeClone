using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    public class ExecuteState : AbstractState
    {
        bool done = false;
        ICommandScheduler scheduler;
        IBattleRules rules;

        BattleStateComponent state;

        public ExecuteState(BattleStateComponent state, ICommandScheduler scheduler, IBattleRules rules)
        {
            this.scheduler = scheduler;
            this.rules = rules;
            this.state = state;
        }
        public override void Init()
        {
            done = false;
        }
        public bool IsDone()
        {
            return done;
        }

        public override IBattleState Update(BattleData data)
        {
            scheduler.AppendCommand(data.playerCommand);
            scheduler.AppendCommand(data.aiCommand);

            foreach (var command in scheduler.ScheduleCommands())
                command.Execute(rules, data);

            data.aiCommand = null;
            data.playerCommand = null;
            
            done = true;

            if (data.AIPkmn.HP == 0 || data.PlayerPkmn.HP == 0)
                return state.charState;

            return state.actionState;
        }
    }
}
