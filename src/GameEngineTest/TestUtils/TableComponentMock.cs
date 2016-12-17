using GameEngine.Graphics;
using GameEngine.GUI.Graphics;

namespace GameEngineTest.TestUtils
{
    public class TableComponentMock<T> : TextGraphicComponentMock, ISelectableTextComponent
    {
        public T Data { get; set; }


        public void Select() { IsSelected = true; }
        public void Unselect() { IsSelected = false; }

    }
}
