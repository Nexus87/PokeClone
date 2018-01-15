using GameEngine.Globals;

namespace GameEngineTest.TestUtils
{
    public class WidgetMock : GraphicComponentMock
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
