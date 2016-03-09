using Base;
using BattleLib.Components.BattleState;
using System.Collections.Generic;

namespace BattleLib
{
    public class Client
    {
        private readonly List<Pokemon> pokemons = new List<Pokemon>();

        public Client(ClientIdentifier id)
        {
            this.ID = id;

            Stats stats = new Stats { Atk = 10, Def = 10, HP = 30, SpAtk = 10, SpDef = 10, Speed = 10 };
            PKData data = new PKData() { Id = 0, Type1 = PokemonType.Normal, BaseStats = stats };

            MoveData moveData = new MoveData()
            {
                Name = "Move",
                Accuracy = 100,
                Damage = 120,
                DamageType = DamageCategory.Physical,
                PkmType = PokemonType.Normal,
                PP = 20
            };

            for (int i = 0; i < 6; i++)
            {
                List<Move> moves = new List<Move>();
                for (int j = 0; j < 4; j++)
                    moves.Add(new Move(moveData));

                pokemons.Add(new Pokemon(data, stats) { Name = ID.Name + "_Pkmn" + i, Level = i + 20, Moves = moves });
            }
        }

        public ClientIdentifier ID { get; private set; }
        public IReadOnlyList<Pokemon> Pokemons { get { return pokemons.AsReadOnly(); } }
    }
}