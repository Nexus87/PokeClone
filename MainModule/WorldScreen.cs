using System;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace MainModule
{
    [GameType]
    public class WorldScreen : AbstractGraphicComponent
    {
        private readonly IGraphicComponent player;
        private readonly IMap map;
        private readonly ScreenConstants constants;


        public WorldScreen(TextureBox player, IMap map, ScreenConstants constants)
            : this((IGraphicComponent) player, map, constants)
        {
        }

        internal WorldScreen(IGraphicComponent player, IMap map, ScreenConstants constants)
        {
            this.player = player;
            this.map = map;
            this.constants = constants;
        }

        protected override void Update()
        {
            base.Update();

            var playerX = constants.ScreenWidth / 2.0f;
            var playerY = constants.ScreenHeight / 2.0f;

            player.XPosition = playerX;
            player.YPosition = playerY;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            player.Draw(time, batch);
            map.Draw(time, batch);
        }

        public override void Setup()
        {
            player.Setup();
            map.Setup();
            map.CenterField(0, 0);
        }

        public void PlayerMove(Direction direction)
        {
            map.MoveMap(ReverseDirection(direction));
        }

        private static Direction ReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }
    }
}