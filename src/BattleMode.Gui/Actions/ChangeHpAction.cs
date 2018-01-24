using GameEngine.Core.ECS;

namespace BattleMode.Gui.Actions
{
    public class ChangeHpAction
    {
        public readonly int Diff;
        public readonly Entity Target;

        public ChangeHpAction(int diff, Entity target)
        {
            Diff = diff;
            Target = target;
        }
    }
}