using System;
using System.Collections.Generic;
using GameEngine.GUI;
using GameEngine.GUI.General;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphics
{
    public class CharacterSprite : AbstractGraphicComponent, ICharacterSprite
    {
        private IGraphicComponent _currentDirection;
        private readonly Dictionary<Direction, IGraphicComponent> _directionDictionary = new Dictionary<Direction, IGraphicComponent>();

        public CharacterSprite(IGraphicComponent lookingLeft, IGraphicComponent lookingRight, IGraphicComponent lookingUp, IGraphicComponent lookingDown)
        {
            if (lookingLeft == null) throw new ArgumentNullException(nameof(lookingLeft));
            if (lookingRight == null) throw new ArgumentNullException(nameof(lookingRight));
            if (lookingUp == null) throw new ArgumentNullException(nameof(lookingUp));
            if (lookingDown == null) throw new ArgumentNullException(nameof(lookingDown));

            _directionDictionary[Direction.Down] = lookingDown;
            _directionDictionary[Direction.Left] = lookingLeft;
            _directionDictionary[Direction.Right] = lookingRight;
            _directionDictionary[Direction.Up] = lookingUp;

            _currentDirection = lookingDown;
        }

        protected override void Update()
        {
            base.Update();
            foreach (var component in _directionDictionary.Values)
                component.SetCoordinates(this);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _currentDirection.Draw(time, batch);
        }

        public override void Setup()
        {
            foreach (var component in _directionDictionary.Values)
            {
                component.Setup();
            }
        }

        public void TurnToDirection(Direction direction)
        {
            _currentDirection = _directionDictionary[direction];
        }
    }
}