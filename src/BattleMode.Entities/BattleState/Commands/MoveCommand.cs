using PokemonShared.Models;

namespace BattleMode.Entities.BattleState.Commands
{
    public class MoveCommand : ICommand
    {
        public Move Move { get; }

        public MoveCommand(Move move)
        {
            Move = move;
        }
    }
}