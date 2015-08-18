using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleLib.Interfaces;
using Base;
namespace BattleLib
{
    class MoveCommand : IClientCommand
    {
        IBattleClient _source;
        Move _move;
        int _targetId;

        public MoveCommand(IBattleClient source, Move move, int targetId)
        {
            _source = source;
            _move = move;
            _targetId = targetId;
        }
        public CommandType Type
        {
            get { return CommandType.Move; }
        }

        public void execute(CommandReceiver receiver)
        {
            receiver.execMove(_source, _move, _targetId);
        }
    }

    class ExitCommand : IClientCommand
    {
        IBattleClient _source;
        public ExitCommand(IBattleClient source)
        {
            _source = source;
        }
        public CommandType Type
        {
            get { return CommandType.Exit; }
        }

        public void execute(CommandReceiver receiver)
        {
            receiver.clientExit(_source);
        }
    }

    class ChangeCommand : IClientCommand
    {
        IBattleClient _source;
        ICharakter _character;

        public ChangeCommand(IBattleClient source, ICharakter character)
        {
            _source = source;
            _character = character;
        }
        public CommandType Type
        {
            get { return CommandType.Change; }
        }

        public void execute(CommandReceiver receiver)
        {
            receiver.execChange(_source, _character);
        }
    }


}
