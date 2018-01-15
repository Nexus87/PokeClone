//using FakeItEasy;
//using GameEngine.Globals;
//using GameEngineTest.TestUtils;
//using NUnit.Framework;

//namespace GameEngineTest.Graphics.GUI
//{
//    [TestFixture]
//    public class GuiManagerTest
//    {
//        [TestCase]
//        public void Draw_AddVisibleWidget_AllGetDrawn()
//        {
//            var widgetMock1 = new WidgetMock();
//            var widgetMock2 = new WidgetMock();
//            var guiManager = CreateGuiManager();

//            guiManager.ShowWidget(widgetMock1);
//            guiManager.ShowWidget(widgetMock2);

//            guiManager.Draw();

//            Assert.True(widgetMock1.WasDrawn);
//            Assert.True(widgetMock2.WasDrawn);

//        }

//        [TestCase]
//        public void Draw_RemoveWidget_RemovedWidgetIsNotDrawn()
//        {
//            var widgetMock1 = new WidgetMock();
//            var widgetMock2 = new WidgetMock();
//            var guiManager = CreateGuiManager();

//            guiManager.ShowWidget(widgetMock1);
//            guiManager.ShowWidget(widgetMock2);
//            guiManager.CloseWidget(widgetMock2);

//            guiManager.Draw();

//            Assert.True(widgetMock1.WasDrawn);
//            Assert.False(widgetMock2.WasDrawn);
//        }

//        [TestCase]
//        public void Draw_CloseGUI_NothingIsDrawn()
//        {
//            var widgetMock1 = new WidgetMock();
//            var widgetMock2 = new WidgetMock();
//            var guiManager = CreateGuiManager();

//            guiManager.ShowWidget(widgetMock1);
//            guiManager.ShowWidget(widgetMock2);

//            guiManager.Close();
//            guiManager.Draw();

//            Assert.False(widgetMock1.WasDrawn);
//            Assert.False(widgetMock2.WasDrawn);
//        }

//        [TestCase]
//        public void HandleKeyInput_AddWidgets_InputIsGivenToLastAddedWidget()
//        {
//            var widgetMock1 = new WidgetMock();
//            var widgetMock2 = new WidgetMock();
//            var guiManager = CreateGuiManager();

//            guiManager.ShowWidget(widgetMock1);
//            guiManager.ShowWidget(widgetMock2);

//            guiManager.HandleKeyInput(CommandKeys.Up);

//            Assert.False(widgetMock1.WasHandleKeyInputCalled);
//            Assert.True(widgetMock2.WasHandleKeyInputCalled);
//            Assert.AreEqual(CommandKeys.Up, widgetMock2.HandleKeyInputArgument);
//        }

//        [TestCase]
//        public void HandleKeyInput_NoWidget_DoesNotThrow()
//        {
//            var guiManager = CreateGuiManager();
//            Assert.DoesNotThrow(() => guiManager.HandleKeyInput(CommandKeys.Up));
//        }

//        private static GuiManager CreateGuiManager()
//        {
//            var manager = new GuiManager(A.Fake<IInputHandlerManager>());
//            manager.Show();
//            return manager;
//        }
//    }
//}
