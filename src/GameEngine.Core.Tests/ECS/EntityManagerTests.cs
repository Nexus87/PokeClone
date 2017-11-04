using System.Linq;
using GameEngine.Core.ECS;
using GameEngine.Core.Tests.ECS.Utils.EntityManager;
using NUnit.Framework;

namespace GameEngine.Core.Tests.ECS
{
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
            SetupEntityManager(entityManager);

            var result = entityManager.GetComponentsOfType<FirstComponent>();
            Assert.AreEqual(4, result.Count());

        }

        private void SetupEntityManager(EntityManager entityManager)
        {
            entityManager.AddFirstComponent(_firstEntity);
            entityManager.AddFirstComponent(_secondEntity);
            entityManager.AddFirstComponent(_thirdEntity);
            entityManager.AddFirstComponent(_fourthEntity);
        }

        [Test]
        public void GetComponentByType_with_multiple_returns_List_of_components_with_inner_join_on_EntityId()
        {
            var entityManager = GetEntityManager();
            SetupEntityManager(entityManager);
            entityManager.AddSecondComponent(_firstEntity);
            entityManager.AddSecondComponent(_secondEntity);

            var result = entityManager.GetComponentsOfType<FirstComponent, SecondComponent>().ToList();
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetComponentsByTypeAndEntity_returns_a_List_of_components_for_type_and_EntityId()
        {
            var entityManager = GetEntityManager();
            SetupEntityManager(entityManager);

            var result = entityManager.GetComponentByTypeAndEntity<FirstComponent>(_firstEntity);
            Assert.AreEqual(1, result.Count());
        }
        private EntityManager GetEntityManager()
        {
            return new EntityManager();
        }
    }
}