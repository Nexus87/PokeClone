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

        private GUIManager CreateGUIManager()
        {
            var manager = new GUIManager();
            manager.Show();
            return manager;
        }
    }
}
