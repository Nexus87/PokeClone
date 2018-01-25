using System;
using GameEngine.Core.ECS;
using PokemonShared.Data;
using PokemonShared.Models;

namespace BattleMode.Shared.Components
{
    public class PokemonComponent : Component
    {
        public PokemonComponent(Guid entityId) : base(entityId)
        {
        }

        public Pokemon Pokemon { get; set; }
        public Stats Modifier { get; set; }

        public int ChangeHp { get; set; }
    }
}
