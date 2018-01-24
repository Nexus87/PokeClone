using System;
using System.Linq;
using GameEngine.Globals;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    internal class GridCell
    {
        private readonly int _row;
        private readonly int _column;
        private readonly Table<GridCell> _cells;
        public EventHandler PreferredSizeChanged;
        private IGuiComponent _guiComponent;

        private Rectangle _contraints;
        private int _columnSpan;
        private int _rowSpan;

        public GridCell(int row, int column, Table<GridCell> cells)
        {
            _row = row;
            _column = column;
            _cells = cells;
        }

        public IGuiComponent GuiComponent
        {
            get { return _guiComponent; }
            set
            {
                if (_guiComponent != null)
                    _guiComponent.PreferredSizeChanged -= PreferredSizeChangedHandler;

                _guiComponent = value;
                if (value != null)
                    value.PreferredSizeChanged += PreferredSizeChangedHandler;
            }
        }

        private void PreferredSizeChangedHandler(object sender, GraphicComponentSizeChangedEventArgs e)
        {
            PreferredSizeChanged?.Invoke(this, e);
        }

        public void SetConstraints(Rectangle constraints, Rectangle gridConstraints)
        {
            _contraints = constraints;
        }

        public float PreferedWidth => GuiComponent?.PreferredWidth ?? 0;
        public float PreferedHeight => GuiComponent?.PreferredHeight ?? 0;

        public bool IsSelectable => GuiComponent?.IsSelectable ?? false;

        public bool IsSelected
        {
            get { return GuiComponent.IsSelected; }
            set { GuiComponent.IsSelected = value; }
        }

        public void ApplySizeToComponent()
        {
            if(GuiComponent == null)
                return;

            var totalHeight = _cells.EnumerateRows(_column).Skip(_row).Take(_rowSpan).Sum(x => x._contraints.Height);
            var totalWidth = _cells.EnumerateColumns(_row).Skip(_column).Take(_columnSpan).Sum(x => x._contraints.Width);

            GuiComponent.Area = new Rectangle(_contraints.X, _contraints.Y, totalWidth, totalHeight);
        }

        public void SetComponent(IGuiComponent component, int rowSpan, int columnSpan)
        {
            GuiComponent = component;
            _rowSpan = rowSpan;
            _columnSpan = columnSpan;
        }
    }
}