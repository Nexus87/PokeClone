﻿using GameEngine.Graphics;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class AbstractGraphicComponentTest : IGraphicComponentTest
    {
        Mock<AbstractGraphicComponent> componentMock;
        Mock<ISpriteBatch> batch;

        [SetUp]
        public void SetUp()
        {
            componentMock = new Mock<AbstractGraphicComponent>(gameMock.Object);
            componentMock.CallBase = true;
            batch = new Mock<ISpriteBatch>();

            testObj = componentMock.Object;
        }

        [TestCase]
        public void ExecuteUpdateTest()
        {
            var sprite = batch.Object;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.XPosition = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.YPosition = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.Width = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.Height = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());
        }

        [TestCase]
        public void MultipleChangedUpdateTest()
        {
            var sprite = batch.Object;

            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.XPosition = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.Object.XPosition = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.YPosition = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.Object.YPosition = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.Width = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.Object.Width = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.Height = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.Object.Height = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());
        }

        [TestCase]
        public void DrawComponentCalled()
        {
            var time = new GameTime();
            var sprite = batch.Object;
            componentMock.Object.Draw(time, sprite);
            componentMock.Protected().Verify("DrawComponent", Times.Once(), time, sprite);
        }

    }
}
