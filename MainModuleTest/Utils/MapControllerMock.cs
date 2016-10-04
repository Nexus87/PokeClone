using GameEngineTest.TestUtils;
using MainModule;
using MainModule.Graphics;

namespace MainModuleTest.Utils
{
    public class MapControllerMock : GraphicComponentMock, IMapController
    {

        public void CenterField(int fieldX, int fieldY)
        {
        }

        public void MoveMap(Direction moveDirection)
        {
            throw new System.NotImplementedException();
        }

        public void LoadMap(Map map)
        {
            throw new System.NotImplementedException();
        }
    }
}