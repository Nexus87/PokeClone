using Base.Data;
using System.Collections.Generic;

namespace Base.Factory
{
    public interface IPokemonRepository
    {
        IEnumerable<int> Ids { get; }
        PokemonData GetPokemonData(int id);
    }
}
