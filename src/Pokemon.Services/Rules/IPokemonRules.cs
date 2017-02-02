using System.Collections.Generic;
using Pokemon.Data;
using Pokemon.Models;

namespace Pokemon.Services.Rules
{
    public interface IPokemonRules
    {
        Stats GenerateIv();

        IEnumerable<Move> LevelUp(Models.Pokemon character);
        void ToLevel(Models.Pokemon character, int level);

        Models.Pokemon FromPokemonData(PokemonData data);
    }
}