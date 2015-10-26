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
        ClientIdentifier id;
        Item item;

        public ItemCommand(ClientIdentifier id, Item item)
        {
            this.id = id;
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
            if (id == data.player)
                rules.UseItem(data.PlayerPkmn, item);
            else if (id == data.ai)
                rules.UseItem(data.AIPkmn, item);
            else
                throw new InvalidOperationException("Id is unknown.");
        }
    }
}
