using System;
using System.Collections.Generic;
using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Shared.Components
{
    public class TrainerComponent : Component
    {
        public TrainerComponent(Guid entityId) : base(entityId)
        {
        }

        public string Name { get; set; }
        public List<Item> Items { get; set; }
        public List<Pokemon> Pokemons { get; set; }
    }
}