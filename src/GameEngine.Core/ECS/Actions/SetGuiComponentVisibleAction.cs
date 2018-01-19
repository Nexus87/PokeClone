using GameEngine.GUI;

namespace GameEngine.Core.ECS.Actions
{
    public class SetGuiComponentVisibleAction
    {
        public readonly IGuiComponent Widget;
        public readonly bool IsVisble;

        public readonly int? Priority;

        public SetGuiComponentVisibleAction(IGuiComponent widget, bool isVisble, int? priority = null)
        {
            Widget = widget;
            IsVisble = isVisble;
            Priority = priority;
        }
    }
}