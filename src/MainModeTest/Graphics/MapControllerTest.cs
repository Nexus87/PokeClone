﻿using FakeItEasy;
using GameEngine.Core;
using GameEngineTest.TestUtils;
using MainMode.Core;
using MainMode.Core.Graphics;
using NUnit.Framework;
using Color = Microsoft.Xna.Framework.Color;

namespace MainModeTest.Graphics
{
    [TestFixture]
    public class MapControllerTest
    {
        private static readonly ScreenConstants DefaultScreenConstant = new ScreenConstants(100, 100, Color.Black);


        [TestCase(100, 100, 10, 10, 10.0f, 1, 3, 45.0f - 10.0f, 45.0f - 3 * 10.0f)]
        [TestCase(200,  50, 5, 20, 10.0f, 1, 3, 95.0f - 10.0f, 20.0f - 3 * 10.0f)]
        [TestCase(200, 50, 5, 20, 10.0f, -1, -1, 95.0f + 10.0f, 20.0f +  10.0f)]
        public void CenterField_AtGivenField_MapPositionAsExpected(
            float screenWidth, float screenHeight,
            int fieldWidth, int fieldHeight,
            float textureSize,
            int fieldX, int fieldY,
            float expectedX, float expectedY
        )
        {
            var screenConstants = new ScreenConstants(screenHeight, screenWidth, Color.Black);
            var mapMock = CreateMapMock(textureSize);
            var mapController = CreateMapController(mapMock, screenConstants);

            mapController.CenterField(fieldX, fieldY);
            mapController.Draw();

            Assert.AreEqual(mapMock.Area.X, (int) expectedX);
            Assert.AreEqual(mapMock.Area.Y, (int) expectedY);
        }

        [TestCase(100, 100, 10, 20, 10, 45.0f, 45.0f)]
        [TestCase(100, 50, 10, 20, 10, 45.0f, 20.0f)]
        public void CenterField_TopLeftField_MapPositionAsExpected(
            float screenWidth, float screenHeight,
            int fieldWidth, int fieldHeight,
            float textureSize,
            float expectedX, float expectedY
        )
        {
            var screenConstants = new ScreenConstants(screenHeight, screenWidth, Color.Black);
            var mapMock = CreateMapMock(textureSize);
            var mapController = CreateMapController(mapMock, screenConstants);

            mapController.CenterField(0, 0);
            mapController.Draw();

            Assert.AreEqual(mapMock.XPosition(), (int) expectedX);
            Assert.AreEqual(mapMock.YPosition(), (int) expectedY);
        }

        [TestCase(10, 32, 9, 8)]
        public void SelectedField_AfterSelection_IsAsExpected(int fieldWidth, int fieldHeight, int fieldX, int fieldY)
        {
            var mapMock = CreateMapMock();
            var map = CreateMapController(mapMock);

            map.CenterField(fieldX, fieldY);

            Assert.AreEqual(fieldX, map.CenteredFieldX);
            Assert.AreEqual(fieldY, map.CenteredFieldY);
        }

        [TestCase(10, 21, 2, 2, 2, 3, Direction.Up)]
        [TestCase(10, 21, 2, 2, 3, 2, Direction.Left)]
        [TestCase(10, 21, 2, 2, 1, 2, Direction.Right)]
        [TestCase(10, 21, 2, 2, 2, 1, Direction.Down)]
        [TestCase(1, 1, 0, 0, 0, -1, Direction.Down)]
        [TestCase(1, 1, 0, 0, 1, 0, Direction.Left)]
        [TestCase(1, 1, 0, 0, -1, 0, Direction.Right)]
        [TestCase(1, 1, 0, 0, 0, 1, Direction.Up)]
        public void MoveMap_StartingAtAGivenPoint_MapIsMoved(
            int fieldWidth, int fieldHeight,
            int startingFieldX, int startingFieldY,
            int expectedFieldX, int expedtedFieldY,
            Direction moveDirection)
        {
            var mapMock = CreateMapMock();
            var map = CreateMapController(mapMock);

            map.CenterField(startingFieldX, startingFieldY);
            map.MoveMap(moveDirection);

            Assert.AreEqual(expectedFieldX, map.CenteredFieldX);
            Assert.AreEqual(expedtedFieldY, map.CenteredFieldY);
        }

        private static IMapGraphic CreateMapMock(float textureSize = 32.0f)
        {
            var mock = A.Fake<IMapGraphic>();
            A.CallTo(() => mock.TextureSize).Returns(textureSize);
            A.CallTo(() => mock.GetXPositionOfColumn(A<int>.Ignored)).ReturnsLazily( (int column) => column * textureSize);
            A.CallTo(() => mock.GetYPositionOfRow(A<int>.Ignored)).ReturnsLazily((int row) => row * textureSize);
            return mock;
        }

        private static FieldMapController CreateMapController(IMapGraphic mapGraphic, ScreenConstants screenConstants = null)
        {
            if(screenConstants == null)
                screenConstants = DefaultScreenConstant;
            var mapLoaderFake = A.Fake<IMapLoader>();
            A.CallTo(() => mapLoaderFake.LoadMap(A<Map>.Ignored)).Returns(mapGraphic);
            var mapController = new FieldMapController(mapLoaderFake, screenConstants);
            mapController.Setup();
            mapController.LoadMap(null);
            return mapController;
        }

    }
}