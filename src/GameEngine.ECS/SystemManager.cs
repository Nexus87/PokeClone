using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine.ECS
{
    public class SystemManager
    {
        private readonly List<ISystem> _systems = new List<ISystem>();

        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
        }

        public void RemoveSystem(ISystem system)
        {
            _systems.Remove(system);
        }

        public void Update(GameTime time)
        {
            _systems.ForEach(x => x.Update(time));
        }
    }
}