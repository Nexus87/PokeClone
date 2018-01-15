using System.Collections.Generic;
using PokemonShared.Data;
using PokemonShared.Models;

namespace PokemonGame.Rules
{
    public class DummyPokemonRules
    {
        public Stats GenerateIv()
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
