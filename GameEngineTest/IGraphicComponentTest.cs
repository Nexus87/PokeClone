using GameEngine.Graphics;
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
    }
}
