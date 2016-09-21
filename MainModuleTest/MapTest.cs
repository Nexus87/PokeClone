using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using GameEngine;
using GameEngine.Utils;
using GameEngineTest.TestUtils;
using MainModule;
using Moq;
using NUnit.Framework;
using Color = Microsoft.Xna.Framework.Color;

namespace MainModuleTest
{
    [TestFixture]
    public class MapTest
    {
        private static readonly ScreenConstants DefaultScreenConstant = new ScreenConstants(100, 100, Color.Black);

        [TestCase(10, 20, 32.0f, 320.0f, 640.0f)]
        public void MapSize_CreateMapWithGivenArgument_SizeIsAsExpected(
            int fieldWidth, int fieldHeight,
            float textureSize,
            float expectedWidth, float expectedHeight
        )
        {
            var map = CreateMap(fieldWidth, fieldHeight, textureSize);
            Assert.AreEqual(expectedWidth, map.TotalWidth);
            Assert.AreEqual(expectedHeight, map.TotalHeight);
        }

        [TestCase(100, 100, 10, 10, 10.0f, 1, 3, 45.0f, 45.0f)]
        [TestCase(200,  50, 5, 20, 10.0f, 1, 3, 95.0f, 20.0f)]
        public void CenterField_AtGivenField_FieldIsAtTheExpectedPosition(
            float screenWidth, float screenHeight,
            int fieldWidth, int fieldHeight,
            float textureSize,
            int fieldX, int fieldY,
            float expectedX, float expectedY
        )
        {
            var screenConstants = new ScreenConstants(screenHeight, screenWidth, Color.Black);
            var componentTable = CreateTable(fieldWidth, fieldHeight);
            var map = CreateMap(componentTable, textureSize, screenConstants);

            map.CenterField(fieldX, fieldY);
            map.Draw();

            Assert.AreEqual(expectedX, componentTable[fieldY, fieldX].XPosition);
            Assert.AreEqual(expectedY, componentTable[fieldY, fieldX].YPosition);
        }

        public static Table<Tuple<float, float>> TableFactory1()
        {
            var table = new Table<Tuple<float, float>>();
            table[0, 0] = new Tuple<float, float>(45.0f, 45.0f);
            for (var i = 0; i < 20; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    table[i, j] = new Tuple<float, float>(45.0f + 10*j, 45.0f + 10*i);
                }
            }

            return table;
        }

        public static Table<Tuple<float, float>> TableFactory2()
        {
            var table = new Table<Tuple<float, float>>();
            table[0, 0] = new Tuple<float, float>(45.0f, 20.0f);
            for (var i = 0; i < 20; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    table[i, j] = new Tuple<float, float>(45.0f + 10 * j, 20.0f + 10 * i);
                }
            }

            return table;
        }


        public static List<TestCaseData> CenterFieldData = new List<TestCaseData>
        {
            new TestCaseData(100, 100, 10, 20, 10.0f, TableFactory1()),
            new TestCaseData(100,  50, 10, 20, 10.0f, TableFactory2())
        };

        [Test, TestCaseSource("CenterFieldData")]
        public void CenterField_TopLeftField_AllFieldsCoordinatesAreAsExpected(
            float screenWidth, float screenHeight,
            int fieldWidth, int fieldHeight,
            float textureSize,
            Table<Tuple<float, float>> expectedTable
        )
        {
            var screenConstants = new ScreenConstants(screenHeight, screenWidth, Color.Black);
            var componentTable = CreateTable(fieldWidth, fieldHeight);
            var map = CreateMap(componentTable, textureSize, screenConstants);

            map.CenterField(0, 0);
            map.Draw();

            var coordinates = componentTable.EnumerateAlongRows()
                .Select(c => new Tuple<float, float>(c.XPosition, c.YPosition));
            var isAsExpected = coordinates
                .SequenceEqual(expectedTable.EnumerateAlongRows());

            Assert.True(isAsExpected);
        }

        [TestCase(10, 32, 9, 8)]
        public void SelectedField_AfterSelection_IsAsExpected(int fieldWidth, int fieldHeight, int fieldX, int fieldY)
        {
            var componentTable = CreateTable(fieldWidth, fieldHeight);
            var map = CreateMap(componentTable);

            map.CenterField(fieldX, fieldY);

            Assert.AreEqual(fieldX, map.CenteredFieldX);
            Assert.AreEqual(fieldY, map.CenteredFieldY);
        }

        [TestCase(10, 21, 2, 2, 2, 1, Direction.Up)]
        [TestCase(10, 21, 2, 2, 1, 2, Direction.Left)]
        [TestCase(10, 21, 2, 2, 3, 2, Direction.Right)]
        [TestCase(10, 21, 2, 2, 2, 3, Direction.Down)]
        [TestCase(1, 1, 0, 0, 0, 0, Direction.Down)]
        [TestCase(1, 1, 0, 0, 0, 0, Direction.Left)]
        [TestCase(1, 1, 0, 0, 0, 0, Direction.Right)]
        [TestCase(1, 1, 0, 0, 0, 0, Direction.Up)]
        public void MoveMap_StartingAtAGivenPoint_MapIsMoved(
            int fieldWidth, int fieldHeight,
            int startingFieldX, int startingFieldY,
            int expectedFieldX, int expedtedFieldY,
            Direction moveDirection)
        {
            var componentTable = CreateTable(fieldWidth, fieldHeight);
            var map = CreateMap(componentTable);

            map.CenterField(startingFieldX, startingFieldY);
            map.MoveMap(moveDirection);

            Assert.AreEqual(expectedFieldX, map.CenteredFieldX);
            Assert.AreEqual(expedtedFieldY, map.CenteredFieldY);
        }
        private static Table<GraphicComponentMock> CreateTable(int fieldWidth, int fieldHeight)
        {
            var table = new Table<GraphicComponentMock>();
            for(var i = 0; i < fieldWidth; i++)
                for(var j = 0; j < fieldHeight; j++)
                    table[j, i] = new GraphicComponentMock();

            return table;
        }

        private static FieldMap CreateMap(int fieldWidth, int fieldHeight, float textureSize = 32.0f, ScreenConstants screenConstants = null)
        {
            var table = CreateTable(fieldWidth, fieldHeight);
            return CreateMap(table, textureSize, screenConstants);
        }

        private static FieldMap CreateMap(Table<GraphicComponentMock> componentTable, float textureSize = 32.0f, ScreenConstants screenConstants = null)
        {
            if(screenConstants == null)
            {
                screenConstants = DefaultScreenConstant;
            }
            var loaderMock = new Mock<IMapLoader>();
            loaderMock.Setup(o => o.GetFieldTextures()).Returns(componentTable);
            var map = new FieldMap(loaderMock.Object, textureSize, screenConstants);
            map.Setup();
            return map;
        }

    }
}