using NUnit.Framework;
using System;
using GameEngine.Core;
using GameEngine.Entities;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace GameEngineTest.TestUtils
{
    public static class Extensions
    {
        public static void Draw(this GuiManager manager)
        {
            manager.Draw(new GameTime(), new SpriteBatchMock());
        }
        public static void Draw(this IGuiComponent component, ISpriteBatch spriteBatch)
        {
            component.Draw(new GameTime(), spriteBatch);
        }

        public static void Draw(this IGuiComponent component)
        {
            component.Draw(new GameTime(), new SpriteBatchMock());
        }

        public static void Update(this IGameEntity entity)
        {
            entity.Update(new GameTime());
        }

        public static void IsInConstraints(this IGuiComponent component, float x, float y, float width, float height)
        {
            var realWidth = Math.Max(0, width);
            var realHeight = Math.Max(0, height);
            var ret = true;
            ret &= (realWidth.CompareTo(0) == 0) || (component.Width().CompareTo(0) == 0) ||
                (component.XPosition().CompareTo(x) >= 0 && component.XPosition().CompareTo(x + realWidth) <= 0);
            Assert.IsTrue(ret);
            ret &= (realHeight.CompareTo(0) == 0) || (component.Width().CompareTo(0) == 0) ||
                (component.YPosition().CompareTo(y) >= 0 && component.YPosition().CompareTo(y + realHeight) <= 0);
            Assert.IsTrue(ret);

            ret &= component.Width().CompareTo(realWidth) <= 0;
            Assert.IsTrue(ret);
            ret &= component.Height().CompareTo(realHeight) <= 0;
            Assert.IsTrue(ret);
        }

        public static void IsInConstraints(this IGuiComponent component, IGuiComponent other)
        {
            component.IsInConstraints(other.XPosition(), other.YPosition(), other.Width(), other.Height());
        }
        public static float YPosition(this IGuiComponent component)
        {
            return component.Area.Y;
        }

        public static float XPosition(this IGuiComponent component)
        {
            return component.Area.X;
        }

        public static float Width(this IGuiComponent component)
        {
            return component.Area.Width;
        }

        public static float Height(this IGuiComponent component)
        {
            return component.Area.Height;
        }

        public static void YPosition(this IGuiComponent component, float value)
        {
            component.Area = new Rectangle(component.Area.X, (int) value, component.Area.Width, component.Area.Height);
        }

        public static void XPosition(this IGuiComponent component, float value)
        {
            component.Area = new Rectangle((int) value, component.Area.Y, component.Area.Width, component.Area.Height);
        }

        public static void Width(this IGuiComponent component, float value)
        {
            component.Area = new Rectangle(component.Area.X, component.Area.Y, (int) value, component.Area.Height);
        }

        public static void Height(this IGuiComponent component, float value)
        {
            component.Area = new Rectangle(component.Area.X, component.Area.Y, component.Area.Width, (int) value);
        }
    }
}
