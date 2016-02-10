using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineTest.Util;
using Microsoft.Xna.Framework;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class SingleComponentLayoutTest : ILayoutTest
    {
        SingleComponentLayout layout;

        [SetUp]
        public void Setup()
        {
            layout = new SingleComponentLayout();

            testLayout = layout;
            testContainer = new Container(engineMock.Object);
            testContainer.FillContainer(1);
            testContainer.Layout = layout;
        }

        [TestCase]
        public void MultipleComponentsTest()
        {
            var batch = new SpriteBatchMock();
            testContainer.FillContainer(10);
            testContainer.SetCoordinates(10.0f, 10.0f, 50.0f, 50.0f);

            testLayout.LayoutContainer(testContainer);

            testContainer.Draw(batch);
            var Objects = from obj in batch.Objects where obj.Size != Vector2.Zero select obj;
            Assert.AreEqual(1, Objects.Count());
            Objects.First().IsInConstraints(testContainer);
        }
    }
}
