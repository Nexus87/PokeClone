using System.Collections.Generic;

namespace GameEngine.Core.GameStates
{
    public class StateManager
    {
        private readonly Screen _screen;
        private readonly Stack<State> _states = new Stack<State>();

        public StateManager(Screen screen)
        {
            _screen = screen;
        }

        public void PushState(State state)
        {
            _states.Push(state);
            state.Init(_screen);
            CurrentState.ScreenState = new ScreenState();
        }

        public void PopState()
        {
            _states.Pop();
        }

        public State CurrentState => _states.Peek();
    }
}
