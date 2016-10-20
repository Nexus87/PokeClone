namespace GameEngine.Graphics.NewGUI
{
    public static class Layout
    {
        public const double UseComputedSize = -1;
    }

    public interface IArea
    {
        double X { get; set; }
        double Y { get; set; }
        double Width { get; set; }
        double Height { get; set; }

        double CalculatePreferedWidth();
        double CalculatePreferedHeight();

        double ParentWidthConstraint { get; set; }
        double ParentHeightConstraint { get; set; }

    }
}