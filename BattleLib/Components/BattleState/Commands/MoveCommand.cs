using Base;

namespace BattleLib.Components.BattleState.Commands
{
    public class MoveCommand : ICommand
    {
        public Move move;

        public MoveCommand(ClientIdentifier source, ClientIdentifier target, Move move)
        {
            this.move = move;
            this.Source = source;
            this.Target = target;
        }

        public int Priority
        {
            get { return move.Priority; }
        }

        public ClientIdentifier Source { get; private set; }
        public ClientIdentifier Target { get; private set; }

        public CommandType Type
        {
            get { return CommandType.Move; }
        }

        public void Execute(CommandExecuter executer, BattleData data)
        {
            PokemonWrapper source = data.GetPokemon(Source);
            PokemonWrapper target = data.GetPokemon(Target);

            executer.ExecMove(source, move, target);
        }
    }
}