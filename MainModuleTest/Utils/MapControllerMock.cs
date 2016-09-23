using GameEngineTest.TestUtils;
using MainModule;

namespace MainModuleTest.Utils
{
    public class MapControllerMock : GraphicComponentMock, IMapController
    {
        public MapControllerMock(FieldSize fieldSize)
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