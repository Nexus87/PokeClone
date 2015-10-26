using Base;
using System.Collections.Generic;

namespace BattleLib.Interfaces
{
    public abstract class AbstractClient
    {
        internal protected IEnumerable<ClientInfo> BattleState { get; internal set; }
        internal protected int Id { get; internal set; }

        protected IClientCommand ExitCommand()
        {
            return new ExitCommand(this);
        }

        protected IClientCommand MoveCommand(Move move, int targetId)
        {
            return new MoveCommand(this, move, targetId);
        }

        protected IClientCommand ChangeCommand(Pokemon character)
        {
            return new ChangeCommand(this, character);
        }

        public abstract string ClientName { get; }
        public abstract IClientCommand RequestAction();
        public abstract Pokemon RequestCharacter();
    }
}
