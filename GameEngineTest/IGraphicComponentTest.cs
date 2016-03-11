using GameEngine;
using GameEngine.Graphics;
using GameEngine.Wrapper;
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
        public Mock<ISpriteFont> fontMock;
        public Mock<PokeEngine> gameMock;

        public IGraphicComponentTest()
        {
            var serviceMock = new Mock<IServiceProvider>();
            contentMock = new Mock<ContentManager>(serviceMock.Object);
            fontMock = new Mock<ISpriteFont>();
            gameMock = new Mock<PokeEngine>(new Configuration());
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
                    testObj.XPosition = X;
                    testObj.YPosition = Y;
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
            GraphicComponentPositionChangedEventArgs positionArgs = null;
            GraphicComponentSizeChangedEventArgs sizeArgs = null;

            testObj.SizeChanged += (a, b) => { sizeEventTriggered = true; sizeArgs = b; };
            testObj.PositionChanged += (a, b) => { positionEventTriggered = true; positionArgs = b; };

            testObj.Width = 1.0f;
            Assert.IsTrue(sizeEventTriggered);
            Assert.IsFalse(positionEventTriggered);
            Assert.AreEqual(1.0f, sizeArgs.Width);

            sizeEventTriggered = false;

            testObj.Height = 1.0f;
            Assert.IsTrue(sizeEventTriggered);
            Assert.IsFalse(positionEventTriggered);
            Assert.AreEqual(1.0f, sizeArgs.Height);

            sizeEventTriggered = false;

            testObj.XPosition = 1.0f;
            Assert.IsTrue(positionEventTriggered);
            Assert.IsFalse(sizeEventTriggered);
            Assert.AreEqual(1.0f, positionArgs.XPosition);

            positionEventTriggered = false;

            testObj.YPosition = 1.0f;
            Assert.IsTrue(positionEventTriggered);
            Assert.IsFalse(sizeEventTriggered);
            Assert.AreEqual(1.0f, positionArgs.YPosition);
        }

        [TestCase]
        public void NoEventTriggeredTest()
        {
            bool sizeEventTriggered = false;
            bool positionEventTriggered = false;

            testObj.Width = 1.0f;
            testObj.Height = 1.0f;
            testObj.XPosition = 1.0f;
            testObj.YPosition = 1.0f;

            testObj.SizeChanged += delegate { sizeEventTriggered = true; };
            testObj.PositionChanged += delegate { positionEventTriggered = true; };

            testObj.Width = 1.0f;
            Assert.IsFalse(sizeEventTriggered);
            Assert.IsFalse(positionEventTriggered);

            testObj.Height = 1.0f;
            Assert.IsFalse(sizeEventTriggered);
            Assert.IsFalse(positionEventTriggered);

            testObj.XPosition = 1.0f;
            Assert.IsFalse(positionEventTriggered);
            Assert.IsFalse(sizeEventTriggered);

            testObj.YPosition = 1.0f;
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

            Assert.AreEqual(DefaultValue, testObj.XPosition);
            Assert.AreEqual(DefaultValue, testObj.YPosition);
            Assert.AreEqual(DefaultValue, testObj.Width);
            Assert.AreEqual(DefaultValue, testObj.Height);

            testObj.XPosition = TestValueX;
            Assert.AreEqual(TestValueX, testObj.XPosition);
            Assert.AreEqual(DefaultValue, testObj.YPosition);
            Assert.AreEqual(DefaultValue, testObj.Width);
            Assert.AreEqual(DefaultValue, testObj.Height);

            testObj.YPosition = TestValueY;
            Assert.AreEqual(TestValueX, testObj.XPosition);
            Assert.AreEqual(TestValueY, testObj.YPosition);
            Assert.AreEqual(DefaultValue, testObj.Width);
            Assert.AreEqual(DefaultValue, testObj.Height);

            testObj.Width = TestValueWidth;
            Assert.AreEqual(TestValueX, testObj.XPosition);
            Assert.AreEqual(TestValueY, testObj.YPosition);
            Assert.AreEqual(TestValueWidth, testObj.Width);
            Assert.AreEqual(DefaultValue, testObj.Height);

            testObj.Height = TestValueHeight;
            Assert.AreEqual(TestValueX, testObj.XPosition);
            Assert.AreEqual(TestValueY, testObj.YPosition);
            Assert.AreEqual(TestValueWidth, testObj.Width);
            Assert.AreEqual(TestValueHeight, testObj.Height);
        }

        public static List<TestCaseData> ValidCoordinates = new List<TestCaseData>{
            new TestCaseData(5.0f, 5.0f, 150.0f, 10.0f ),
            new TestCaseData( 0.0f, 0.0f, 150.0f, 10.0f ),
            new TestCaseData( 0.0f, 0.0f, 0.0f, 10.0f ),
            new TestCaseData( 0.0f, 0.0f, 150.0f, 0.0f),
            new TestCaseData( 0.0f, 0.0f, 50.0f, 150.0f)
        };

        [Test, TestCaseSource(typeof(IGraphicComponentTest), "ValidCoordinates")]
        public void DrawInConstraintsTest(float X, float Y, float Width, float Height)
        {
            SpriteBatchMock batch = new SpriteBatchMock();

            testObj.XPosition = X;
            testObj.YPosition = Y;
            testObj.Width = Width;
            testObj.Height = Height;

            testObj.Draw(new GameTime(), batch);

            foreach (var obj in batch.Objects)
                Assert.IsTrue(obj.IsInConstraints(testObj));
        }
    }
}
