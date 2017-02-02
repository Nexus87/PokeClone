using System.Collections.Generic;
using GameEngine.TypeRegistry;
using PokemonShared.Data;
using PokemonShared.Models;
using PokemonShared.Services.Rules;

namespace PokemonGame.Rules
{
    [GameService(typeof(IPokemonRules))]
    public class DummyPokemonRules : IPokemonRules
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
