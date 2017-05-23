using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.GameStates
{
    public class StateManager
    {
        private readonly IGameTypeRegistry _registry;
        private readonly Stack<State> _states = new Stack<State>();

        public StateManager(IGameTypeRegistry registry)
        {
            _registry = registry;
        }

        public void PushState<T>() where T: State
        {
            _states.Push(_registry.ResolveType<T>());
        }

        public void PopState()
        {
            _states.Pop();
        }

        public State CurrentState => _states.Peek();
    }
}
