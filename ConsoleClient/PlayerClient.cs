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

        public new IClientCommand exitCommand()
        {
            return base.exitCommand();
        }

        public new IClientCommand moveCommand(Move move, int targetId)
        {
            return base.moveCommand(move, targetId);
        }

        public new IClientCommand changeCommand(ICharakter charakter)
        {
            return base.changeCommand(charakter);
        }
        public override IClientCommand requestAction()
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

        public override Base.ICharakter requestCharakter()
        {
            _current = (from chars in _player._pkm
                    where !chars.isKO()
                    select chars).FirstOrDefault();

            return _current;
        }
    }
}
