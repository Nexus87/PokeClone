using GameEngine.Graphics;

namespace GameEngineTest.TestUtils
{
    public class TableComponentMock<T> : TextGraphicComponentMock, ISelectableTextComponent
    {
        public T Data { get; set; }
        public bool IsSelected { get; set; }



        public void Select() { IsSelected = true; }
        public void Unselect() { IsSelected = false; }

    }
}
