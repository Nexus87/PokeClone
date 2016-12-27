using Base;

namespace BattleMode.Core.Components.BattleState.Commands
{
    public class MoveCommand : ICommand
    {
        public Move Move { get; private set; }

        public MoveCommand(ClientIdentifier source, ClientIdentifier target, Move move)
        {
            this.Move = move;
            this.Source = source;
            this.Target = target;
        }

        public int Priority
        {
            get { return Move.Priority; }
        }

        public ClientIdentifier Source { get; private set; }
        public ClientIdentifier Target { get; private set; }


        public void Execute(CommandExecuter executer)
        {
            executer.DispatchCommand(this);
        }
    }
}