using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
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
    public abstract class ILayoutTest
    {
        public ILayout testLayout;
        public Container testContainer;

        public Mock<PokeEngine> engineMock = new Mock<PokeEngine>();

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
            testContainer.SetCoordinates(X, Y, Width, Height);

            testLayout.LayoutContainer(testContainer);
            testContainer.Draw(batch);

            foreach (var obj in batch.Objects)
                obj.IsInConstraints(testContainer);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void MarginTest(float X, float Y, float Width, float Height)
        {
            int Margin = 10;
            SpriteBatchMock batch = new SpriteBatchMock();

            testContainer.SetCoordinates(X, Y, Width, Height);
            testLayout.LayoutContainer(testContainer);

            testLayout.SetMargin(left: Margin);
            testLayout.LayoutContainer(testContainer);

            testContainer.Draw(batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X + Margin, Y, Width - Margin, Height);
            batch.Objects.Clear();

            testLayout.SetMargin(right: Margin);
            testLayout.LayoutContainer(testContainer);

            testContainer.Draw(batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y, Width - Margin, Height);
            batch.Objects.Clear();

            testLayout.SetMargin(top: Margin);
            testLayout.LayoutContainer(testContainer);

            testContainer.Draw(batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y + Margin, Width, Height - Margin);
            batch.Objects.Clear();

            testLayout.SetMargin(bottom: Margin);
            testLayout.LayoutContainer(testContainer);

            testContainer.Draw(batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X, Y, Width, Height - Margin);
            batch.Objects.Clear();

            testLayout.SetMargin(Margin, Margin, Margin, Margin); 
            testLayout.LayoutContainer(testContainer);

            testContainer.Draw(batch);
            foreach (var obj in batch.Objects)
                obj.IsInConstraints(X + Margin, Y + Margin, Width - 2*Margin, Height - 2*Margin);
            batch.Objects.Clear();


        }
    }
}
