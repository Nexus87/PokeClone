using System.Collections.Generic;
using GameEngine.TypeRegistry;
using Pokemon.Data;
using Pokemon.Services.Rules;
using Pokemon.Models;

namespace PokemonGame.Rules
{
    [GameService(typeof(IPokemonRules))]
    public class DummyPokemonRules : IPokemonRules
    {
        public Stats GenerateIv()
        {
            return new Stats();
        }

        public IEnumerable<Move> LevelUp(Pokemon.Models.Pokemon character)
        {
            return new List<Move>();
        }

        public void ToLevel(Pokemon.Models.Pokemon character, int level)
        {
            
        }

        public Pokemon.Models.Pokemon FromPokemonData(PokemonData data)
        {
            return new Pokemon.Models.Pokemon(data, new Stats());
        }
    }
}
