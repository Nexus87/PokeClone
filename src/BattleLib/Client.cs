using Base;
using Base.Data;
using System;
using System.Collections.Generic;

namespace BattleLib
{
    public class Client
    {
        private readonly List<Pokemon> pokemons = new List<Pokemon>();
        private readonly List<Item> items = new List<Item>();

        public Client(ClientIdentifier id, List<Pokemon> pokemons)
        {
            Id = id;
            this.pokemons.AddRange(pokemons);
        }

        public Client(ClientIdentifier id)
        {
            Id = id;

            var stats = new Stats { Atk = 10, Def = 10, HP = 30, SpAtk = 10, SpDef = 10, Speed = 10 };
            var data = new PokemonData { Id = 0, Type1 = PokemonType.Normal, BaseStats = stats };

            var moveData = new MoveData
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
                for (int j = 0; j < 2; j++)
                    pkmn.SetMove(j, new Move(moveData));
                pkmn.Stats.HP = 30;
                pkmn.HP = 30;

                pokemons.Add(pkmn);
            }
        }

        public void UseItem(int index)
        {
            if (index < 0 || index >= items.Count)
                throw new ArgumentException("Index out of bound");

            var item = items[index];
            item.StackSize--;
            if (item.StackSize <= 0)
                items.RemoveAt(index);

            ItemUsed(this, new ItemUsedEventArgs(index, item));
        }
        public event EventHandler<ItemUsedEventArgs> ItemUsed = delegate { };
        public IReadOnlyList<Item> Items { get { return items.AsReadOnly(); } }
        public ClientIdentifier Id { get; private set; }
        public IReadOnlyList<Pokemon> Pokemons { get { return pokemons.AsReadOnly(); } }
    }
}