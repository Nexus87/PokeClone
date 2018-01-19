namespace GameEngine.Core.ECS.Actions
{
    public class SetGuiVisibleAction
    {

        public readonly bool IsVisible;

        public SetGuiVisibleAction(bool isVisible)
        {
            IsVisible = isVisible;
        }
    }
}