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
    public class NullGraphicObjectTest : IGraphicComponentTest
    {
        protected override GameEngine.Graphics.IGraphicComponent CreateComponent()
        {
            return new NullGraphicObject(gameMock.Object);
        }
    }
}
