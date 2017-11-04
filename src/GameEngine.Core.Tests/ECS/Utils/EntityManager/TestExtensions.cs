using GameEngine.Core.ECS;

namespace GameEngine.Core.Tests.ECS.Utils.EntityManager
{
    public static class TestExtensions
    {
        public static void AddFirstComponent(this Core.ECS.EntityManager entityManager, Entity entity)
        {
            entityManager.AddComponent(new FirstComponent(entity.Id));
        }
        public static void AddSecondComponent(this Core.ECS.EntityManager entityManager, Entity entity)
        {
            entityManager.AddComponent(new SecondComponent(entity.Id));
        }
    }
}