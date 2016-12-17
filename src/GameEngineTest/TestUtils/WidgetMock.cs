using GameEngine.Globals;
using GameEngine.GUI.Graphics.GUI;

namespace GameEngineTest.TestUtils
{
    public class WidgetMock : GraphicComponentMock, IWidget
    {
        public bool WasHandleKeyInputCalled;
        public CommandKeys HandleKeyInputArgument;

        public override void HandleKeyInput(CommandKeys key)
        {
            WasHandleKeyInputCalled = true;
            HandleKeyInputArgument = key;
        }
    }
}
