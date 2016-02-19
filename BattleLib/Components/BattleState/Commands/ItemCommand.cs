using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState.Commands
{
    public class ItemCommand : ICommand
    {
        public ClientIdentifier Source { get; private set; }
        Item item;

        public ItemCommand(ClientIdentifier source, Item item)
        {
            this.Source = source;
            this.item = item;
        }

        public CommandType Type
        {
            get { return CommandType.Item; }
        }

        public int Priority
        {
            get { throw new NotImplementedException(); }
        }

        public void Execute(IBattleRules rules, BattleData data)
        {
            if (Source == data.player)
                rules.UseItem(data.PlayerPkmn, item);
            else if (Source == data.ai)
                rules.UseItem(data.AIPkmn, item);
            else
                throw new InvalidOperationException("Id is unknown.");
        }
    }
}
