using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState.Commands
{
    public class ChangeCommand : ICommand
    {
        ClientIdentifier id;
        Pokemon newPkmn;

        public ChangeCommand(ClientIdentifier id, Pokemon newPkmn)
        {
            this.id = id;
            this.newPkmn = newPkmn;
        }

        public CommandType Type
        {
            get { return CommandType.Change; }
        }

        public int Priority
        {
            get { throw new NotImplementedException(); }
        }

        public void Execute(IBattleRules rules, BattleData data)
        {
            Pokemon source = null;
            if (id == data.player)
            {
                source = data.PlayerPkmn;
                if (!rules.ExecChange(source, newPkmn))
                    return;

                data.PlayerPkmn = newPkmn;
            }
            else if (id == data.ai)
            {
                source = data.AIPkmn;

                if (!rules.ExecChange(source, newPkmn))
                    return;

                data.AIPkmn = newPkmn;
            }
            else
                throw new InvalidOperationException("Source id not found");
        }
    }
}
