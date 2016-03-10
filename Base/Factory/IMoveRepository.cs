using Base.Data;
using System.Collections.Generic;
namespace Base
{
    public interface IMoveRepository
    {
        List<int> GetIds();
        MoveData GetMoveData(int id);
    }
}
