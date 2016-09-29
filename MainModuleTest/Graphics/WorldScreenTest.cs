using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using MainModule;
using MainModule.Graphics;
using MainModuleTest.Utils;
using Moq;
using NUnit.Framework;

namespace MainModuleTest.Graphics
{
    [TestFixture]
    public class WorldScreenTest
    {
        [TestCase]
        public void Draw_NormalSetup_PlayerAndMapAreDrawn()
        {
            var player = new GraphicComponentMock();
            var mapMock = CreateMap();
            var screen = CreateWorldScreen(player, mapMock);

            screen.Setup();
            screen.Draw();

            Assert.True(player.WasDrawn);
            Assert.True(mapMock.WasDrawn);
        }

        [TestCase(Direction.Up, Direction.Down)]
        [TestCase(Direction.Left, Direction.Right)]
        [TestCase(Direction.Right, Direction.Left)]
        [TestCase(Direction.Down, Direction.Up)]
        public void PlayerMove_GivenDirection_MapIsMovedInExpectedDirection(Direction moveDirection, Direction expectedMapMoveDirection)
        {
            var player = new GraphicComponentMock();
            var mapMock = new Mock<IMapController>();
            var screen = CreateWorldScreen(player, mapMock.Object);

            screen.Setup();
            screen.PlayerMove(moveDirection);

            mapMock.Verify(m => m.MoveMap(expectedMapMoveDirection), Times.Once());
        }
        private static MapControllerMock CreateMap()
        {
            const int defaultWidht = 10;
            const int defaultHeight = 10;
            return CreateMap(defaultWidht, defaultHeight);
        }

        private static MapControllerMock CreateMap(int width, int height)
        {
            var mock = new MapControllerMock();
            return mock;
        }

        private static WorldScreen CreateWorldScreen(IGraphicComponent player, IMapController mapController, ScreenConstants screenConstants = null)
        {
            if(screenConstants == null)
                screenConstants = new ScreenConstants();
            var loaderMock = new Mock<ISpriteLoader>();
            return new WorldScreen(player, mapController, loaderMock.Object, screenConstants);
        }
    }
}