using System;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngineTest.TestUtils
{
    public class DrawnObject
    {
        public Vector2 Position;
        public Vector2 Size;

        public Color Color;
        public void IsInConstraints(float X, float Y, float Width, float Height)
        {
            float realWidth = Math.Max(0, Width);
            float realHeight = Math.Max(0, Height);

            if (Size.X.AlmostEqual(0) || Size.Y.AlmostEqual(0))
                return;

            Assert.GreaterOrEqual(Position.X, X);
            Assert.GreaterOrEqual(Position.Y, Y);

            Assert.LessOrEqual(Position.X, X + realWidth);
            Assert.LessOrEqual(Position.Y, Y + realHeight);

            Assert.LessOrEqual(Position.X + Size.X, X + realWidth);
            Assert.LessOrEqual(Position.Y + Size.Y, Y + realHeight);
        }

        public void IsInConstraints(IGraphicComponent component)
        {
            IsInConstraints(component.XPosition(), component.YPosition(), component.Width(), component.Height());
        }
    }
}