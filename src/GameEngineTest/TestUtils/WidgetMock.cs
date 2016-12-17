using GameEngine;
using GameEngine.Globals;
using GameEngine.Graphics.GUI;
using GameEngine.GUI.Graphics.GUI;

namespace GameEngineTest.TestUtils
{
    public class WidgetMock : GraphicComponentMock, IWidget
    {
        public bool WasHandleInputCalled = false;
        public CommandKeys HandleInputArgument;

        public bool HandleInput(CommandKeys key)
        {
            WasHandleInputCalled = true;
            HandleInputArgument = key;

            return true;
        }
    }
}
