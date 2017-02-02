using System.Collections.Generic;
using Pokemon.Data;

namespace Pokemon.Services.Factory
{
    public interface IMoveRepository
    {
        IReadOnlyCollection<int> Ids { get; }
        MoveData GetMoveData(int id);
    }
}
