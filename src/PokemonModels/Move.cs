using System;
using Pokemon.Data;

namespace Pokemon.Models
{
    public class Move
    {
        private readonly MoveData data;
        
        public Move(MoveData data)
        {
            this.data = data ?? throw new ArgumentNullException("data", "Argument should not be null");

            RemainingPP = data.PP;
        }
        
        public int? Accuracy { get { return data.Accuracy; } }
        public int? Damage { get { return data.Damage; } }
        public DamageCategory DamageType { get { return data.DamageType; } }
        public string Name { get { return data.Name; } }
        public PokemonType PokemonType { get { return data.PokemonType; } }
        public int PP { get { return data.PP; } }
        public int Priority { get { return data.Priority; } }
        public int RemainingPP { get; set; }
        
        public override string ToString()
        {
            return data.Name;
        }
    }
}