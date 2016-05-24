using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameEngineTest.TestUtils
{
    public static class Extensions
    {
        public static void Draw(this GUIManager manager)
        {
            manager.Draw(new GameTime(), new SpriteBatchMock());
        }
        public static void Draw(this IGraphicComponent component, ISpriteBatch spriteBatch)
        {
            component.Draw(new GameTime(), spriteBatch);
        }

        public static void Draw(this IGraphicComponent component)
        {
            component.Draw(new GameTime(), new SpriteBatchMock());
        }

        public static void Update(this GameEngine.IGameComponent component)
        {
            component.Update(new GameTime());
        }

        public static List<GraphicComponentMock> SetupContainer(this Container container, int number, float initialSize = 30.0f)
        {
            var ret = new List<GraphicComponentMock>();

            for(int i = 0; i < number; i++)
            {
                var comp = new GraphicComponentMock();
                comp.Width = initialSize;
                comp.Height = initialSize;
                ret.Add(comp);
                container.AddComponent(comp);
            }

            return ret;
        }

        public static void IsInConstraints(this IGraphicComponent component, float X, float Y, float Width, float Height)
        {
            float realWidth = Math.Max(0, Width);
            float realHeight = Math.Max(0, Height);
            bool ret = true;
            ret &= (realWidth.CompareTo(0) == 0) || (component.Width.CompareTo(0) == 0) ||
                (component.XPosition.CompareTo(X) >= 0 && component.XPosition.CompareTo(X + realWidth) <= 0);
            Assert.IsTrue(ret);
            ret &= (realHeight.CompareTo(0) == 0) || (component.Width.CompareTo(0) == 0) ||
                (component.YPosition.CompareTo(Y) >= 0 && component.YPosition.CompareTo(Y + realHeight) <= 0);
            Assert.IsTrue(ret);

            ret &= component.Width.CompareTo(realWidth) <= 0;
            Assert.IsTrue(ret);
            ret &= component.Height.CompareTo(realHeight) <= 0;
            Assert.IsTrue(ret);
        }

        public static void IsInConstraints(this IGraphicComponent component, IGraphicComponent other)
        {
            component.IsInConstraints(other.XPosition, other.YPosition, other.Width, other.Height);
        }
    }
}
