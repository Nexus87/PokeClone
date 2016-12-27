using System.Collections.Generic;
using Base.Data;

namespace Base.Factory
{
    public interface IMoveRepository
    {
        IReadOnlyCollection<int> Ids { get; }
        MoveData GetMoveData(int id);
    }
}
