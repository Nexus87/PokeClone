using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public interface ICharRepository
    {
        IEnumerable<int> Ids { get; }
        PKData getPKData(int id);
    }
}
