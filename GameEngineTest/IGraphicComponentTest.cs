using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest
{

    public abstract class IGraphicComponentTest
    {
        public IGraphicComponent testObj;
        public Mock<ContentManager> contentMock;

        public IGraphicComponentTest()
        {
            var serviceMock = new Mock<IServiceProvider>();
            contentMock = new Mock<ContentManager>(serviceMock.Object);
        }

        public static List<TestCaseData> InvalidData = new List<TestCaseData>{
            new TestCaseData(0.0f, 0.0f, -1.0f, 1.0f),
            new TestCaseData(0.0f, 0.0f, 1.0f, -1.0f),
            new TestCaseData(0.0f, 0.0f, -1.0f, -1.0f)
        };

        [TestCaseSource(typeof(IGraphicComponentTest), "InvalidData")]
        public void InvalidCoordinatesTest(float X, float Y, float Width, float Height)
        {
            Assert.Throws<ArgumentException>(delegate()
                {
                    testObj.X = X;
                    testObj.Y = Y;
                    testObj.Width = Width;
                    testObj.Height = Height;
                }
            );
        }
        [TestCase]
        public void ChangeEventTest()
        {
            bool sizeEventTriggered = false;
            bool positionEventTriggered = false;

            testObj.SizeChanged += delegate { sizeEventTriggered = true; };
            testObj.PositionChanged += delegate { positionEventTriggered = true; };

            testObj.Width = 1.0f;
            Assert.IsTrue(sizeEventTriggered);
            Assert.IsFalse(positionEventTriggered);

            sizeEventTriggered = false;

            testObj.Height = 1.0f;
            Assert.IsTrue(sizeEventTriggered);
            Assert.IsFalse(positionEventTriggered);

            sizeEventTriggered = false;

            testObj.X = 1.0f;
            Assert.IsTrue(positionEventTriggered);
            Assert.IsFalse(sizeEventTriggered);

            positionEventTriggered = false;

            testObj.Y = 1.0f;
            Assert.IsTrue(positionEventTriggered);
            Assert.IsFalse(sizeEventTriggered);
        }

        [TestCase]
        public void NoEventTriggeredTest()
        {
            bool sizeEventTriggered = false;
            bool positionEventTriggered = false;

            testObj.Width = 1.0f;
            testObj.Height = 1.0f;
            testObj.X = 1.0f;
            testObj.Y = 1.0f;

            testObj.SizeChanged += delegate { sizeEventTriggered = true; };
            testObj.PositionChanged += delegate { positionEventTriggered = true; };

            testObj.Width = 1.0f;
            Assert.IsFalse(sizeEventTriggered);
            Assert.IsFalse(positionEventTriggered);

            testObj.Height = 1.0f;
            Assert.IsFalse(sizeEventTriggered);
            Assert.IsFalse(positionEventTriggered);

            testObj.X = 1.0f;
            Assert.IsFalse(positionEventTriggered);
            Assert.IsFalse(sizeEventTriggered);

            testObj.Y = 1.0f;
            Assert.IsFalse(positionEventTriggered);
            Assert.IsFalse(sizeEventTriggered);

        }

        [TestCase]
        public void PropertySetterTest()
        {
            float TestValueX = 1.0f;
            float TestValueY = 2.0f;
            float TestValueWidth = 3.0f;
            float TestValueHeight = 4.0f;

            float DefaultValue = 0.0f;

            Assert.AreEqual(DefaultValue, testObj.X);
            Assert.AreEqual(DefaultValue, testObj.Y);
            Assert.AreEqual(DefaultValue, testObj.Width);
            Assert.AreEqual(DefaultValue, testObj.Height);

            testObj.X = TestValueX;
            Assert.AreEqual(TestValueX, testObj.X);
            Assert.AreEqual(DefaultValue, testObj.Y);
            Assert.AreEqual(DefaultValue, testObj.Width);
            Assert.AreEqual(DefaultValue, testObj.Height);

            testObj.Y = TestValueY;
            Assert.AreEqual(TestValueX, testObj.X);
            Assert.AreEqual(TestValueY, testObj.Y);
            Assert.AreEqual(DefaultValue, testObj.Width);
            Assert.AreEqual(DefaultValue, testObj.Height);

            testObj.Width = TestValueWidth;
            Assert.AreEqual(TestValueX, testObj.X);
            Assert.AreEqual(TestValueY, testObj.Y);
            Assert.AreEqual(TestValueWidth, testObj.Width);
            Assert.AreEqual(DefaultValue, testObj.Height);

            testObj.Height = TestValueHeight;
            Assert.AreEqual(TestValueX, testObj.X);
            Assert.AreEqual(TestValueY, testObj.Y);
            Assert.AreEqual(TestValueWidth, testObj.Width);
            Assert.AreEqual(TestValueHeight, testObj.Height);
        }

        public static List<TestCaseData> DivideCases = new List<TestCaseData>{
            new TestCaseData(5.0f, 5.0f, 150.0f, 10.0f ),
            new TestCaseData( 0.0f, 0.0f, 150.0f, 10.0f ),
            new TestCaseData( 0.0f, 0.0f, 0.0f, 10.0f ),
            new TestCaseData( 0.0f, 0.0f, 150.0f, 0.0f)
        };

        [Test, TestCaseSource(typeof(IGraphicComponentTest), "DivideCases")]
        public void DrawInConstraintsTest(float X, float Y, float Width, float Height)
        {
            SpriteBatchMock batch = new SpriteBatchMock();

            testObj.X = X;
            testObj.Y = Y;
            testObj.Width = Width;
            testObj.Height = Height;

            testObj.Draw(new GameTime(), batch);

            foreach (var obj in batch.Objects)
                Assert.IsTrue(obj.IsInConstraints(testObj));
        }
    }
}
