using GameEngine.Graphics.Basic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Basic
{
    public class NullGraphicObjectTest : IGraphicComponentTest
    {
        [SetUp]
        public void Setup()
        {
            testObj = new NullGraphicObject();
        }
    }
}
