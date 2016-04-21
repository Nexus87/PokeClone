﻿using GameEngine.Graphics;
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
        [TestCase]
        public void MultipleComponentsTest()
        {
            var layout = CreateLayout();
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10);
            testContainer.SetCoordinates(10.0f, 10.0f, 50.0f, 50.0f);

            layout.LayoutContainer(testContainer);

            var Objects = from obj in components where obj.Width.CompareTo(0) != 0 && obj.Height.CompareTo(0) != 0 select obj;
            Assert.AreEqual(1, Objects.Count());
            Objects.First().IsInConstraints(testContainer);
        }

        protected override ILayout CreateLayout()
        {
            return new SingleComponentLayout();
        }
    }
}
