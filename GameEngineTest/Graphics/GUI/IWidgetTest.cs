using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.GUI
{
    public abstract class IWidgetTest : IGraphicComponentTest
    {
        protected abstract IWidget CreateWidget();

        protected override IGraphicComponent CreateComponent()
        {
            return CreateWidget();
        }

        [TestCase]
        public void Visibility_ChangeVisibilityToFalse_RaisesEvent()
        {
            var widget = CreateWidget();
            bool eventRaised = false;
            widget.IsVisible = true;
            widget.OnVisibilityChanged += delegate { eventRaised = true; };

            widget.IsVisible = false;

            Assert.True(eventRaised);

        }

        [TestCase]
        public void Visibility_ChangeVisibilityToTrue_RaisesEvent()
        {
            var widget = CreateWidget();
            bool eventRaised = false;
            widget.IsVisible = false;
            widget.OnVisibilityChanged += delegate { eventRaised = true; };

            widget.IsVisible = true;

            Assert.True(eventRaised);

        }

        [TestCase]
        public void Visibility_SetSameVisibility_NoEventRaised()
        {
            var widget = CreateWidget();
            bool eventRaised = false;
            widget.IsVisible = false;
            widget.OnVisibilityChanged += delegate { eventRaised = true; };

            widget.IsVisible = false;

            Assert.False(eventRaised);

        }
    }
}
