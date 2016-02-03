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
            Assert.GreaterOrEqual(this.Width, 0);
            Assert.GreaterOrEqual(this.Height, 0);

            if (Width >= 0)
                Assert.LessOrEqual(this.Width, Width);
            else
                Assert.AreEqual(0, this.Width);

            if(Height >= 0)
                Assert.LessOrEqual(this.Height, Height);
            else
                Assert.AreEqual(0, this.Height);
        }

        public override int Rows { get { throw new NotImplementedException(); } }
        public override int Columns { get { throw new NotImplementedException(); } }

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
            testLayout = layoutMock.Object;
        }

        public static List<TestCaseData> ValidData = new List<TestCaseData>
        {
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 0.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 0.0f, 0),
            new TestCaseData(0.0f, 0.0f, 0.0f, 0.0f, 0),
            new TestCaseData(0.0f, 0.0f, 150.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 150.0f, 0),
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 10),
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 40),
            new TestCaseData(0.0f, 0.0f, 0.0f, 50.0f, 10),
            new TestCaseData(0.0f, 0.0f, 50.0f, 0.0f, 10),
            new TestCaseData(0.0f, 0.0f, 0.0f, 0.0f, 10),
        };
        [TestCaseSource("ValidData")]
        public void ProtectedPropertiesTest(float X, float Y, float Width, float Height, int Margin)
        {
            var compMock = new Mock<IGraphicComponent>();
            var testObj = new TestLayout();
            compMock.SetCoordinates(X, Y, Width, Height);

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

            testLayout.Init(compMock.Object);

            testLayout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once());
            layoutMock.ResetCalls();

            testLayout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Never());
            layoutMock.ResetCalls();

            compMock.Raise(o => o.PositionChanged += null, new GraphicComponentPositionChangedArgs(X, Y));
            testLayout.Draw(new GameTime(), batch);
            testLayout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once());
            layoutMock.ResetCalls();

            compMock.Raise(o => o.SizeChanged += null, new GraphicComponentSizeChangedArgs(Width, Height));
            testLayout.Draw(new GameTime(), batch);
            testLayout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once());
            layoutMock.ResetCalls();

            compMock.Raise(o => o.SizeChanged += null, new GraphicComponentSizeChangedArgs(Width, Height));
            compMock.Raise(o => o.PositionChanged += null, new GraphicComponentPositionChangedArgs(X, Y));
            testLayout.Draw(new GameTime(), batch);
            testLayout.Draw(new GameTime(), batch);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once());
            layoutMock.ResetCalls();
        }
    }
}
