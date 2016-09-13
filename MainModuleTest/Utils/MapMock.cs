using GameEngineTest.TestUtils;
using MainModule;

namespace MainModuleTest.Utils
{
    public class MapMock : GraphicComponentMock, IMap
    {
        public MapMock(FieldSize fieldSize)
        {
            FieldSize = fieldSize;
        }

        public FieldSize FieldSize { get; set; }
    }
}