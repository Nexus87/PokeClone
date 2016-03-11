using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Rules
{
    public class RulesSet
    {
        public IBattleRules BattleRules { get; private set; }
        public IPokemonRules PokemonRules { get; private set; }
        public ITypeTable TypeTable { get; private set; }

        public RulesSet(IBattleRules battleRules, IPokemonRules pokemonRules, ITypeTable typeTable)
        {
            BattleRules = battleRules;
            PokemonRules = pokemonRules;
            TypeTable = typeTable;
        }
    }
}
