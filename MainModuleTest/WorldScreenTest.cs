using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using MainModule;
using Moq;
using NUnit.Framework;

namespace MainModuleTest
{
    [TestFixture]
    public class WorldScreenTest
    {
        [TestCase]
        public void Draw_NormalSetup_PlayerAndMapAreDrawn()
        {
            var player = new GraphicComponentMock();
            var map = CreateMap();
            var screen = CreateWorldScreen(player, map);

            screen.Setup();
            screen.Draw();

            Assert.True(player.WasDrawn);
        }

        private static IMap CreateMap()
        {
            const int defaultWidht = 10;
            const int defaultHeight = 10;
            return CreateMap(defaultWidht, defaultHeight);
        }

        [TestCase(20, 30, 20, 30)]
        public void GetFieldSize_NormalSetup_SizeAsExpected(int width, int height, int expectedWidth, int expectedHeight)
        {
            var expected = new FieldSize(expectedWidth, expectedHeight);
            var player = new GraphicComponentMock();
            var map = CreateMap(width, height);
            var screen = CreateWorldScreen(player, map);

            var realSize = screen.FieldSize;
            Assert.AreEqual(expected, realSize);
        }

        private static IMap CreateMap(int width, int height)
        {
            var mock = new Mock<IMap>();
            mock.SetupGet(o => o.FieldSize).Returns(new FieldSize(width, height));
            return mock.Object;
        }

        private static WorldScreen CreateWorldScreen(IGraphicComponent player, IMap map)
        {
            return new WorldScreen(player, map, new ScreenConstants());
        }
    }
}