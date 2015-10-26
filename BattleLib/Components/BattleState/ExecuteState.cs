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

        public ExecuteState(ICommandScheduler scheduler)
        {
            this.scheduler = scheduler;
        }
        public override void Init()
        {
            done = false;
        }
        public override bool IsDone()
        {
            return done;
        }

        public override void Update(BattleData data)
        {
            scheduler.AppendCommand(data.playerCommand);
            scheduler.AppendCommand(data.aiCommand);

            foreach (var command in scheduler.ScheduleCommands())
                command.Execute();

            data.aiCommand = null;
            data.playerCommand = null;
            
            done = true;
        }
    }
}
