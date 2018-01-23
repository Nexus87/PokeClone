using BattleMode.Entities.Components;
using GameEngine.Core.ECS;

namespace BattleMode.Core.Entities
{
    public static class BattleStateEntity
    {
        public static Entity Create(IEntityManager entityManager)
        {
            var entity = new Entity();

            entityManager.AddComponent(new BattleStateComponent(entity.Id));

            return entity;
        }
    }
}