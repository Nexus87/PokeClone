using System.Runtime.Serialization;

namespace Base.Data
{
    [DataContract]
    public class Stats
    {
        [DataMember]
        public int Atk { get; set; }

        [DataMember]
        public int Def { get; set; }

        [DataMember]
        public int Hp { get; set; }

        [DataMember]
        public int SpAtk { get; set; }

        [DataMember]
        public int SpDef { get; set; }

        [DataMember]
        public int Speed { get; set; }
    }
}