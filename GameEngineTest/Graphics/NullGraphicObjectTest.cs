using GameEngine.Graphics;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class NullGraphicObjectTest : IGraphicComponentTest
    {
        protected override GameEngine.Graphics.IGraphicComponent CreateComponent()
        {
            return new NullGraphicObject();
        }
    }
}
