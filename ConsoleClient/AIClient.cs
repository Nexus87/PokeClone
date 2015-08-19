using Base;
using BattleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class AIClient : IBattleClient
    {
        Pokemon _current;
        public List<Pokemon> _chars = new List<Pokemon>();
        public IBattleObserver Observer { get; set; }
        public string ClientName
        {
            get { return "AI"; }
        }

        int searchTarget()
        {
            return (from info in Observer.getAllInfos()
                    where info.ClientName != ClientName
                    select info.Id).First();
        }
        public void requestAction(IBattleState state)
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, _current.Moves.Count());
            var move = _current.Moves[toSkip];
            state.makeMove(move, this, searchTarget());
        }

        public Base.ICharakter requestCharakter()
        {
            _current = (from character in _chars
                        where !character.isKO()
                        select character).FirstOrDefault();

            return _current;
        }
    }
}
