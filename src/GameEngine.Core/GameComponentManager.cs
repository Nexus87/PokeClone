using System.Linq;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Globals;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public class GameComponentManager : IGameComponentManager
    {
        private readonly GameComponentCollection _component;
        private readonly InputComponent _inputComponent;
        private readonly PokeEngine _game;
        private IInputHandler _inputHandler;

        public GameComponentManager(GameComponentCollection component, InputComponent inputComponent, PokeEngine game)
        {
            _component = component;
            _inputComponent = inputComponent;
            _game = game;
        }

        public IInputHandler InputHandler
        {
            set
            {
                _inputHandler = value;
                if (_inputComponent.Handler == null)
                    _inputComponent.Handler = _inputHandler;
            }
            get
            {
                return _inputHandler;
            }
        }

        public void RemoveGameComponent(IGameComponent component)
        {
            var res = _component.FirstOrDefault( c =>
            {
                var comp = c as GameComponentWrapper;
                if (comp == null)
                    return false;

                return comp.Component == component;
            });

            if (res == null)
                return;

            _component.Remove(res);
        }

        public void AddGameComponent(IGameComponent component)
        {
            _component.Add(new GameComponentWrapper(component, _game));
            component.Initialize();
        }

        public IGraphicComponent Graphic
        {
            get { return _game.Graphic; }
            set { _game.Graphic = value; }
        }
    }
}