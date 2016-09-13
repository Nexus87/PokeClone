using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using MainModule;
using MainModuleTest.Utils;
using Microsoft.Xna.Framework;
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
            var mapMock = CreateMap();
            var screen = CreateWorldScreen(player, mapMock);

            screen.Setup();
            screen.Draw();

            Assert.True(player.WasDrawn);
            Assert.True(mapMock.WasDrawn);
        }

        private static MapMock CreateMap()
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

        private static MapMock CreateMap(int width, int height)
        {
            var mock = new MapMock(new FieldSize(width, height));
            return mock;
        }

        private static WorldScreen CreateWorldScreen(IGraphicComponent player, IMap map, ScreenConstants screenConstants = null)
        {
            if(screenConstants == null)
                screenConstants = new ScreenConstants();
            return new WorldScreen(player, map, screenConstants);
        }
    }
}