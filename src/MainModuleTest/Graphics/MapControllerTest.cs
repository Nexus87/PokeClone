using GameEngine;
using GameEngine.Utils;
using GameEngineTest.TestUtils;
using MainModule;
using MainModule.Graphics;
using Moq;
using NUnit.Framework;
using Color = Microsoft.Xna.Framework.Color;

namespace MainModuleTest.Graphics
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
            var mapMock = CreateMapMock(fieldWidth, fieldHeight, textureSize);
            var mapController = CreateMapController(mapMock.Object, screenConstants);

            mapController.CenterField(fieldX, fieldY);
            mapController.Draw();

            mapMock.VerifySet(m => m.XPosition = expectedX);
            mapMock.VerifySet(m => m.YPosition = expectedY);
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
            var mapMock = CreateMapMock(fieldWidth, fieldHeight, textureSize);
            var mapController = CreateMapController(mapMock.Object, screenConstants);

            mapController.CenterField(0, 0);
            mapController.Draw();

            mapMock.VerifySet(m => m.XPosition = expectedX);
            mapMock.VerifySet(m => m.YPosition = expectedY);
        }

        [TestCase(10, 32, 9, 8)]
        public void SelectedField_AfterSelection_IsAsExpected(int fieldWidth, int fieldHeight, int fieldX, int fieldY)
        {
            var mapMock = CreateMapMock(fieldWidth, fieldHeight);
            var map = CreateMapController(mapMock.Object);

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
            var mapMock = CreateMapMock(fieldWidth, fieldHeight);
            var map = CreateMapController(mapMock.Object);

            map.CenterField(startingFieldX, startingFieldY);
            map.MoveMap(moveDirection);

            Assert.AreEqual(expectedFieldX, map.CenteredFieldX);
            Assert.AreEqual(expedtedFieldY, map.CenteredFieldY);
        }

        private static Mock<IMapGraphic> CreateMapMock(int fieldWidth, int fieldHeight, float textureSize = 32.0f)
        {
            var mock = new Mock<IMapGraphic>();
            mock.Setup(m => m.TextureSize).Returns(textureSize);
            mock.Setup(m => m.GetXPositionOfColumn(It.IsAny<int>())).Returns<float>(column => column * textureSize);
            mock.Setup(m => m.GetYPositionOfRow(It.IsAny<int>())).Returns<float>(row => row * textureSize);
            return mock;
        }

        private static Table<GraphicComponentMock> CreateTable(int fieldWidth, int fieldHeight)
        {
            var table = new Table<GraphicComponentMock>();
            for(var i = 0; i < fieldWidth; i++)
                for(var j = 0; j < fieldHeight; j++)
                    table[j, i] = new GraphicComponentMock();

            return table;
        }

        private static FieldMapController CreateMapController(IMapGraphic mapGraphic, ScreenConstants screenConstants = null)
        {
            if(screenConstants == null)
                screenConstants = DefaultScreenConstant;
            var mapLoaderFake = new Mock<IMapLoader>();
            mapLoaderFake.Setup(l => l.LoadMap(It.IsAny<Map>())).Returns(mapGraphic);
            var mapController = new FieldMapController(mapLoaderFake.Object, screenConstants);
            mapController.Setup();
            mapController.LoadMap(null);
            return mapController;
        }

    }
}