using System;
using System.Runtime.Serialization;

namespace Base
{
    [DataContract]
    public class MoveList
    {
        [DataMember]
        public Tuple<int, string>[] Moves;
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
		public string name;
		[DataMember]
		public int id;
		[DataMember]
		public Stats baseStats;
		[DataMember]
		public PokemonType type1;
		[DataMember]
		public PokemonType type2;
        [DataMember]
        public MoveList moveList;
	}
}

