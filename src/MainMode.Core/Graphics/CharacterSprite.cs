using System;
using System.Collections.Generic;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphics
{
    public class CharacterSprite : AbstractCharacterSprite
    {
        private readonly Dictionary<Direction, ITexture2D> _directionDictionary = new Dictionary<Direction, ITexture2D>();

        public CharacterSprite(ITexture2D lookingLeft, ITexture2D lookingRight, ITexture2D lookingUp, ITexture2D lookingDown)
        {
            if (lookingLeft == null) throw new ArgumentNullException(nameof(lookingLeft));
            if (lookingRight == null) throw new ArgumentNullException(nameof(lookingRight));
            if (lookingUp == null) throw new ArgumentNullException(nameof(lookingUp));
            if (lookingDown == null) throw new ArgumentNullException(nameof(lookingDown));

            _directionDictionary[Direction.Down] = lookingDown;
            _directionDictionary[Direction.Left] = lookingLeft;
            _directionDictionary[Direction.Right] = lookingRight;
            _directionDictionary[Direction.Up] = lookingUp;

            Texture = lookingDown;
        }

        public override void TurnToDirection(Direction direction)
        {
            Texture = _directionDictionary[direction];
        }
    }
}