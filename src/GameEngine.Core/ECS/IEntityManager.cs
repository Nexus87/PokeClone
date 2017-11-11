using System;
using System.Collections.Generic;
using GameEngine.Core.ECS.Components;

namespace GameEngine.Core.ECS
{
    public interface IEntityManager
    {
        T GetFirstCompnentOfType<T>() where T : Component;
        IEnumerable<T> GetComponentsOfType<T>() where T : Component;
        IEnumerable<(T1, T2)> GetComponentsOfType<T1, T2>() where T1 : Component where T2 : Component;
        IEnumerable<(T1, T2, T3)> GetComponentsOfType<T1, T2, T3>() where T1 : Component where T2 : Component where T3 : Component;

        IEnumerable<T> GetComponentByTypeAndEntity<T>(Entity entity) where T : Component;
        IEnumerable<(T1, T2)> GetComponentByTypeAndEntity<T1, T2>(Entity entity) where T1 : Component where T2 : Component;
        IEnumerable<(T1, T2, T3)> GetComponentByTypeAndEntity<T1, T2, T3>(Entity entity) where T1 : Component where T2 : Component where T3 : Component;

        void AddComponent<TComponent>(TComponent component) where TComponent : Component;

    }
}