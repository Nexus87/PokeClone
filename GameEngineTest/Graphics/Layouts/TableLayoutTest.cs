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

            IGraphicComponent[,] component = new IGraphicComponent[2, 2];
            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                    component[i, j] = layout.GetComponent(i, j);
            }

            layout.SetComponent(2, 1, new Mock<IGraphicComponent>().Object);

            Assert.AreEqual(3, layout.Rows);
            Assert.AreEqual(2, layout.Columns);

            layout.SetComponent(2, 3, new Mock<IGraphicComponent>().Object);

            Assert.AreEqual(3, layout.Rows);
            Assert.AreEqual(4, layout.Columns);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                   Assert.AreEqual(component[i, j], layout.GetComponent(i, j));
            }
        }
        
        [TestCase]
        public void NullComponentTest()
        {
            var spriteBatch = new SpriteBatchMock();
            var tableLayout = new TableLayout(5, 5);
            var component = new Mock<IGraphicComponent>();
            var serviceMock = new Mock<IServiceProvider>();
            var contentMock = new Mock<ContentManager>(serviceMock.Object);
            component.SetCoordinates(0.0f, 00.0f, 250.0f, 250.0f);

            tableLayout.Init(component.Object);
            tableLayout.Setup(contentMock.Object);
            tableLayout.Draw(spriteBatch);

            Assert.AreEqual(0, spriteBatch.Objects.Count);

            spriteBatch.Objects.Clear();
            tableLayout.SetComponent(4, 4, new Mock<AbstractGraphicComponent>().Object);
            tableLayout.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.Objects.Count);
            Assert.AreEqual(50.0f, spriteBatch.Objects[0].Size.X);
            Assert.AreEqual(50.0f, spriteBatch.Objects[0].Size.Y);
            Assert.AreEqual(4*50.0f, spriteBatch.Objects[0].Position.X);
            Assert.AreEqual(4 * 50.0f, spriteBatch.Objects[0].Position.Y);
        }
    }
}
