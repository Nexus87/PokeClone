using System.Collections.Generic;
using GameEngine.Core.ECS.Systems;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS
{
    public class SystemManager
    {
        private readonly List<ISystem> _systems = new List<ISystem>();
        
        internal SystemManager()
        {
        }

        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
        }

        public void RemoveSystem(ISystem system)
        {
            _systems.Remove(system);
        }

        public void Update(GameTime time, EntityManager entityManager)
        {

            _systems.ForEach(x => x.Update(time, entityManager));
        }
    }
}