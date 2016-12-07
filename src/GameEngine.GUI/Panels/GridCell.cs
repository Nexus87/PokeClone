using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    internal class GridCell
    {
        public IGuiComponent GuiComponent { get; set; }

        public void SetConstraints(Rectangle constraints, Rectangle gridConstraints)
        {
            if(GuiComponent == null)
                return;
            GuiComponent.Constraints = constraints;
            GuiComponent.ScissorArea = GuiComponent.ScissorArea = Rectangle.Intersect(constraints, gridConstraints);
        }
        public float PreferedWidth => GuiComponent?.PreferedWidth ?? 0;
        public float PreferedHeight => GuiComponent?.PreferedHeight ?? 0;
        public bool IsSelectable => GuiComponent?.IsSelectable ?? false;
        public bool IsSelected {
            get
            {
                return GuiComponent.IsSelected;

            }
            set
            {
                GuiComponent.IsSelected = value;
            }
        }
    }
}