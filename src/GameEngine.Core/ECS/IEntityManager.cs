using System;
using System.Collections.Generic;

namespace GameEngine.Core.ECS
{
    public interface IEntityManager
    {
        Entity CreateEntity();
        void AddEntity(Entity entity);
        void RemoveEntity(Entity entity);
        IEnumerable<T> GetComponentsOfType<T>() where T : IComponent;
        Entity GetEntityById(Guid id);
        void AddEntities(IEnumerable<Entity> entities);
    }
}