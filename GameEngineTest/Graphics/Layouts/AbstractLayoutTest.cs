using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameEngineTest.Util;
using Moq.Protected;

namespace GameEngineTest.Graphics.Layouts
{
    class TestLayout : AbstractLayout
    {
        public override void AddComponent(IGraphicComponent component) { throw new NotImplementedException(); }
        public override void RemoveComponent(IGraphicComponent component) { throw new NotImplementedException(); }
        public override void Setup(ContentManager content) { throw new NotImplementedException(); }
        protected override void DrawComponents(GameTime time, ISpriteBatch batch) { throw new NotImplementedException(); }
        protected override void UpdateComponents() { throw new NotImplementedException(); }

        public void TestProperties(float X, float Y, float Width, float Height)
        {
            Assert.GreaterOrEqual(this.X, X);
            Assert.GreaterOrEqual(this.Y, Y);
            Assert.LessOrEqual(this.Width, Width);
            Assert.LessOrEqual(this.Height, Height);
        }
    }
    [TestFixture]
    public class AbstractLayoutTest : ILayoutTest
    {
        public Mock<AbstractLayout> layoutMock;
        [SetUp]
        public void Setup()
        {
            layoutMock = new Mock<AbstractLayout>();
            layoutMock.CallBase = true;
            layout = layoutMock.Object;
        }

        [TestCase]
        public void ProtectedPropertiesTest()
        {
            float X = 1.0f;
            float Y = 1.0f;
            float Width = 30.0f;
            float Height = 50.0f;
            int Margin = 10;

            var compMock = new Mock<IGraphicComponent>();
            var testObj = new TestLayout();

            compMock.Setup(o => o.X).Returns(X);
            compMock.Setup(o => o.Y).Returns(Y);
            compMock.Setup(o => o.Width).Returns(Width);
            compMock.Setup(o => o.Height).Returns(Height);

            testObj.Init(compMock.Object);

            testObj.TestProperties(X, Y, Width, Height);

            testObj.SetMargin(left: Margin);
            testObj.TestProperties(X + Margin, Y, Width - Margin, Height);

            testObj.SetMargin(right: Margin);
            testObj.TestProperties(X, Y, Width - Margin, Height);

            testObj.SetMargin(top: Margin);
            testObj.TestProperties(X, Y + Margin, Width, Height - Margin);

            testObj.SetMargin(bottom: Margin);
            testObj.TestProperties(X, Y, Width, Height - Margin);

            testObj.SetMargin(Margin, Margin, Margin, Margin);
            testObj.TestProperties(X + Margin, Y + Margin, Width - 2*Margin, Height - 2*Margin);
        }

        [TestCase]
        public void UpdateCalledTest()
        {
            float X = 1.0f;
            float Y = 1.0f;
            float Width = 30.0f;
            float Height = 50.0f;

            var compMock = new Mock<IGraphicComponent>();
            var batch = new SpriteBatchMock();

            compMock.Setup(o => o.X).Returns(X);
            compMock.Setup(o => o.Y).Returns(Y);
            compMock.Setup(o => o.Width).Returns(Width);
            compMock.Setup(o => o.Height).Returns(Height);

            layout.Init(compMock.Object);

            layout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once());
            layoutMock.ResetCalls();

            layout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Never());
            layoutMock.ResetCalls();

            compMock.Raise(o => o.PositionChanged += null, new EventArgs());
            layout.Draw(new GameTime(), batch);
            layout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once());
            layoutMock.ResetCalls();

            compMock.Raise(o => o.SizeChanged += null, new EventArgs());
            layout.Draw(new GameTime(), batch);
            layout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once());
            layoutMock.ResetCalls();

            compMock.Raise(o => o.SizeChanged += null, new EventArgs());
            compMock.Raise(o => o.PositionChanged += null, new EventArgs());
            layout.Draw(new GameTime(), batch);
            layout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once());
            layoutMock.ResetCalls();
        }
    }
}
