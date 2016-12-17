using GameEngine.Graphics;
using GameEngine.GUI.Graphics;

namespace GameEngineTest.TestUtils
{
    public class SelectableGraphicComponentMock : GraphicComponentMock, ISelectableGraphicComponent
    {
        public void Select()
        {
            IsSelected = true;
        }

        public void Unselect()
        {
            IsSelected = false;
        }
    }
}
