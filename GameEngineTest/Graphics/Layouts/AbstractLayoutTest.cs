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
using GameEngine.Graphics.Basic;
using GameEngine;

namespace GameEngineTest.Graphics.Layouts
{
    class TestLayout : AbstractLayout
    {
        protected override void UpdateComponents(Container container) { }

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
            testContainer = new Container(engineMock.Object);
            testContainer.FillContainer(10);
            testContainer.Layout = testLayout;
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
            var container = new Container(engineMock.Object);
            var testObj = new TestLayout();
            container.SetCoordinates(X, Y, Width, Height);

            testObj.LayoutContainer(container);
            testObj.TestProperties(X, Y, Width, Height);

            testObj.SetMargin(left: Margin);
            testObj.LayoutContainer(container);
            testObj.TestProperties(X + Margin, Y, Width - Margin, Height);

            testObj.SetMargin(right: Margin);
            testObj.LayoutContainer(container);
            testObj.TestProperties(X, Y, Width - Margin, Height);

            testObj.SetMargin(top: Margin);
            testObj.LayoutContainer(container);
            testObj.TestProperties(X, Y + Margin, Width, Height - Margin);

            testObj.SetMargin(bottom: Margin);
            testObj.LayoutContainer(container);
            testObj.TestProperties(X, Y, Width, Height - Margin);

            testObj.SetMargin(Margin, Margin, Margin, Margin);
            testObj.LayoutContainer(container);
            testObj.TestProperties(X + Margin, Y + Margin, Width - 2*Margin, Height - 2*Margin);
        }

        [TestCase]
        public void UpdateCalledTest()
        {
            var container = new Container(engineMock.Object);
            container.SetCoordinates(200, 200, 200, 200);
            testLayout.LayoutContainer(container);
            layoutMock.Protected().Verify("UpdateComponents", Times.Once(), container);
        }
    }
}
