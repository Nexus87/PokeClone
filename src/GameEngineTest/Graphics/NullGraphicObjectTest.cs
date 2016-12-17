using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class NullGraphicObjectTest : IGraphicComponentTest
    {
        protected override IGraphicComponent CreateComponent()
        {
            return new NullGraphicObject();
        }
    }
}
