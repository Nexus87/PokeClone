using GameEngine.GUI.Graphics;

namespace GameEngineTest.TestUtils
{
    public class TextGraphicContainerFake : GraphicComponentMock, IGraphicComponent
    {
        public bool HasNext()
        {
            return true;
        }

        public void NextLine()
        {
        }

        public string Text { get; set; }

    }
}
