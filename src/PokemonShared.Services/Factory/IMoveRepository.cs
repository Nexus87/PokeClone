using System.Collections.Generic;
using PokemonShared.Data;

namespace PokemonShared.Services.Factory
{
    public interface IMoveRepository
    {
        IReadOnlyCollection<int> Ids { get; }
        MoveData GetMoveData(int id);
    }
}
