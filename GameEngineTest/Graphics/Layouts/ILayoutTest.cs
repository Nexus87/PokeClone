using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngineTest.Util;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Layouts
{
    public abstract class ILayoutTest
    {
        public ILayout layout;

        [TestCase]
        public void DrawInConstraintsTest()
        {
            float X = 1.0f;
            float Y = 1.0f;
            float Width = 30.0f;
            float Height = 50.0f;
            SpriteBatchMock batch = new SpriteBatchMock();
            var compMock = new Mock<IGraphicComponent>();

            compMock.Setup(o => o.X).Returns(X);
            compMock.Setup(o => o.Y).Returns(Y);
            compMock.Setup(o => o.Width).Returns(Width);
            compMock.Setup(o => o.Height).Returns(Height);

            layout.Draw(new GameTime(), batch);

            foreach (var obj in batch.Objects)
                obj.IsInConstraints(compMock.Object);
        }

        [TestCase]
        public void MarginTest()
        {
            float X = 1.0f;
            float Y = 1.0f;
            float Width = 30.0f;
            float Height = 50.0f;
            int Margin = 10;
            SpriteBatchMock batch = new SpriteBatchMock();
            var compMock = new Mock<IGraphicComponent>();

            compMock.Setup(o => o.X).Returns(X);
            compMock.Setup(o => o.Y).Returns(Y);
            compMock.Setup(o => o.Width).Returns(Width);
            compMock.Setup(o => o.Height).Returns(Height);


            layout.SetMargin(left: Margin);
            layout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X + Margin, Y, Width - Margin, Height);
            batch.Objects.Clear();

            layout.SetMargin(right: Margin);
            layout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y, Width - Margin, Height);
            batch.Objects.Clear();

            layout.SetMargin(top: Margin);
            layout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y + Margin, Width, Height - Margin);
            batch.Objects.Clear();

            layout.SetMargin(bottom: Margin);
            layout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y, Width, Height - Margin);
            batch.Objects.Clear();

            layout.SetMargin(Margin, Margin, Margin, Margin);
            layout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X + Margin, Y + Margin, Width - 2*Margin, Height - 2*Margin);
            batch.Objects.Clear();


        }
    }
}
