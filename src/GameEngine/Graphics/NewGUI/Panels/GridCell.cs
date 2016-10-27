using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI.Panels
{
    internal class GridCell
    {
        public GridCell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public IGraphicComponent GraphicComponent { get; set; }
        public int Row { get; }
        public int Column { get; }

        public void SetConstraints(Rectangle constraints)
        {
            if(GraphicComponent == null)
                return;
            GraphicComponent.Constraints = constraints;
            GraphicComponent.ScissorArea = GraphicComponent.ScissorArea = constraints;
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