using Base;
using Base.Data;
using Base.Rules;
using System.Collections.Generic;

namespace Game.Rules
{
    class DummyPokemonRules : IPokemonRules
    {
        public Stats GenerateIV()
        {
            return new Stats();
        }

        public IEnumerable<Move> LevelUp(Pokemon character)
        {
            return new List<Move>();
        }

        public void ToLevel(Pokemon character, int level)
        {
            
        }

        public Pokemon FromPokemonData(PokemonData data)
        {
            return new Pokemon(data, new Stats());
        }
    }
}
