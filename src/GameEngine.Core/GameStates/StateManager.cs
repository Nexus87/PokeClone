using System.Collections.Generic;
using GameEngine.Globals;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.GameStates
{
    public class StateManager
    {
        private readonly Screen _screen;
        private readonly IReadOnlyDictionary<Keys, CommandKeys> _keyMap;
        private readonly Stack<State> _states = new Stack<State>();

        public StateManager(Screen screen, IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            _screen = screen;
            _keyMap = keyMap;
        }

        public void PushState(State state)
        {
            _states.Push(state);
            state.Init(_screen, _keyMap);
        }

        public void PopState()
        {
            _states.Pop();
        }

        public State CurrentState => _states.Peek();
    }
}
