using GameEngine;
using GameEngine.Globals;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics.GUI
{
    [TestFixture]
    public class GuiManagerTest
    {
        [TestCase]
        public void Draw_AddVisibleWidget_AllGetDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.Draw();

            Assert.True(widgetMock1.WasDrawn);
            Assert.True(widgetMock2.WasDrawn);

        }

        [TestCase]
        public void Draw_AddInvisibleWidgets_NoneIsDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = false };
            var widgetMock2 = new WidgetMock { IsVisible = false };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.Draw();

            Assert.False(widgetMock1.WasDrawn);
            Assert.False(widgetMock2.WasDrawn);
        }

        [TestCase]
        public void Draw_RemoveWidget_RemovedWidgetIsNotDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            guiManager.RemoveWidget(widgetMock2);

            guiManager.Draw();

            Assert.True(widgetMock1.WasDrawn);
            Assert.False(widgetMock2.WasDrawn);
        }

        [TestCase]
        public void Draw_VisiblilityChangeToNotVisible_WidgetIsNotDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            widgetMock2.IsVisible = false;

            guiManager.Draw();

            Assert.True(widgetMock1.WasDrawn);
            Assert.False(widgetMock2.WasDrawn);
        }

        [TestCase]
        public void Draw_ChangeVisibilityToVisible_WidgetIsDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = false };
            var widgetMock2 = new WidgetMock { IsVisible = false };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            widgetMock2.IsVisible = true;

            guiManager.Draw();

            Assert.False(widgetMock1.WasDrawn);
            Assert.True(widgetMock2.WasDrawn);
        }

        [TestCase]
        public void Draw_AddWidgets_WidgetsAreDrawnInTheAddedOrder()
        {
            int counter = 1;
            int widget1Order = 0;
            int widget2Order = 0;

            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGuiManager();

            widgetMock1.DrawCallback = () => { widget1Order = counter++; };
            widgetMock2.DrawCallback = () => { widget2Order = counter++; };

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.Draw();

            Assert.Greater(widget2Order, widget1Order);
        }

        [TestCase]
        public void Draw_ChangeVisibilityToTrue_WidgetsAreDrawnInTheOrderTheirVisiblityChanged()
        {
            int counter = 1;
            int widget1Order = 0;
            int widget2Order = 0;

            var widgetMock1 = new WidgetMock { IsVisible = false };
            var widgetMock2 = new WidgetMock { IsVisible = false };
            var guiManager = CreateGuiManager();

            widgetMock1.DrawCallback = () => { widget1Order = counter++; };
            widgetMock2.DrawCallback = () => { widget2Order = counter++; };

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            widgetMock2.IsVisible = true;
            widgetMock1.IsVisible = true;

            guiManager.Draw();

            Assert.Greater(widget1Order, widget2Order);
        }

        [TestCase]
        public void Draw_CloseGUI_NothingIsDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.Close();
            guiManager.Draw();

            Assert.False(widgetMock1.WasDrawn);
            Assert.False(widgetMock2.WasDrawn);
        }

        [TestCase]
        public void HandleKeyInput_AddWidgets_InputIsGivenToLastAddedWidget()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.HandleKeyInput(CommandKeys.Up);

            Assert.False(widgetMock1.WasHandleKeyInputCalled);
            Assert.True(widgetMock2.WasHandleKeyInputCalled);
            Assert.AreEqual(CommandKeys.Up, widgetMock2.HandleKeyInputArgument);
        }

        [TestCase]
        public void HandleKeyInput_AddInvisibleWidgetLast_InputIsGivenToFirstWidget()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = false };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.HandleKeyInput(CommandKeys.Up);

            Assert.True(widgetMock1.WasHandleKeyInputCalled);
            Assert.False(widgetMock2.WasHandleKeyInputCalled);
            Assert.AreEqual(CommandKeys.Up, widgetMock1.HandleKeyInputArgument);
        }

        [TestCase]
        public void HandleKeyInput_ChangeVisibility_InputIsGivenNewVisibleWidget()
        {
            var widgetMock1 = new WidgetMock { IsVisible = false };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            widgetMock1.IsVisible = true;

            guiManager.HandleKeyInput(CommandKeys.Up);

            Assert.True(widgetMock1.WasHandleKeyInputCalled);
            Assert.False(widgetMock2.WasHandleKeyInputCalled);
            Assert.AreEqual(CommandKeys.Up, widgetMock1.HandleKeyInputArgument);
        }

        [TestCase]
        public void HandleKeyInput_ChangeVisibilityOfFocusedWidget_InputIsGivenFirstWidget()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            widgetMock2.IsVisible = false;

            guiManager.HandleKeyInput(CommandKeys.Up);

            Assert.True(widgetMock1.WasHandleKeyInputCalled);
            Assert.False(widgetMock2.WasHandleKeyInputCalled);
            Assert.AreEqual(CommandKeys.Up, widgetMock1.HandleKeyInputArgument);
        }

        [TestCase]
        public void HandleKeyInput_NoWidget_DoesNotThrow()
        {
            var guiManager = CreateGuiManager();
            Assert.DoesNotThrow(() => guiManager.HandleKeyInput(CommandKeys.Up));
        }

        [TestCase]
        public void Draw_ChangeVisibilityOfWidgetAfterRemove_IsNotDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = false };
            var guiManager = CreateGuiManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            guiManager.RemoveWidget(widgetMock2);

            widgetMock2.IsVisible = true;

            guiManager.Draw();

            Assert.False(widgetMock2.WasDrawn);
        }
        private static GuiManager CreateGuiManager()
        {
            var manager = new GuiManager();
            manager.Show();
            return manager;
        }
    }
}
