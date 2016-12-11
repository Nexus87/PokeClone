using GameEngine.Globals;

namespace GameEngine.Graphics.GUI
{
    public abstract class AbstractWidget : AbstractGraphicComponent, IWidget
    {

        public abstract bool HandleInput(CommandKeys key);
    }
}