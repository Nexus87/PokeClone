using System.Collections.Generic;
using Pokemon.Data;

namespace Pokemon.Services.Factory
{
    public interface IPokemonRepository
    {
        IEnumerable<int> Ids { get; }
        PokemonData GetPokemonData(int id);
    }
}
