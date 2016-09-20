using System;
using GameEngine.Graphics;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    internal class TestLayout : AbstractLayout
    {
        protected override void UpdateComponents(Container container) 
        {
        }

        public void TestProperties(float X, float Y, float Width, float Height)
        {
            // To make the test code easier, we allow negative size as input
            Width = Math.Max(0, Width);
            Height = Math.Max(0, Height);

            Assert.AreEqual(this.XPosition, X);
            Assert.AreEqual(this.YPosition, Y);
            Assert.AreEqual(this.Width, Width);
            Assert.AreEqual(this.Height, Height);
        }

    }
}