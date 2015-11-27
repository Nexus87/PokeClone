using GameEngine.Graphics.Basic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Basic
{
    [TestFixture]
    public class LineTest : IGraphicComponentTest
    {
        [SetUp]
        public void Setup()
        {
            var line = new Line();
        }
    }
}
