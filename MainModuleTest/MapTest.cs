using GameEngine.Graphics;
using GameEngine.Utils;
using MainModule;
using NUnit.Framework;

namespace MainModuleTest
{
    [TestFixture]
    public class MapTest
    {
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


        private static FieldMap CreateMap(int fieldWidth, int fieldHeight, float textureSize)
        {
            var table = new Table<IGraphicComponent>(fieldHeight, fieldWidth);
            return new FieldMap(table, textureSize);
        }
    }
}