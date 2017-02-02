﻿using System;
using BattleMode.Shared;

namespace BattleMode.Entities.BattleState.Commands
{
    public class ChangeCommand : ICommand
    {
        public ClientIdentifier Source { get; private set; }
        public PokemonShared.Models.Pokemon Pokemon { get; private set; }

        public ChangeCommand(ClientIdentifier source, PokemonShared.Models.Pokemon newPkmn)
        {
            Source = source;
            Pokemon = newPkmn;
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
