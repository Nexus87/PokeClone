using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
{
    public static class Extensions
    {
        public static void SetCoordinates(this Mock<IGraphicComponent> compMock, float X, float Y, float Width, float Height)
        {
            compMock.Setup(o => o.XPosition).Returns(X);
            compMock.Setup(o => o.YPosition).Returns(Y);
            compMock.Setup(o => o.Width).Returns(Width);
            compMock.Setup(o => o.Height).Returns(Height);
        }

        public static void Draw(this IGraphicComponent component, ISpriteBatch spriteBatch)
        {
            component.Draw(new GameTime(), spriteBatch);
        }

        public static List<GraphicComponentMock> SetupContainer(this Container container, int number, float initialSize = 0)
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

        public static void ClearContainer(this Container container)
        {
            var list = container.Components;
            while (list.Count != 0)
            {
                container.RemoveComponent(list[0]);
                list = container.Components;
            }
        }

        public static void SetupMeasureString(this Mock<ISpriteFont> fontMock)
        {
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
        }

        public static void SetupModelMock<T>(this Mock<ITableModel<T>> model, int rows, int columns, T data)
        {
            model.Setup(o => o.Rows).Returns(rows);
            model.Setup(o => o.Columns).Returns(columns);
            model.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>())).Returns(data);
        }

        public static void SetupModelMock<T>(this Mock<ITableModel<T>> model, int rows, int columns, Func<int, int, T> retFunc)
        {
            model.Setup(o => o.Rows).Returns(rows);
            model.Setup(o => o.Columns).Returns(columns);
            model.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>())).Returns((int a, int b) => retFunc(a, b));
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
