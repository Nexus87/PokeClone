﻿using Base;
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

        public void Execute(CommandExecuter executer, BattleData data)
        {
            executer.UseItem(data.GetPokemon(Source), item);
        }
    }
}
