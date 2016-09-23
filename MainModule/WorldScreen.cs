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
        private readonly IMapController mapController;
        private readonly ScreenConstants constants;


        public WorldScreen(TextureBox player, IMapController mapController, ScreenConstants constants)
            : this((IGraphicComponent) player, mapController, constants)
        {
        }

        internal WorldScreen(IGraphicComponent player, IMapController mapController, ScreenConstants constants)
        {
            this.player = player;
            this.mapController = mapController;
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
            mapController.Draw(time, batch);
        }

        public override void Setup()
        {
            player.Setup();
            mapController.Setup();
            mapController.CenterField(0, 0);
        }

        public void PlayerMove(Direction direction)
        {
            mapController.MoveMap(ReverseDirection(direction));
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