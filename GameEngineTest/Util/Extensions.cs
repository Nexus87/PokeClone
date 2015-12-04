using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
{
    static class Extensions
    {
        public static void SetCoordinates(this Mock<IGraphicComponent> compMock, float X, float Y, float Width, float Height)
        {
            compMock.Setup(o => o.X).Returns(X);
            compMock.Setup(o => o.Y).Returns(Y);
            compMock.Setup(o => o.Width).Returns(Width);
            compMock.Setup(o => o.Height).Returns(Height);
        }

        public static void Draw(this IGraphicComponent component, ISpriteBatch spriteBatch)
        {
            component.Draw(new GameTime(), spriteBatch);
        }

        public static void Draw(this ILayout layout, ISpriteBatch spriteBatch)
        {
            layout.Draw(new GameTime(), spriteBatch);
        }
    }
}
