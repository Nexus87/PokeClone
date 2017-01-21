using Base.Data;

namespace PokemonRulesTest.TestData
{
    public class CharFactoryTestData
    {
        public static PokemonData[] Data = { d1, d2, d3 };

        private static PokemonData d1 = new PokemonData
            {
                BaseStats = new Stats
                    {
                        Atk = 10,
                        Def = 10,
                        Hp = 10,
                        SpAtk = 10,
                        SpDef = 10,
                        Speed = 10
                    },
                Id = 0,
                Name = "Name1",
                Type1 = PokemonType.Bug,
                Type2 = 0
            };

        private static PokemonData d2 = new PokemonData
        {
            BaseStats = new Stats
            {
                Atk = 1,
                Def = 1,
                Hp = 1,
                SpAtk = 1,
                SpDef = 1,
                Speed = 1
            },
            Id = 1,
            Name = "Name2",
            Type1 = PokemonType.Normal,
            Type2 = 0
        };

        private static PokemonData d3 = new PokemonData
        {
            BaseStats = new Stats
            {
                Atk = 2,
                Def = 2,
                Hp = 2,
                SpAtk = 2,
                SpDef = 2,
                Speed = 2
            },
            Id = 2,
            Name = "Name3",
            Type1 = PokemonType.Electric,
            Type2 = PokemonType.Normal
        };
    }
}