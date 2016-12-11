using GameEngine.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    internal class GridCell
    {
        public IGraphicComponent GuiComponent { get; set; }

        public void SetConstraints(Rectangle constraints, Rectangle gridConstraints)
        {
            if(GuiComponent == null)
                return;
            GuiComponent.Area = constraints;
            GuiComponent.ScissorArea = GuiComponent.ScissorArea = Rectangle.Intersect(constraints, gridConstraints);
        }
        public float PreferedWidth => GuiComponent?.PreferredWidth ?? 0;
        public float PreferedHeight => GuiComponent?.PreferredHeight ?? 0;
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