using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace GameEngine.Core.GameStates
{
    public class StateManager
    {
        private readonly IContainer _container;
        private readonly Stack<State> _states = new Stack<State>();

        public StateManager(IContainer container)
        {
            _container = container;
        }

        public void PushState<T>() where T: State
        {
            _states.Push(_container.Resolve<T>());
        }

        public void PopState()
        {
            _states.Pop();
        }

        public State CurrentState => _states.Peek();
    }
}
