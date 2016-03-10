using System.Runtime.Serialization;

namespace Base.Data
{
    [DataContract]
    public class PokemonData
    {
        [DataMember]
        public Stats BaseStats { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public MoveList MoveList { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public PokemonType Type1 { get; set; }

        [DataMember]
        public PokemonType Type2 { get; set; }
    }
}