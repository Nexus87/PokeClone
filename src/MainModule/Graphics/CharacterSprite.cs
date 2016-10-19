using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace MainModule.Graphics
{
    public class CharacterSprite : AbstractGraphicComponent, ICharacterSprite
    {
        private IGraphicComponent currentDirection;
        private readonly Dictionary<Direction, IGraphicComponent> directionDictionary = new Dictionary<Direction, IGraphicComponent>();

        public CharacterSprite(IGraphicComponent lookingLeft, IGraphicComponent lookingRight, IGraphicComponent lookingUp, IGraphicComponent lookingDown)
        {
            if (lookingLeft == null) throw new ArgumentNullException("lookingLeft");
            if (lookingRight == null) throw new ArgumentNullException("lookingRight");
            if (lookingUp == null) throw new ArgumentNullException("lookingUp");
            if (lookingDown == null) throw new ArgumentNullException("lookingDown");

            directionDictionary[Direction.Down] = lookingDown;
            directionDictionary[Direction.Left] = lookingLeft;
            directionDictionary[Direction.Right] = lookingRight;
            directionDictionary[Direction.Up] = lookingUp;

            currentDirection = lookingDown;
        }

        protected override void Update()
        {
            base.Update();
            foreach (var component in directionDictionary.Values)
                component.SetCoordinates(this);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            currentDirection.Draw(time, batch);
        }

        public override void Setup()
        {
            foreach (var component in directionDictionary.Values)
            {
                component.Setup();
            }
        }

        public void TurnToDirection(Direction direction)
        {
            currentDirection = directionDictionary[direction];
        }
    }
}