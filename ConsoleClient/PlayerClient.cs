using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleLib;
using Base;
namespace ConsoleClient
{
    class WaitForInputArgs : EventArgs
    {
        public IBattleState State;
        public Pokemon Current;
        public IBattleClient Client;
    }
    delegate void WaitForInput(object sender, EventArgs args);
    class PlayerClient : IBattleClient
    {
        public event WaitForInput WaitForInputEvent = (a, b) => { };

        Player _player;
        Pokemon _current;
        public PlayerClient(Player player)
        {
            _player = player;
        }

        public string ClientName
        {
            get { return "Player"; }
        }

        public void requestAction(IBattleState state)
        {
            WaitForInputEvent(this, new WaitForInputArgs { State = state, Current = _current, Client = this });
        }

        public Base.ICharakter requestCharakter()
        {
            _current = (from chars in _player._pkm
                    where !chars.isKO()
                    select chars).FirstOrDefault();

            return _current;
        }
    }
}
