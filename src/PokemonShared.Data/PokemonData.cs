namespace PokemonShared.Data
{
    public class PokemonData
    {
        public Stats BaseStats { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public PokemonType Type1 { get; set; }

        public PokemonType Type2 { get; set; }
    }
}