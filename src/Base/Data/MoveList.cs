using System;
using System.Runtime.Serialization;

namespace Base.Data
{
    [DataContract]
    public class MoveList
    {
        [DataMember]
        public Tuple<int, int>[] Moves { get; set; }
    }
}