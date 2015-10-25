using System.Collections.Generic;

namespace Base
{
    public interface ICharRepository
    {
        IEnumerable<int> Ids { get; }
        PKData getPKData(int id);
    }
}
