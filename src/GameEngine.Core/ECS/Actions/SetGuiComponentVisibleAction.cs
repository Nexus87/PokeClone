using GameEngine.GUI;

namespace GameEngine.Core.ECS.Actions
{
    public class SetGuiComponentVisibleAction
    {
        public IGuiComponent Widget { get; set; }
        public bool IsVisble { get; set; }

        public int? Priority { get; set; }
    }
}