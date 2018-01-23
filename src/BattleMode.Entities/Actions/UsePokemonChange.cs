using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Entities.Actions
{
    public class UsePokemonChange
    {
        public readonly Pokemon Pokemon;
        public readonly Entity Target;

        public UsePokemonChange(Pokemon pokemon, Entity target)
        {
            Pokemon = pokemon;
            Target = target;
        }
    }
}