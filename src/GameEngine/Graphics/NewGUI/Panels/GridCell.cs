using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI.Panels
{
    internal class GridCell
    {
        public IGraphicComponent GraphicComponent { get; set; }

        public void SetConstraints(Rectangle constraints, Rectangle gridConstraints)
        {
            if(GraphicComponent == null)
                return;
            GraphicComponent.Constraints = constraints;
            GraphicComponent.ScissorArea = GraphicComponent.ScissorArea = Rectangle.Intersect(constraints, gridConstraints);
        }
        public float PreferedWidth => GraphicComponent?.PreferedWidth ?? 0;
        public float PreferedHeight => GraphicComponent?.PreferedHeight ?? 0;
        public bool IsSelectable => GraphicComponent?.IsSelectable ?? false;
        public bool IsSelected {
            get
            {
                return GraphicComponent.IsSelected;

            }
            set
            {
                GraphicComponent.IsSelected = value;
            }
        }
    }
}