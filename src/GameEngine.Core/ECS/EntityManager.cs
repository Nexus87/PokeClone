using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Core.ECS
{
    public class EntityManager : IEntityManager
    {
        private Dictionary<Type, List<Component>> _components = new Dictionary<Type, List<Component>>();

        public IEnumerable<T> GetComponentsOfType<T>() where T : Component
        {
            return TryGetComponents<T>()
                .OfType<T>();
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

        public IEnumerable<(T1, T2)> GetComponentsOfType<T1, T2>() where T1 : Component where T2 : Component
        {
            var t1 = TryGetComponents<T1>();
            var t2 = TryGetComponents<T2>();

            return t1.Join(t2, 
                x => x.EntityId, 
                x => x.EntityId,
                (c1, c2) => ((T1) c1, (T2) c2));

        }

        public IEnumerable<(T1, T2, T3)> GetComponentsOfType<T1, T2, T3>() where T1 : Component where T2 : Component where T3 : Component
        {
            var t1 = TryGetComponents<T1>();
            var t2 = TryGetComponents<T2>();
            var t3 = TryGetComponents<T3>();

            return t1.Join(t2,
                    x => x.EntityId,
                    x => x.EntityId,
                    (c1, c2) => ((T1) c1, (T2) c2))
                .Join(t3,
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

        public IEnumerable<(T1, T2)> GetComponentByTypeAndEntity<T1, T2>(Entity entity) where T1 : Component where T2 : Component
        {
            var t1 = TryGetComponents<T1>().Where(x => x.EntityId == entity.Id);
            var t2 = TryGetComponents<T2>().Where(x => x.EntityId == entity.Id);

            return t1.Join(t2,
                x => x.EntityId,
                x => x.EntityId,
                (c1, c2) => ((T1)c1, (T2)c2));
        }

        public IEnumerable<(T1, T2, T3)> GetComponentByTypeAndEntity<T1, T2, T3>(Entity entity) where T1 : Component where T2 : Component where T3 : Component
        {
            var t1 = TryGetComponents<T1>().Where(x => x.EntityId == entity.Id);
            var t2 = TryGetComponents<T2>().Where(x => x.EntityId == entity.Id);
            var t3 = TryGetComponents<T3>().Where(x => x.EntityId == entity.Id);

            return t1.Join(t2,
                    x => x.EntityId,
                    x => x.EntityId,
                    (c1, c2) => ((T1)c1, (T2)c2))
                .Join(t3,
                    x => x.Item1.EntityId,
                    x => x.EntityId,
                    (c1, c2) => (c1.Item1, c1.Item2, (T3)c2));
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
    }
}