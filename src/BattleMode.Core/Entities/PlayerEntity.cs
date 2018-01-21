using System.Collections.Generic;
using System.Linq;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using PokemonShared.Data;
using PokemonShared.Models;

namespace BattleMode.Core.Entities
{
    public static class PlayerEntity
    {
        public static Entity Create(IEntityManager entityManager)
        {
            var stats = new Stats { Atk = 10, Def = 10, Hp = 30, SpAtk = 10, SpDef = 10, Speed = 10 };
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

            var pokemons = new List<Pokemon>();
            for (var i = 0; i < 6; i++)
            {
                var pkmn = new Pokemon(data, stats) { Name = "Player_Pkmn" + i, Level = i + 20 };
                for (var j = 0; j < 2; j++)
                    pkmn.SetMove(j, new Move(moveData));
                pkmn.Stats.Hp = 900;
                pkmn.Hp = 900;

                pokemons.Add(pkmn);
            }

            var items = new List<Item>
            {
                new Item {Name = "Item1"},
                new Item {Name = "Item2"},
                new Item {Name = "Item3"},
            };

            var entity = new Entity();
            entityManager.AddComponent(new TrainerComponent(entity.Id){Items=items, Name = "Player", Pokemons = pokemons});
            entityManager.AddComponent(new PokemonComponent(entity.Id){Pokemon = pokemons.First()});

            return entity;
        }
    }
}