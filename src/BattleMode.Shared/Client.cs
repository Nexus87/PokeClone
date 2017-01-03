using System;
using System.Collections.Generic;
using Base;
using Base.Data;

namespace BattleMode.Shared
{
    public class Client
    {
        private readonly List<Pokemon> _pokemons = new List<Pokemon>();
        private readonly List<Item> _items = new List<Item>();

        public Client(ClientIdentifier id, List<Pokemon> pokemons)
        {
            Id = id;
            _pokemons.AddRange(pokemons);
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

            for (var i = 0; i < 6; i++)
            {
                var pkmn = new Pokemon(data, stats) { Name = Id.Name + "_Pkmn" + i, Level = i + 20};
                for (var j = 0; j < 2; j++)
                    pkmn.SetMove(j, new Move(moveData));
                pkmn.Stats.HP = 900;
                pkmn.HP = 900;

                _pokemons.Add(pkmn);
            }
        }

        public void UseItem(int index)
        {
            if (index < 0 || index >= _items.Count)
                throw new ArgumentException("Index out of bound");

            var item = _items[index];
            item.StackSize--;
            if (item.StackSize <= 0)
                _items.RemoveAt(index);

            ItemUsed(this, new ItemUsedEventArgs(index, item));
        }
        public event EventHandler<ItemUsedEventArgs> ItemUsed = delegate { };
        public IReadOnlyList<Item> Items => _items.AsReadOnly();
        public ClientIdentifier Id { get; }
        public IReadOnlyList<Pokemon> Pokemons => _pokemons.AsReadOnly();
    }
}