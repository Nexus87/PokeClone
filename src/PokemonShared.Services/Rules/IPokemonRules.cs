using System.Collections.Generic;
using PokemonShared.Data;
using PokemonShared.Models;

namespace PokemonShared.Services.Rules
{
    public interface IPokemonRules
    {
        Stats GenerateIv();

        IEnumerable<Move> LevelUp(Pokemon character);
        void ToLevel(Pokemon character, int level);

        Pokemon FromPokemonData(PokemonData data);
    }
}