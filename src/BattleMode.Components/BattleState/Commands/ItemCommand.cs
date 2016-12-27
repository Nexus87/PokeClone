using System;
using Base;
using BattleMode.Shared;

namespace BattleMode.Components.BattleState.Commands
{
    public class ItemCommand : ICommand
    {
        public ClientIdentifier Source { get; private set; }
        public Item Item { get; private set; }

        public ItemCommand(ClientIdentifier source, Item item)
        {
            Source = source;
            Item = item;
        }

        public int Priority
        {
            get { throw new NotImplementedException(); }
        }

        public void Execute(CommandExecuter executer)
        {
            executer.DispatchCommand(this);
        }
    }
}
