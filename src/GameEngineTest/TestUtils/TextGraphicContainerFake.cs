using GameEngine.GUI;

namespace GameEngineTest.TestUtils
{
    public class TextGraphicContainerFake : GraphicComponentMock, IGuiComponent
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
