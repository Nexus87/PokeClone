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
        }

        [TestCase]
        public void MultipleComponentsTest()
        {
            var batch = new SpriteBatchMock();
            testContainer.FillContainer(10);
            testLayout.LayoutContainer(testContainer);

            testContainer.Draw(batch);
            Assert.AreEqual(1, batch.Objects);
            batch.Objects[0].IsInConstraints(testContainer);
        }
    }
}
