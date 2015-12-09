using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using Moq;
using GameEngineTest.Util;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class VBoxLayoutTest : ILayoutTest
    {
        VBoxLayout layout;

        [SetUp]
        public void Setup()
        {
            
            layout = new VBoxLayout();
            for (int i = 0; i < 4; i++)
            {
                var mock = new GraphicComponentMock();
                layout.AddComponent(mock);
            }

            testLayout = layout;
        }

        [TestCase]
        public void PositionTest()
        {
            var spriteBatch = new SpriteBatchMock();
            var compMock = new Mock<IGraphicComponent>();
            layout.SetMargin();
            compMock.SetCoordinates(0.0f, 0.0f, 200.0f, 200.0f);

            layout.Init(compMock.Object);
            layout.Draw(spriteBatch);

            spriteBatch.Objects.Sort(delegate(DrawnObject a, DrawnObject b) { return a.Position.Y.CompareTo(b.Position.Y); });

            for (int i = 0; i < spriteBatch.Objects.Count; i++ )
            {
                var obj = spriteBatch.Objects[i];
                Assert.AreEqual(0.0f, obj.Position.X);
                Assert.AreEqual(50.0f * i, obj.Position.Y);
                Assert.AreEqual(200.0f, obj.Size.X);
                Assert.AreEqual(50.0f, obj.Size.Y);
            }
        }
    }

    [TestFixture]
    public class HBoxLayoutTest : ILayoutTest
    {
        HBoxLayout layout;

        [SetUp]
        public void Setup()
        {

            layout = new HBoxLayout();
            for (int i = 0; i < 4; i++)
                layout.AddComponent(new GraphicComponentMock());

            testLayout = layout;
        }

        [TestCase]
        public void PositionTest()
        {
            var spriteBatch = new SpriteBatchMock();
            var compMock = new Mock<IGraphicComponent>();
            layout.SetMargin();
            compMock.SetCoordinates(0.0f, 0.0f, 200.0f, 200.0f);

            layout.Init(compMock.Object);
            layout.Draw(spriteBatch);

            spriteBatch.Objects.Sort(delegate(DrawnObject a, DrawnObject b) { return a.Position.X.CompareTo(b.Position.X); });

            for (int i = 0; i < spriteBatch.Objects.Count; i++)
            {
                var obj = spriteBatch.Objects[i];
                Assert.AreEqual(50.0f * i, obj.Position.X);
                Assert.AreEqual(0.0f, obj.Position.Y);
                Assert.AreEqual(50.0f, obj.Size.X);
                Assert.AreEqual(200.0f, obj.Size.Y);
            }
        }
    }
}
