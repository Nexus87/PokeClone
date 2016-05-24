using GameEngine;
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
        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;
        public bool WasHandleInputCalled = false;
        public CommandKeys HandleInputArgument;
        public bool IsVisible { get; set; }

        public bool HandleInput(CommandKeys key)
        {
            WasHandleInputCalled = true;
            HandleInputArgument = key;

            return true;
        }

        public void SetVisibility(bool visibility)
        {
            IsVisible = visibility;
            VisibilityChanged(this, new VisibilityChangedEventArgs(visibility));
        }
    }
}
