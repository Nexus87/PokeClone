using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Core.ECS
{
    public class EntityManager : IEntityManager
    {
        private readonly Dictionary<Type, List<Component>> _components = new Dictionary<Type, List<Component>>();

        public T GetFirstComponentOfType<T>() where T : Component
        {
            return GetComponentsOfType<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetComponentsOfType<T>() where T : Component
        {
            return TryGetComponents<T>()
                .OfType<T>();
        }

        public IEnumerable<(T1, T2)> GetComponentsOfType<T1, T2>() where T1 : Component where T2 : Component
        {
            return GetComponentsOfType<T1>()
                .Join(TryGetComponents<T2>(),
                    x => x.EntityId,
                    x => x.EntityId,
                    (c1, c2) => (c1, (T2) c2));
        }

        public IEnumerable<(T1, T2, T3)> GetComponentsOfType<T1, T2, T3>()
            where T1 : Component where T2 : Component where T3 : Component
        {
            return GetComponentsOfType<T1, T2>()
                .Join(TryGetComponents<T3>(),
                    x => x.Item1.EntityId,
                    x => x.EntityId,
                    (c1, c2) => (c1.Item1, c1.Item2, (T3) c2));
        }

        public IEnumerable<T> GetComponentByTypeAndEntity<T>(Entity entity) where T : Component
        {
            return TryGetComponents<T>()
                .Where(x => x.EntityId == entity.Id)
                .OfType<T>();
        }

        public IEnumerable<(T1, T2)> GetComponentByTypeAndEntity<T1, T2>(Entity entity)
            where T1 : Component where T2 : Component
        {
            return GetComponentByTypeAndEntity<T1>(entity)
                .Join(TryGetComponents<T2>(),
                    x1 => x1.EntityId,
                    x2 => x2.EntityId,
                    (c1, c2) => (c1, (T2) c2)
                );
        }

        public IEnumerable<(T1, T2, T3)> GetComponentByTypeAndEntity<T1, T2, T3>(Entity entity)
            where T1 : Component where T2 : Component where T3 : Component
        {
            return GetComponentByTypeAndEntity<T1, T2>(entity)
                .Join(TryGetComponents<T3>(),
                    x => x.Item1.EntityId,
                    x => x.EntityId,
                    (c1, c2) => (c1.Item1, c1.Item2, (T3) c2));
        }

        public void AddComponent<TComponent>(TComponent component) where TComponent : Component
        {
            if (!_components.TryGetValue(typeof(TComponent), out var components))
            {
                components = new List<Component>();
                _components[typeof(TComponent)] = components;
            }

            components.Add(component);
        }

        private IEnumerable<Component> TryGetComponents<T>() where T : Component
        {
            if (!_components.TryGetValue(typeof(T), out var components))
            {
                components = new List<Component>();
                _components[typeof(T)] = components;
            }
            return components;
        }

        public bool HasComponent<TComponent>(Entity entitiy)
        {
            if(_components.TryGetValue(typeof(TComponent), out var components)){
                return components.Any(x => x.EntityId == entitiy.Id);
            }

            return false;
        }
    }
}