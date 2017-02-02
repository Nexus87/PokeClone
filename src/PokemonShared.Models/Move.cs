using System;
using PokemonShared.Data;

namespace PokemonShared.Models
{
    public class Move
    {
        private readonly MoveData _data;
        
        public Move(MoveData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            _data = data;

            RemainingPp = data.PP;
        }
        
        public int? Accuracy => _data.Accuracy;
        public int? Damage => _data.Damage;
        public DamageCategory DamageType => _data.DamageType;
        public string Name => _data.Name;
        public PokemonType PokemonType => _data.PokemonType;
        public int Pp => _data.PP;
        public int Priority => _data.Priority;
        public int RemainingPp { get; set; }
        
        public override string ToString()
        {
            return _data.Name;
        }
    }
}