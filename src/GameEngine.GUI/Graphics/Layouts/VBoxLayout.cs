namespace GameEngine.Graphics.Layouts
{
    public class VBoxLayout : BoxLayout
    {
        protected override void UpdateComponents(Container container)
        {
            SetComponents(container.Components);

            HeightModifier = c => c.Height;
            WidthModifier = c => 0;
            WidthForExtendingComponents = Width;
            HeightForExtendingComponents = CalculateHeightForExtendingComponents();

            LayoutComponents();
        }
    }
}