using Base.Data;
using System.Collections.Generic;

namespace Base.Rules
{
    public interface ICharacterRules
    {
        Stats GenerateIV();

        IEnumerable<Move> LevelUp(Pokemon character);

        void ToLevel(Pokemon character, int level);

        Pokemon ToPokemon(PokemonData data);
    }
}