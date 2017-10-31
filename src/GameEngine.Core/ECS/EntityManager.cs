using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Core.ECS
{
    public class EntityManager
    {
        private readonly List<Entity> _entities = new List<Entity>();

        public Entity CreateEntity()
        {
            return new Entity(Guid.NewGuid());
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);
        }

        public IEnumerable<T> GetComponentsOfType<T>() where T : IComponent
        {
            return _entities
                .SelectMany(x => x.Components)
                .Where(x => x.GetType() == typeof(T))
                .Select(x => (T) x);
        }

        public Entity GetEntityById(Guid id)
        {
            return _entities.Single(x => x.Id == id);
        }

        public void AddEntities(IEnumerable<Entity> entities)
        {
            _entities.AddRange(entities);
        }
    }
}