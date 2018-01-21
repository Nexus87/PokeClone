using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Shared.Actions
{
    public class SetPokemonAction
    {
        public readonly Entity Entity;
        public readonly Pokemon Pokemon;

        public SetPokemonAction(Entity entity, Pokemon pokemon)
        {
            Entity = entity;
            Pokemon = pokemon;
        }
    }
}