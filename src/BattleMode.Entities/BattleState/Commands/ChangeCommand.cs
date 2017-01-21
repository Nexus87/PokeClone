﻿using System;
using Base;
using BattleMode.Shared;

namespace BattleMode.Entities.BattleState.Commands
{
    public class ChangeCommand : ICommand
    {
        public ClientIdentifier Source { get; private set; }
        public Pokemon Pokemon { get; private set; }

        public ChangeCommand(ClientIdentifier source, Pokemon newPkmn)
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