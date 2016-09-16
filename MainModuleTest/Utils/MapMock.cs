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
        public void CenterField(int fieldX, int fieldY)
        {
        }

        public void MoveMap(Direction moveDirection)
        {
            throw new System.NotImplementedException();
        }
    }
}