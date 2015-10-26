using System;
using System.Runtime.Serialization;

namespace Base
{
    public enum DamageCategory {
        Physical,
        Special,
        Status
    }

    public enum Target
    {
        Enemy,
        Self
    }

    [DataContract]
    public class MoveData {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public PokemonType PkmType { get; set; }
        [DataMember]
        public int? Damage { get; set; }
        [DataMember]
        public int? Accuracy { get; set; }
        [DataMember]
        public int PP { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public Target TargetMode { get; set; }
        [DataMember]
        public DamageCategory DamageType { get; set; }
    }

    public class Move
    {
        public override string ToString()
        {
            return Data.Name;
        }

        public MoveData Data { get; private set; }
        public int RemainingPP { get; set; }
        public Move (MoveData data)
        {
            if (data == null) throw new ArgumentNullException("data", "Argument should not be null");

            Data = data;
            RemainingPP = data.PP;
        }
    }
}

