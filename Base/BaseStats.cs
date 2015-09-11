using System;
using System.Runtime.Serialization;

namespace Base
{
    [DataContract]
    public class MoveList
    {
        [DataMember]
        public Tuple<int, string>[] Moves { get; set; }
    }

	[DataContract]
	public class Stats {
		[DataMember]
		public int HP { get; set; }
		[DataMember]
		public int Atk { get; set; }
		[DataMember]
		public int Def { get; set; }
		[DataMember]
		public int SpAtk { get; set; }
		[DataMember]
		public int SpDef { get; set; }
		[DataMember]
		public int Speed { get; set; }
	}

	[DataContract]
	public class PKData
	{
        [DataMember]
        public string Name { get; set; }
		[DataMember]
        public int Id { get; set; }
		[DataMember]
        public Stats BaseStats { get; set; }
		[DataMember]
        public PokemonType Type1 { get; set; }
		[DataMember]
        public PokemonType Type2 { get; set; }
        [DataMember]
        public MoveList MoveList { get; set; }
	}
}

