using GameEngine.Globals;
using GameEngine.GUI;

namespace GameEngineTest.TestUtils
{
    public class WidgetMock : GraphicComponentMock, IInputHandler, IGraphicComponent
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
