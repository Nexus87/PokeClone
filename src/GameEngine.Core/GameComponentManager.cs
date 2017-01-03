using System.Linq;
using GameEngine.Entities;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public class GameComponentManager : IGameComponentManager
    {
        private readonly GameComponentCollection _component;
        private readonly GameRunner _game;

        public GameComponentManager(GameComponentCollection component, GameRunner game)
        {
            _component = component;
            _game = game;
        }

        public void RemoveGameComponent(IGameEntity entity)
        {
            var res = _component.FirstOrDefault( c =>
            {
                var comp = c as GameComponentWrapper;
                if (comp == null)
                    return false;

                return comp.Entity == entity;
            });

            if (res == null)
                return;

            _component.Remove(res);
        }

        public void AddGameComponent(IGameEntity entity)
        {
            using (var gameComponentWrapper = new GameComponentWrapper(entity, _game))
            {
                _component.Add(gameComponentWrapper);
            }
        }

        public IGraphicComponent Graphic
        {
            get { return _game.Graphic; }
            set { _game.Graphic = value; }
        }
    }
}