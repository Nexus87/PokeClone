using GameEngine.Globals;

namespace GameEngine.GUI.Graphics.GUI
{
    public abstract class AbstractWidget : AbstractGraphicComponent, IWidget
    {

        public abstract bool HandleInput(CommandKeys key);
    }
}