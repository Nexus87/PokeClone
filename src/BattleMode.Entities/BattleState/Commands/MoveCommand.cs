﻿using BattleMode.Shared;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState.Commands
{
    public class MoveCommand : ICommand
    {
        public Move Move { get; }

        public MoveCommand(ClientIdentifier source, ClientIdentifier target, Move move)
        {
            Move = move;
            Source = source;
            Target = target;
        }

        public int Priority => Move.Priority;

        public ClientIdentifier Source { get; }
        public ClientIdentifier Target { get; }


        public void Execute(CommandExecuter executer)
        {
            executer.DispatchCommand(this);
        }
    }
}