using System;
using System.Collections.Generic;

namespace GameEngine.Core.ECS
{
    public class EntityManager
    {
        public Entity CreateEntity()
        {
            throw new NotImplementedException();
        }

        public void AddEntity(Entity entity)
        {

        }

        public void RemoveEntity(Entity entity)
        {

        }

        public IEnumerable<T> GetComponentsOfType<T>()
        {
            throw new NotImplementedException();
        }

        public Entity GetEntityById(int id)
        {
            throw new NotImplementedException();
        }
    }
}