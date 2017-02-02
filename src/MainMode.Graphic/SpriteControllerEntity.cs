using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Graphics;
using GameEngine.Graphics.Textures;
using MainMode.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainMode.Graphic
{
    public abstract class SpriteControllerEntity
    {
        public Sprite Sprite { get; }

        private readonly Dictionary<Direction, Tuple<ITexture2D, SpriteEffects>> _standingDictionary;
        private readonly Dictionary<Direction, Tuple<ITexture2D, SpriteEffects>> _movingDictionary;

        private bool IsMoving => _path.Count != 0;
        private Direction _direction;
        private Action _callback;
        private readonly Queue<Tuple<Point, Action>> _path = new Queue<Tuple<Point, Action>>();
        private double _timer;

        protected SpriteControllerEntity(SpriteEntityTextures spriteEntityTextures)
        {
            Sprite = new Sprite
            {
                Position = new Rectangle(Point.Zero, Constants.Size),
                SpriteEffect = spriteEntityTextures.StandingTextures[Direction.Down].Item2,
                Texture = spriteEntityTextures.StandingTextures[Direction.Down].Item1
            };
            _standingDictionary = spriteEntityTextures.StandingTextures;
            _movingDictionary = spriteEntityTextures.MovingTextures;
        }

        protected abstract void MoveSprite(Point position);

        public void Update(GameTime time)
        {
            if (!IsMoving)
                return;

            _timer += time.ElapsedGameTime.Milliseconds;
            if(_timer < 25)
                return;

            var next = _path.Dequeue();

            MoveSprite(next.Item1);
            next.Item2();

            _timer = 0;

            if(_path.Count > 0)
                return;

            SetSpriteTexture(_standingDictionary[_direction]);
            _callback?.Invoke();
        }

        public void TurnToDirection(Direction direction)
        {
            if (IsMoving)
                return;

            _direction = direction;
            SetSpriteTexture(_standingDictionary[direction]);
        }

        public void MoveToDirection(Point target, Action callback)
        {
            if (IsMoving)
                return;

            _callback = callback;
            var path = target - Sprite.Position.Location;

            if (path.X != 0)
            {
                _direction = path.X < 0 ? Direction.Left : Direction.Right;
            }
            else if (path.Y != 0)
            {
                _direction = path.Y < 0 ? Direction.Up : Direction.Down;
            }

            SetSpriteTexture(_movingDictionary[_direction]);

            _path.Clear();

            const int steps = 8;
            var locations = Enumerable
                .Range(1, steps)
                .Select(x => Tuple.Create<Point, Action>(
                    new Point(x * path.X / steps, x *path.Y / steps) + Sprite.Position.Location,
                    () => SetTexture(x)
                ));

            foreach (var location in locations)
            {
                _path.Enqueue(location);
            }

            _timer = 0;
        }

        private void SetTexture(int i)
        {
            SetSpriteTexture(i % 4 == 0 ? _standingDictionary[_direction] : _movingDictionary[_direction]);
        }
        private void SetSpriteTexture(Tuple<ITexture2D, SpriteEffects> texture)
        {
            Sprite.Texture = texture.Item1;
            Sprite.SpriteEffect = texture.Item2;
        }

        public void PlaceAt(Point location)
        {
            MoveSprite(location);
        }
    }
}