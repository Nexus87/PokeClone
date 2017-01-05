using FakeItEasy;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngine.GUI.Test.Panels
{
    [TestFixture]
    public class ScrollAreaTest
    {
        private IGuiComponent _content;
        private readonly IGuiComponent _focusedComponent;

        public ScrollAreaTest()
        {

            _focusedComponent = A.Fake<IGuiComponent>();
        }

        [TestCase(
             100,  200, 100,  50,
             200,  300,  50,  60,
             -50, -110
         )]
        [TestCase(
             100,  200, 100,  50,
              50,   10,  20,  30,
              50,  190
         )]
        [TestCase(
             100,  200, 100,  50,
             150,  210,  20,  30,
               0,    0
         )]
        public void FocusChangedEventHandler_AfterRaisingEvent_ComponentIsAsExpected(
            int x, int y, int width, int height,
            int focusedX, int focusedY, int focusedWidth, int focusedHeight,
            int expectedX, int expectedY
        )
        {
            var initalArea = new Rectangle(x, y, width, height);
            var focusedArea = new Rectangle(focusedX, focusedY, focusedWidth, focusedHeight);
            var expectedArea = new Rectangle(expectedX, expectedY, 0, 0);

            var scrollArea = CreateScrollArea(initalArea);
            scrollArea.Content =_content;
            _content.SetCoordinates(0, 0, 1000, 1000);
            _focusedComponent.Area = focusedArea;

            _content.ComponentSelected += Raise.With(new ComponentSelectedEventArgs {Source = _focusedComponent});

            Assert.AreEqual(expectedArea.Top, _content.Area.Top);
            Assert.AreEqual(expectedArea.Left, _content.Area.Left);
        }

        private ScrollArea CreateScrollArea(Rectangle initalArea)
        {
            var scrollArea = new ScrollArea(new ClassicScrollAreaRenderer()) {Area = initalArea};
            _content = A.Fake<IGuiComponent>();
            return scrollArea;
        }
    }
}