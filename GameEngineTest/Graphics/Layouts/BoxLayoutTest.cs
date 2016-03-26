﻿using GameEngine.Graphics;
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
        VBoxLayout layout;

        [SetUp]
        public void Setup()
        {            
            layout = new VBoxLayout();
            testLayout = layout;
        }

        [TestCase(4, 0, 0, 200, 200)]
        public void PositionTest(int cnt, float x, float y, float width, float height)
        {
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
    }

    [TestFixture]
    public class HBoxLayoutTest : ILayoutTest
    {
        HBoxLayout layout;

        [SetUp]
        public void Setup()
        {

            layout = new HBoxLayout();
            testLayout = layout;
        }

        [TestCase(4, 0, 0, 200, 200)]
        public void PositionTest(int cnt, float x, float y, float width, float height)
        {
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
    }
}
