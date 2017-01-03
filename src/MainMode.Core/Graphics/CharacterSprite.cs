using System;
using System.Collections.Generic;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphics
{
    public class CharacterSprite : AbstractGuiComponent, ICharacterSprite
    {
        private IGuiComponent _currentDirection;
        private readonly Dictionary<Direction, IGuiComponent> _directionDictionary = new Dictionary<Direction, IGuiComponent>();

        public CharacterSprite(IGuiComponent lookingLeft, IGuiComponent lookingRight, IGuiComponent lookingUp, IGuiComponent lookingDown)
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

        public void TurnToDirection(Direction direction)
        {
            _currentDirection = _directionDictionary[direction];
        }
    }
}