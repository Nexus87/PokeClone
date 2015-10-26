using Base;
using BattleLib.Interfaces;
using System;
namespace BattleLib
{
    public class ItemCommand : IClientCommand
    {
        Item item;

        public ItemCommand(Item item)
        {
            this.item = item;
        }
        public CommandType Type
        {
            get { return CommandType.Item; }
        }

        public void Execute(ICommandReceiver receiver)
        {
            throw new NotImplementedException();
        }


        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
    public class MoveCommand : IClientCommand
    {
        AbstractClient _source;
        Move _move;
        int _targetId;

        public MoveCommand(Move move)
        {
            _move = move;
        }

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


        public void Execute()
        {
            throw new NotImplementedException();
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


        public void Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class ChangeCommand : IClientCommand
    {
        AbstractClient _source;
        ICharacter _character;

        public ChangeCommand(Pokemon newChar)
        {
            _character = newChar;
        }

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


        public void Execute()
        {
            throw new NotImplementedException();
        }
    }


}
