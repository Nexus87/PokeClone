using GameEngine.Graphics;

namespace GameEngineTest.TestUtils
{
    public class SelectableGraphicComponentMock : GraphicComponentMock, ISelectableGraphicComponent
    {
        public bool IsSelected { get; set; }

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
