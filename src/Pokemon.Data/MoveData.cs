namespace Pokemon.Data
{
    public class MoveData
    {
        public int? Accuracy { get; set; }

        public int? Damage { get; set; }

        public DamageCategory DamageType { get; set; }

        public string Name { get; set; }

        public PokemonType PokemonType { get; set; }

        public int PP { get; set; }

        public int Priority { get; set; }
    }
}