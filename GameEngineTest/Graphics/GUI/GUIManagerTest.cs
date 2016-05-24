using GameEngine;
using GameEngine.Graphics.GUI;
using GameEngineTest.TestUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.GUI
{
    [TestFixture]
    public class GUIManagerTest
    {
        [TestCase]
        public void Draw_AddVisibleWidget_AllGetDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGUIManager();

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
            var guiManager = CreateGUIManager();

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
            var guiManager = CreateGUIManager();

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
            var guiManager = CreateGUIManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            widgetMock2.SetVisibility(false);

            guiManager.Draw();

            Assert.True(widgetMock1.WasDrawn);
            Assert.False(widgetMock2.WasDrawn);
        }

        [TestCase]
        public void Draw_ChangeVisibilityToVisible_WidgetIsDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = false };
            var widgetMock2 = new WidgetMock { IsVisible = false };
            var guiManager = CreateGUIManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            widgetMock2.SetVisibility(true);

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
            var guiManager = CreateGUIManager();

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
            var guiManager = CreateGUIManager();

            widgetMock1.DrawCallback = () => { widget1Order = counter++; };
            widgetMock2.DrawCallback = () => { widget2Order = counter++; };

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            widgetMock2.SetVisibility(true);
            widgetMock1.SetVisibility(true);

            guiManager.Draw();

            Assert.Greater(widget1Order, widget2Order);
        }

        [TestCase]
        public void Draw_CloseGUI_NothingIsDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGUIManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.Close();
            guiManager.Draw();

            Assert.False(widgetMock1.WasDrawn);
            Assert.False(widgetMock2.WasDrawn);
        }

        [TestCase]
        public void HandleInput_AddWidgets_InputIsGivenToLastAddedWidget()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGUIManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.HandleInput(CommandKeys.Up);

            Assert.False(widgetMock1.WasHandleInputCalled);
            Assert.True(widgetMock2.WasHandleInputCalled);
            Assert.AreEqual(CommandKeys.Up, widgetMock2.HandleInputArgument);
        }

        [TestCase]
        public void HandleInput_AddInvisibleWidgetLast_InputIsGivenToFirstWidget()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = false };
            var guiManager = CreateGUIManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);

            guiManager.HandleInput(CommandKeys.Up);

            Assert.True(widgetMock1.WasHandleInputCalled);
            Assert.False(widgetMock2.WasHandleInputCalled);
            Assert.AreEqual(CommandKeys.Up, widgetMock1.HandleInputArgument);
        }

        [TestCase]
        public void HandleInput_ChangeVisibility_InputIsGivenNewVisibleWidget()
        {
            var widgetMock1 = new WidgetMock { IsVisible = false };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGUIManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            widgetMock1.SetVisibility(true);

            guiManager.HandleInput(CommandKeys.Up);

            Assert.True(widgetMock1.WasHandleInputCalled);
            Assert.False(widgetMock2.WasHandleInputCalled);
            Assert.AreEqual(CommandKeys.Up, widgetMock1.HandleInputArgument);
        }

        [TestCase]
        public void HandleInput_ChangeVisibilityOfFocusedWidget_InputIsGivenFirstWidget()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = true };
            var guiManager = CreateGUIManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            widgetMock2.SetVisibility(false);

            guiManager.HandleInput(CommandKeys.Up);

            Assert.True(widgetMock1.WasHandleInputCalled);
            Assert.False(widgetMock2.WasHandleInputCalled);
            Assert.AreEqual(CommandKeys.Up, widgetMock1.HandleInputArgument);
        }

        [TestCase]
        public void HandleInput_NoWidget_DoesNotThrow()
        {
            var guiManager = CreateGUIManager();
            Assert.DoesNotThrow(() => guiManager.HandleInput(CommandKeys.Up));
        }

        [TestCase]
        public void Draw_ChangeVisibilityOfWidgetAfterRemove_IsNotDrawn()
        {
            var widgetMock1 = new WidgetMock { IsVisible = true };
            var widgetMock2 = new WidgetMock { IsVisible = false };
            var guiManager = CreateGUIManager();

            guiManager.AddWidget(widgetMock1);
            guiManager.AddWidget(widgetMock2);
            guiManager.RemoveWidget(widgetMock2);

            widgetMock2.SetVisibility(true);

            guiManager.Draw();

            Assert.False(widgetMock2.WasDrawn);
        }
        private GUIManager CreateGUIManager()
        {
            var manager = new GUIManager();
            manager.Show();
            return manager;
        }
    }
}
