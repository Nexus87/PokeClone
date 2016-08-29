using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
