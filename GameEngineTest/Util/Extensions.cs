using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moq;
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

        public static void SetCoordinates(this Container container, float X, float Y, float Width, float Height)
        {
            container.XPosition = X;
            container.YPosition = Y;
            container.Width = Width;
            container.Height = Height;
        }

        public static void Draw(this IGraphicComponent component, ISpriteBatch spriteBatch)
        {
            component.Draw(new GameTime(), spriteBatch);
        }

        public static void FillContainer(this Container container, int number)
        {
            for (int i = 0; i < number; i++)
                container.AddComponent(new TestGraphicComponent());
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
        public static void SetupLoad(this Mock<ContentManager> contentMock)
        {
            var dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
            contentMock.Setup(o => o.Load<Texture2D>(It.IsAny<string>())).Returns(new Texture2D(dev, 10, 10));
        }

        public static void SetupMeasureString(this Mock<ISpriteFont> fontMock)
        {
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
        }
    }
}
