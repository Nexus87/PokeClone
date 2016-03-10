using Base;
using Base.Data;
using BattleLib.Components.BattleState;
using System.Collections.Generic;

namespace BattleLib
{
    public class Client
    {
        private readonly List<Pokemon> pokemons = new List<Pokemon>();

        public Client(ClientIdentifier id)
        {
            this.Id = id;

            Stats stats = new Stats { Atk = 10, Def = 10, HP = 30, SpAtk = 10, SpDef = 10, Speed = 10 };
            PokemonData data = new PokemonData() { Id = 0, Type1 = PokemonType.Normal, BaseStats = stats };

            MoveData moveData = new MoveData()
            {
                Name = "Move",
                Accuracy = 100,
                Damage = 120,
                DamageType = DamageCategory.Physical,
                PokemonType = PokemonType.Normal,
                PP = 20
            };

            for (int i = 0; i < 6; i++)
            {
                var pkmn = new Pokemon(data, stats) { Name = Id.Name + "_Pkmn" + i, Level = i + 20};
                for (int j = 0; j < 4; j++)
                    pkmn.SetMove(j, new Move(moveData));

                pokemons.Add(pkmn);
            }
        }

        public ClientIdentifier Id { get; private set; }
        public IReadOnlyList<Pokemon> Pokemons { get { return pokemons.AsReadOnly(); } }
    }
}