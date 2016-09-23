using GameEngineTest.TestUtils;
using MainModule;

namespace MainModuleTest.Utils
{
    public class MapControlerMock : GraphicComponentMock, IMapControler
    {
        public MapControlerMock(FieldSize fieldSize)
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