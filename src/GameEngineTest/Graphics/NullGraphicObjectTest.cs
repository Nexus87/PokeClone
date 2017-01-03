using GameEngine.GUI;
using GameEngine.GUI.Graphics;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class NullGraphicObjectTest : IGraphicComponentTest
    {
        protected override IGuiComponent CreateComponent()
        {
            return new NullGuiObject();
        }
    }
}
