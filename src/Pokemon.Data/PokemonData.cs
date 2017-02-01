using System.Runtime.Serialization;

namespace Base.Data
{
    public class PokemonData
    {
        public Stats BaseStats { get; set; }

        public int Id { get; set; }

        public MoveList MoveList { get; set; }

        public string Name { get; set; }

        public PokemonType Type1 { get; set; }

        public PokemonType Type2 { get; set; }
    }
}