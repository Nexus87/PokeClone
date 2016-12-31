using System;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngineTest.TestUtils
{
    public class DrawnObject
    {
        public Vector2 Position;
        public Vector2 Size;

        public Color Color;
        public void IsInConstraints(float x, float y, float width, float height)
        {
            var realWidth = Math.Max(0, width);
            var realHeight = Math.Max(0, height);

            if (Size.X.AlmostEqual(0) || Size.Y.AlmostEqual(0))
                return;

            Assert.GreaterOrEqual(Position.X, x);
            Assert.GreaterOrEqual(Position.Y, y);

            Assert.LessOrEqual(Position.X, x + realWidth);
            Assert.LessOrEqual(Position.Y, y + realHeight);

            Assert.LessOrEqual(Position.X + Size.X, x + realWidth);
            Assert.LessOrEqual(Position.Y + Size.Y, y + realHeight);
        }

        public void IsInConstraints(IGraphicComponent component)
        {
            IsInConstraints(component.XPosition(), component.YPosition(), component.Width(), component.Height());
        }
    }
}