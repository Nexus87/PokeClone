using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class NullGraphicObjectTest : IGraphicComponentTest
    {
        protected override IGuiComponent CreateComponent()
        {
            return new Spacer();
        }
    }
}
