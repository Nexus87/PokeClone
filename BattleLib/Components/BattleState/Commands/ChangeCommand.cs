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
        public ClientIdentifier Source { get; private set; }
        Pokemon newPkmn;

        public ChangeCommand(ClientIdentifier source, Pokemon newPkmn)
        {
            this.Source = source;
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

        public void Execute(CommandExecuter executer, BattleData data)
        {
            executer.ChangePokemon(data.GetPkmn(Source), newPkmn);
        }
    }
}
