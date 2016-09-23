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
        private readonly IMapControler mapControler;
        private readonly ScreenConstants constants;


        public WorldScreen(TextureBox player, IMapControler mapControler, ScreenConstants constants)
            : this((IGraphicComponent) player, mapControler, constants)
        {
        }

        internal WorldScreen(IGraphicComponent player, IMapControler mapControler, ScreenConstants constants)
        {
            this.player = player;
            this.mapControler = mapControler;
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
            mapControler.Draw(time, batch);
        }

        public override void Setup()
        {
            player.Setup();
            mapControler.Setup();
            mapControler.CenterField(0, 0);
        }

        public void PlayerMove(Direction direction)
        {
            mapControler.MoveMap(ReverseDirection(direction));
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