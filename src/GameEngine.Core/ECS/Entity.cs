﻿using System;
using System.Collections.Generic;

namespace GameEngine.Core.ECS
{
    public class Entity
    {
        private readonly List<IComponent> _components = new List<IComponent>();
        internal Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public void AddComponent(IComponent component)
        {
            _components.Add(component);
        }

        public void RemoveComponent(IComponent component)
        {
            _components.Remove(component);
        }

        public IEnumerable<IComponent> Components => _components;
    }
}