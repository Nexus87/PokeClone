using GameEngine.Graphics.Basic;
using GameEngine.Graphics;
using GameEngine.Utils;
using GameEngineTest.Util;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Basic
{
    [TestFixture]
    public class MultiLayeredComponentTest : IGraphicComponentTest
    {
        private MultiLayeredComponent testComponent;

        [SetUp]
        public void Setup()
        {
            
            testComponent = new MultiLayeredComponent(new TextureBox(gameMock.Object), gameMock.Object);
            testComponent.Background = new TextureBox(gameMock.Object);
            testComponent.Foreground = new TextureBox(gameMock.Object);
            testComponent.Setup(contentMock.Object);

            testObj = testComponent;
        }

        [TestCase]
        public void NullTest()
        {
            var batch = new SpriteBatchMock();
            testComponent.SetCoordinates(100, 100, 500, 500);

            testComponent.Background = null;
            testComponent.Draw(batch);

            foreach (var o in batch.Objects)
                o.IsInConstraints(testComponent);

            batch.Objects.Clear();
            testComponent.Foreground = null;
            testComponent.Draw(batch);

            foreach (var o in batch.Objects)
                o.IsInConstraints(testComponent);

            Assert.Throws(typeof(ArgumentNullException), delegate { testComponent.MainComponent = null; });
        }
    }
}
