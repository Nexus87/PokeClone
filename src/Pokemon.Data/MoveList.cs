using System;
using System.Runtime.Serialization;

namespace Base.Data
{
    public class MoveList
    {
        public Tuple<int, int>[] Moves { get; set; }
    }
}