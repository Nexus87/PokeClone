using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleLib;
using Base;
using BattleLib.Interfaces;
namespace ConsoleClient
{
    class WaitForInputArgs : EventArgs
    {
        public Pokemon Current;
        public PlayerClient Client;
    }
    delegate void WaitForInput(object sender, EventArgs args);
    class PlayerClient : AbstractClient
    {
        public event WaitForInput WaitForInputEvent = (a, b) => { };

        Player _player;
        Pokemon _current;
        public IClientCommand Command;
        public PlayerClient(Player player)
        {
            _player = player;
        }

        public override string ClientName
        {
            get { return "Player"; }
        }

        public new IClientCommand ExitCommand()
        {
            return base.ExitCommand();
        }

        public new IClientCommand MoveCommand(Move move, int targetId)
        {
            return base.MoveCommand(move, targetId);
        }

        public new IClientCommand ChangeCommand(ICharacter charakter)
        {
            return base.ChangeCommand(charakter);
        }
        public override IClientCommand RequestAction()
        {
            WaitForInputEvent(this, new WaitForInputArgs {Current = _current, Client = this });
            return Command;
        }
        public int searchTarget()
        {
            return (from info in BattleState
                    where info.ClientId != Id
                    select info.ClientId).First();
        }

        public override Base.ICharacter RequestCharacter()
        {
            _current = (from chars in _player._pkm
                    where !chars.IsKO()
                    select chars).FirstOrDefault();

            return _current;
        }
    }
}
