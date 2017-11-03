using System;
using System.Linq;
using GameEngine.Core.ECS;
using NUnit.Framework;

namespace GameEngine.Core.Tests.ECS
{
    public class FirstComponent : Component
    {
        public FirstComponent(Guid entityId) : base(entityId)
        {
        }
    }

    public class SecondComponent : Component
    {
        public SecondComponent(Guid entityId) : base(entityId)
        {
        }
    }

    public static class TestExtensions
    {
        public static void AddFirstComponent(this EntityManager entityManager, Entity entity)
        {
            entityManager.AddComponent(new FirstComponent(entity.Id));
        }
        public static void AddSecondComponent(this EntityManager entityManager, Entity entity)
        {
            entityManager.AddComponent(new SecondComponent(entity.Id));
        }
    }
    public class EntityManagerTests
    {
        private readonly Entity _firstEntity = new Entity();
        private readonly Entity _secondEntity = new Entity();
        private readonly Entity _thirdEntity = new Entity();
        private readonly Entity _fourthEntity = new Entity();

        [Test]
        public void GetComponentByType_returns_a_list_of_components_with_the_given_type()
        {
            var entityManager = GetEntityManager();
            entityManager.AddFirstComponent(_firstEntity);
            entityManager.AddFirstComponent(_secondEntity);
            entityManager.AddFirstComponent(_thirdEntity);
            entityManager.AddFirstComponent(_fourthEntity);

            var result = entityManager.GetComponentsOfType<FirstComponent>();
            Assert.AreEqual(4, result.Count());

        }

        [Test]
        public void GetComponentByType_with_multiple_returns_List_of_components_with_inner_join_on_EntityId()
        {
            var entityManager = GetEntityManager();
            entityManager.AddFirstComponent(_firstEntity);
            entityManager.AddSecondComponent(_firstEntity);
            entityManager.AddFirstComponent(_secondEntity);
            entityManager.AddSecondComponent(_secondEntity);
            entityManager.AddFirstComponent(_thirdEntity);
            entityManager.AddFirstComponent(_fourthEntity);

            var result = entityManager.GetComponentsOfType<FirstComponent, SecondComponent>().ToList();
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetComponentsByTypeAndEntity_returns_a_List_of_components_for_type_and_EntityId()
        {
            var entityManager = GetEntityManager();
            entityManager.AddFirstComponent(_firstEntity);
            entityManager.AddFirstComponent(_secondEntity);
            entityManager.AddFirstComponent(_thirdEntity);
            entityManager.AddFirstComponent(_fourthEntity);

            var result = entityManager.GetComponentByTypeAndEntity<FirstComponent>(_firstEntity);
            Assert.AreEqual(1, result.Count());
        }
        private EntityManager GetEntityManager()
        {
            return new EntityManager();
        }
    }
}