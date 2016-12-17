using GameEngine.GUI.Graphics;

namespace GameEngineTest.TestUtils
{
    public class TextGraphicContainerFake : GraphicComponentMock, ITextGraphicContainer
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
