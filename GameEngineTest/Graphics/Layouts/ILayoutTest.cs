using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using GameEngineTest.Util;
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

namespace GameEngineTest.Graphics.Layouts
{
    public class GraphicComponentMock : AbstractGraphicComponent
    {
        Texture2D texture;
        public GraphicComponentMock()
            : base(new Mock<PokeEngine>(new Configuration()).Object)
        {
            var dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
            texture = new Texture2D(dev, 1, 1);
        }
        public override void Setup(ContentManager content)
        {
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            var batchMock = (SpriteBatchMock)batch;
            batch.Draw(texture: texture, position: Position, destinationRectangle: null, scale: Size);
        }
    }
    public abstract class ILayoutTest
    {
        public ILayout testLayout;

        public static List<TestCaseData> ValidData = new List<TestCaseData>
        {
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f),
            new TestCaseData(0.0f, 0.0f, 50.0f, 50.0f),
            new TestCaseData(0.0f, 0.0f, 0.0f, 50.0f),
            new TestCaseData(0.0f, 0.0f, 50.0f, 0.0f),
            new TestCaseData(0.0f, 0.0f, 0.0f, 0.0f),
            new TestCaseData(0.0f, 0.0f, 150.0f, 50.0f),
            new TestCaseData(0.0f, 0.0f, 50.0f, 150.0f)
        };

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void DrawInConstraintsTest(float X, float Y, float Width, float Height)
        {
            SpriteBatchMock batch = new SpriteBatchMock();
            var compMock = new Mock<IGraphicComponent>();
            compMock.SetCoordinates(X, Y, Width, Height);

            testLayout.Init(compMock.Object);
            testLayout.Draw(new GameTime(), batch);

            foreach (var obj in batch.Objects)
                obj.IsInConstraints(compMock.Object);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void MarginTest(float X, float Y, float Width, float Height)
        {
            int Margin = 10;
            SpriteBatchMock batch = new SpriteBatchMock();
            var compMock = new Mock<IGraphicComponent>();
            compMock.SetCoordinates(X, Y, Width, Height);
            testLayout.Init(compMock.Object);

            testLayout.SetMargin(left: Margin);
            testLayout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X + Margin, Y, Width - Margin, Height);
            batch.Objects.Clear();

            testLayout.SetMargin(right: Margin);
            testLayout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y, Width - Margin, Height);
            batch.Objects.Clear();

            testLayout.SetMargin(top: Margin);
            testLayout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y + Margin, Width, Height - Margin);
            batch.Objects.Clear();

            testLayout.SetMargin(bottom: Margin);
            testLayout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y, Width, Height - Margin);
            batch.Objects.Clear();

            testLayout.SetMargin(Margin, Margin, Margin, Margin);
            testLayout.Draw(new GameTime(), batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X + Margin, Y + Margin, Width - 2*Margin, Height - 2*Margin);
            batch.Objects.Clear();


        }
    }
}
