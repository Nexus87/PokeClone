using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleLib.Interfaces;
using Base;
namespace BattleLib
{
    public class MoveCommand : IClientCommand
    {
        AbstractClient _source;
        Move _move;
        int _targetId;

        public MoveCommand(AbstractClient source, Move move, int targetId)
        {
            _source = source;
            _move = move;
            _targetId = targetId;
        }
        public CommandType Type
        {
            get { return CommandType.Move; }
        }

        public void Execute(ICommandReceiver receiver)
        {
            if (receiver == null) throw new ArgumentNullException("receiver", "Argument should not be null");

            receiver.ExecMove(_source, _move, _targetId);
        }
    }

    public class ExitCommand : IClientCommand
    {
        AbstractClient _source;
        public ExitCommand(AbstractClient source)
        {
            _source = source;
        }
        public CommandType Type
        {
            get { return CommandType.Exit; }
        }

        public void Execute(ICommandReceiver receiver)
        {
            if (receiver == null) throw new ArgumentNullException("receiver", "Argument should not be null");

            receiver.ClientExit(_source);
        }
    }

    public class ChangeCommand : IClientCommand
    {
        AbstractClient _source;
        ICharacter _character;

        public ChangeCommand(AbstractClient source, ICharacter character)
        {
            _source = source;
            _character = character;
        }
        public CommandType Type
        {
            get { return CommandType.Change; }
        }

        public void Execute(ICommandReceiver receiver)
        {
            if (receiver == null) throw new ArgumentNullException("receiver", "Argument should not be null");

            receiver.ExecChange(_source, _character);
        }
    }


}
