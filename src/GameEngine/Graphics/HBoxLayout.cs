namespace GameEngine.Graphics
{
    public class HBoxLayout : BoxLayout
    {
        protected override void UpdateComponents(Container container)
        {
            SetComponents(container.Components);
            HeightModifier = c => 0;
            WidthModifier = c => c.Width;
            HeightForExtendingComponents = Height;
            WidthForExtendingComponents = CalculateWidthForExtendingComponents();

            LayoutComponents();
        }
    }
}