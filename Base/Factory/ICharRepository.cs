using Base.Data;
using System.Collections.Generic;

namespace Base.Factory
{
    public interface ICharRepository
    {
        IEnumerable<int> Ids { get; }
        PokemonData getPKData(int id);
    }
}
