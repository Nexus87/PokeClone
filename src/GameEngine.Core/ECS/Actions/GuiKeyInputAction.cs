using GameEngine.Globals;

namespace GameEngine.Core.ECS.Actions
{
    public class GuiKeyInputAction
    {
        public readonly CommandKeys Key;

        public GuiKeyInputAction(CommandKeys key)
        {
            Key = key;
        }
    }
}