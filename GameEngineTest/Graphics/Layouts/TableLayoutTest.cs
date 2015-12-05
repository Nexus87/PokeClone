using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngineTest.Util;
using Microsoft.Xna.Framework.Content;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class TableLayoutTest : ILayoutTest
    {
        TableLayout layout;

        [SetUp]
        public void Setup()
        {
            layout = new TableLayout(2, 2);
            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                    layout.SetComponent(i, j, new Mock<IGraphicComponent>().Object);
            }

            testLayout = layout;
        }

        [TestCase]
        public void GrowingLayoutTest()
        {
            Assert.AreEqual(2, layout.Rows);
            Assert.AreEqual(2, layout.Columns);

            layout.SetComponent(3, 2, new Mock<IGraphicComponent>().Object);

            Assert.AreEqual(3, layout.Rows);
            Assert.AreEqual(2, layout.Columns);

            layout.SetComponent(3, 4, new Mock<IGraphicComponent>().Object);

            Assert.AreEqual(3, layout.Rows);
            Assert.AreEqual(4, layout.Columns);
        }

        [TestCase]
        public void NullComponentTest()
        {
            var spriteBatch = new SpriteBatchMock();
            var tableLayout = new TableLayout(5, 5);
            var component = new Mock<IGraphicComponent>();
            var serviceMock = new Mock<IServiceProvider>();
            var contentMock = new Mock<ContentManager>(serviceMock.Object);
            component.SetCoordinates(10.0f, 10.0f, 250.0f, 250.0f);

            layout.Init(component.Object);
            layout.Setup(contentMock.Object);
            layout.Draw(spriteBatch);

            Assert.AreEqual(0, spriteBatch.Objects.Count);
        }
    }
}
