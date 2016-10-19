using Base.Data;
using System.Collections.Generic;
namespace Base
{
    public interface IMoveRepository
    {
        IReadOnlyCollection<int> Ids { get; }
        MoveData GetMoveData(int id);
    }
}
