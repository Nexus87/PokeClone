using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Interfaces
{
    public abstract class AbstractClient
    {
        internal protected IEnumerable<ClientInfo> BattleState { get; internal set; }
        internal protected int Id { get; internal set; }

        protected IClientCommand exitCommand()
        {
            return new ExitCommand(this);
        }

        protected IClientCommand moveCommand(Move move, int targetId)
        {
            return new MoveCommand(this, move, targetId);
        }

        protected IClientCommand changeCommand(ICharakter charakter)
        {
            return new ChangeCommand(this, charakter);
        }

        public abstract string ClientName { get; }
        public abstract IClientCommand requestAction();
        public abstract ICharakter requestCharakter();
    }
}
