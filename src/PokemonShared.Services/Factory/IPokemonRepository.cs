using System.Collections.Generic;
using PokemonShared.Data;

namespace PokemonShared.Services.Factory
{
    public interface IPokemonRepository
    {
        IEnumerable<int> Ids { get; }
        PokemonData GetPokemonData(int id);
    }
}
