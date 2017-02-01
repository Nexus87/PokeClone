using Base.Data;
using System.Collections.Generic;

namespace Base.Rules
{
    public interface IPokemonRules
    {
        Stats GenerateIv();

        IEnumerable<Move> LevelUp(Pokemon character);
        void ToLevel(Pokemon character, int level);

        Pokemon FromPokemonData(PokemonData data);
    }
}