using GameEngine.Graphics.Basic;
using GameEngine.Graphics;
using GameEngine.Utils;
using GameEngineTest.Util;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineTest.Views;

namespace GameEngineTest.Graphics.Basic
{
    [TestFixture]
    public class MultiLayeredComponentTest : IGraphicComponentTest
    {

        [TestCase]
        public void Draw_NoBackground_NoBackgroundDrawn()
        {
            var backgroundComponent = new GraphicComponentMock();
            var testComponent = CreateComponent(background: backgroundComponent);
            var batch = new SpriteBatchMock();
            testComponent.SetCoordinates(100, 100, 500, 500);

            testComponent.Background = null;
            testComponent.Draw(batch);

            Assert.False(backgroundComponent.WasDrawn);
        }

        [TestCase]
        public void Draw_NoForeground_NoForegroundDrawn()
        {
            var foregroundComponent = new GraphicComponentMock();
            var testComponent = CreateComponent(foreground: foregroundComponent);
            var batch = new SpriteBatchMock();
            testComponent.SetCoordinates(100, 100, 500, 500);

            testComponent.Foreground = null;
            testComponent.Draw(batch);

            Assert.False(foregroundComponent.WasDrawn);
        }

        [TestCase]
        public void Draw_AllComponentsSet_RightOrder()
        {
            int drawCounter = 0;
            int foregroundNumber = -1;
            int mainComponentNumber = -1;
            int backgroundNumber = -1;

            var foregroundComponent = new GraphicComponentMock();
            var mainComponent = new GraphicComponentMock();
            var backgroundComponent = new GraphicComponentMock();

            var testComponent = CreateComponent(mainComponent, backgroundComponent, foregroundComponent);
            foregroundComponent.DrawCallback = () => { drawCounter++; foregroundNumber = drawCounter; };
            mainComponent.DrawCallback = () => { drawCounter++; mainComponentNumber = drawCounter; };
            backgroundComponent.DrawCallback = () => { drawCounter++; backgroundNumber = drawCounter; };

            testComponent.Draw(new SpriteBatchMock());

            Assert.AreEqual(1, backgroundNumber);
            Assert.AreEqual(2, mainComponentNumber);
            Assert.AreEqual(3, foregroundNumber);
        }

        [TestCase]
        public void MainComponent_SetNull_ThrowsException()
        {
            var testComponent = CreateComponent();

            Assert.Throws<ArgumentNullException>(() => testComponent.MainComponent = null);
        }

        [TestCase]
        public void SetCoordinates_ValidCoordinates_InnerComponentsAreUpdated()
        {
            float x = 100;
            float y = 200;
            float width = 300;
            float height = 400;
            var foregroundComponent = new GraphicComponentMock();
            var backgroundComponent = new GraphicComponentMock();
            var mainComponent = new GraphicComponentMock();

            var testComponent = CreateComponent(mainComponent, backgroundComponent, foregroundComponent);

            testComponent.SetCoordinates(x, y, width, height);

            testComponent.Draw(new SpriteBatchMock());

            AssertCoordinatesEqual(foregroundComponent, testComponent);
            AssertCoordinatesEqual(backgroundComponent, testComponent);
            AssertCoordinatesEqual(mainComponent, testComponent);
        }


        private void AssertCoordinatesEqual(IGraphicComponent firstComponent, IGraphicComponent secondComponent)
        {
            Assert.AreEqual(firstComponent.XPosition, secondComponent.XPosition);
            Assert.AreEqual(firstComponent.YPosition, secondComponent.YPosition);
            Assert.AreEqual(firstComponent.Width, secondComponent.Width);
            Assert.AreEqual(firstComponent.Height, secondComponent.Height);
        }

        private MultiLayeredComponent CreateComponent(IGraphicComponent mainComponent = null, IGraphicComponent background = null, IGraphicComponent foreground = null)
        {
            if (mainComponent == null)
                mainComponent = new GraphicComponentMock();
            var testComponent = new MultiLayeredComponent(mainComponent, gameMock.Object);
            testComponent.Background = background;
            testComponent.Foreground = foreground;

            return testComponent;
        }

        protected override IGraphicComponent CreateComponent()
        {
            var mainComponent = new GraphicComponentMock();
            var backgroundComponent = new GraphicComponentMock();
            var foregroundComponent = new GraphicComponentMock();

            return CreateComponent(mainComponent, backgroundComponent, foregroundComponent);
        }
    }
}
