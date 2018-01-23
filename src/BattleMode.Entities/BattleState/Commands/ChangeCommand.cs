using System;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState.Commands
{
    public class ChangeCommand : ICommand
    {
        public Pokemon Pokemon { get; }

        public ChangeCommand(Pokemon newPkmn)
        {
            Pokemon = newPkmn;
        }
    }
}
