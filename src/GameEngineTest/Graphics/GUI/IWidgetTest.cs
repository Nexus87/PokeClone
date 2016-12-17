using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;

namespace GameEngineTest.Graphics.GUI
{
    public abstract class IWidgetTest : IGraphicComponentTest
    {
        protected abstract IWidget CreateWidget();

        protected override IGraphicComponent CreateComponent()
        {
            return CreateWidget();
        }
    }
}
