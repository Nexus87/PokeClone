using Base;
using BattleLib;
using BattleLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class AIClient : AbstractClient
    {
        Pokemon _current;
        public List<Pokemon> _chars = new List<Pokemon>();
        public override string ClientName
        {
            get { return "AI"; }
        }

        int searchTarget()
        {
            return (from info in BattleState
                    where info.ClientId != Id
                    select info.ClientId).First();
        }
        public override IClientCommand requestAction()
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, _current.Moves.Count());
            var move = _current.Moves[toSkip];
            return moveCommand(move, searchTarget());
        }

        public override Base.ICharakter requestCharakter()
        {
            _current = (from character in _chars
                        where !character.isKO()
                        select character).FirstOrDefault();

            return _current;
        }
    }
}
