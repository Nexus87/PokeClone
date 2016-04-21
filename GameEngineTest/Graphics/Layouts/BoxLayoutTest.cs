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
using GameEngine.Graphics.Basic;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class VBoxLayoutTest : ILayoutTest
    {
        [TestCase(4, 0, 0, 200, 200)]
        public void PositionTest(int cnt, float x, float y, float width, float height)
        {
            var layout = new VBoxLayout();
            var container = new Container(engineMock.Object);
            var components = container.SetupContainer(cnt);
            layout.SetMargin();
            container.SetCoordinates(x, y, width, height);

            layout.LayoutContainer(container);
            
            for (int i = 0; i < components.Count; i++ )
            {
                var comp = components[i];
                Assert.AreEqual(x, comp.XPosition);
                Assert.AreEqual((height / cnt) * i, comp.YPosition);
                Assert.AreEqual(width, comp.Width);
                Assert.AreEqual(height / cnt, comp.Height);
            }
        }

        protected override ILayout CreateLayout()
        {
            return new VBoxLayout();
        }
    }

    [TestFixture]
    public class HBoxLayoutTest : ILayoutTest
    {


        [TestCase(4, 0, 0, 200, 200)]
        public void PositionTest(int cnt, float x, float y, float width, float height)
        {
            var layout = new HBoxLayout();
            var container = new Container(engineMock.Object);
            var components = container.SetupContainer(cnt);
            layout.SetMargin();
            container.SetCoordinates(x, y, width, height);

            layout.LayoutContainer(container);


            for (int i = 0; i < components.Count; i++)
            {
                var comp = components[i];
                Assert.AreEqual((width / cnt) * i, comp.XPosition);
                Assert.AreEqual(y, comp.YPosition);
                Assert.AreEqual(width / cnt, comp.Width);
                Assert.AreEqual(height, comp.Height);
            }
        }

        protected override ILayout CreateLayout()
        {
            return new HBoxLayout();
        }
    }
}
