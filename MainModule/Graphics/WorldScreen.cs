using System;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace MainModule.Graphics
{
    [GameService(typeof(IWorldScreenController))]
    public class WorldScreen : AbstractGraphicComponent, IWorldScreenController
    {
        private ICharacterSprite player;
        private readonly IMapController mapController;
        private readonly ISpriteLoader spriteLoader;
        private readonly ScreenConstants constants;

        public WorldScreen(IMapController mapController, ISpriteLoader spriteLoader, ScreenConstants constants)
        {
            this.mapController = mapController;
            this.spriteLoader = spriteLoader;
            this.constants = constants;
        }

        protected override void Update()
        {
            base.Update();

            var playerX = constants.ScreenWidth / 2.0f - 64;
            var playerY = constants.ScreenHeight / 2.0f - 64;

            player.XPosition = playerX;
            player.YPosition = playerY;

            //TODO remove hardcoded values
            player.Width = 128;
            player.Height = 128;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            mapController.Draw(time, batch);
            player.Draw(time, batch);
        }

        public override void Setup()
        {
            spriteLoader.Setup();
            player = spriteLoader.GetSprite("player");
            mapController.Setup();
            mapController.CenterField(0, 0);
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

        public void PlayerTurnDirection(Direction direction)
        {
            player.TurnToDirection(direction);
        }

        public void PlayerMoveDirection(Direction direction)
        {
            mapController.MoveMap(ReverseDirection(direction));
        }
    }
}