using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using GameEngine.Utils;
using GameEngineTest.TestUtils;
using MainModule;
using Microsoft.Xna.Framework;
using NUnit.Framework;

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
            var componentTable = CreateTable(fieldWidth, fieldHeight);
            var map = CreateMap(componentTable, textureSize);

            map.CenterField(fieldX, fieldY);
            map.Draw();

            Assert.AreEqual(expectedX, componentTable[fieldX, fieldY].XPosition);
            Assert.AreEqual(expectedY, componentTable[fieldX, fieldY].YPosition);
        }

        public static Table<Tuple<float, float>> TableFactory1()
        {
            var table = new Table<Tuple<float, float>>();
            table[0, 0] = new Tuple<float, float>(45.0f, 45.0f);
            for (var i = 0; i < 20; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    table[i, j] = new Tuple<float, float>(45.0f + 10*i, 45.0f + 10*j);
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
                    table[i, j] = new Tuple<float, float>(45.0f + 10 * i, 20.0f + 10 * j);
                }
            }

            return table;
        }


        public static List<TestCaseData> CenterFieldData = new List<TestCaseData>
        {
            new TestCaseData(100, 100, 10, 20, 10.0f, TableFactory1()),
            new TestCaseData(100,  50, 10, 20, 10.0f, TableFactory2())
        };

        [TestCaseSource("CenterFieldData")]
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

            var isAsExpected = componentTable
                .Select(c => new Tuple<float, float>(c.XPosition, c.YPosition))
                .SequenceEqual(expectedTable);

            Assert.True(isAsExpected);
        }

        private static Table<GraphicComponentMock> CreateTable(int fieldWidth, int fieldHeight)
        {
            var table = new Table<GraphicComponentMock>();
            for(var i = 0; i < fieldWidth; i++)
                for(var j = 0; j < fieldHeight; j++)
                    table[i, j] = new GraphicComponentMock();

            return table;
        }

        private static FieldMap CreateMap(int fieldWidth, int fieldHeight, float textureSize, ScreenConstants screenConstants = null)
        {
            var table = CreateTable(fieldHeight, fieldWidth);
            return CreateMap(table, textureSize, screenConstants);
        }

        private static FieldMap CreateMap(Table<GraphicComponentMock> componentTable, float textureSize, ScreenConstants screenConstants = null)
        {
            if(screenConstants == null)
            {
                screenConstants = DefaultScreenConstant;
            }
            return new FieldMap(componentTable, textureSize, screenConstants);
        }

    }
}