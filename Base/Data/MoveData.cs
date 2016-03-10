using System.Runtime.Serialization;

namespace Base.Data
{
    [DataContract]
    public class MoveData
    {
        [DataMember]
        public int? Accuracy { get; set; }

        [DataMember]
        public int? Damage { get; set; }

        [DataMember]
        public DamageCategory DamageType { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public PokemonType PokemonType { get; set; }

        [DataMember]
        public int PP { get; set; }

        [DataMember]
        public int Priority { get; set; }
    }
}