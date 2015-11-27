using GameEngine.Graphics;
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
    class MyTestClass : AbstractGraphicComponent
    {

        public override void Setup(ContentManager content)
        {
            throw new NotImplementedException();
        }

        protected override void DrawComponent(GameTime time, SpriteBatch batch)
        {
        }
    }


    [TestFixture]
    public class AbstractGraphicComponentTest : IGraphicComponentTest
    {
        Mock<AbstractGraphicComponent> componentMock;
        GraphicsAdapter adapter;
        GraphicsDevice dev;
        SpriteBatch sprite;

        [SetUp]
        public void SetUp()
        {
            componentMock = new Mock<AbstractGraphicComponent>();
            componentMock.CallBase = true;
            adapter = GraphicsAdapter.DefaultAdapter;
            dev = new GraphicsDevice(adapter, GraphicsProfile.Reach, new PresentationParameters());
            sprite = new SpriteBatch(dev);
            testObj = componentMock.Object;
            
        }

        [TestCase]
        public void ExecuteUpdateTest()
        {
            var obj = componentMock.Object;
            obj.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.X = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.Y = 1.0f;
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
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.X = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.Object.X = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.ResetCalls();

            componentMock.Object.Y = 1.0f;
            componentMock.Object.Draw(new GameTime(), sprite);
            componentMock.Protected().Verify("Update", Times.Once());

            componentMock.Object.Y = 1.0f;
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
            componentMock.Object.Draw(time, sprite);
            componentMock.Protected().Verify("DrawComponent", Times.Once(), time, sprite);
        }

    }
}
