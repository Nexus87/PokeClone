using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Widgets
{
    public class TableWidget<T> : AbstractGraphicComponent, IWidget
    {
        private bool isVisible;

        public int? VisibleRows { get; set; }
        public int? VisibleColumns { get; set; }

        public TableWidget(PokeEngine game)
            : base(game)
        {
        }

        public event EventHandler<SelectionEventArgs<T>> ItemSelected = delegate { };
        public event EventHandler OnExitRequested = delegate { };
        public event EventHandler<VisibilityChangedEventArgs> OnVisibilityChanged = delegate { };
        private ITableView<T> tableView;
        private int lastSelectedRow = 0;
        private int lastSelectedColumns = 0;
        private int? visibleRows;

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value == isVisible)
                    return;

                isVisible = value;
                OnVisibilityChanged(this, new VisibilityChangedEventArgs(isVisible));
            }
        }

        public ITableView<T> TableView
        {
            get { return tableView; }
            internal set
            {
                value.CheckNull("value");
                tableView = value;
                Invalidate();
            }
        }


        public ITableModel<T> Model
        {
            get { return tableView.Model; }
            set { tableView.Model = value; }
        }

        public bool HandleInput(CommandKeys key)
        {
            throw new NotImplementedException();
        }

        protected override void Update()
        {
            tableView.XPosition = XPosition;
            tableView.YPosition = YPosition;
            tableView.Width = Width;
            tableView.Height = Height;
        }

        protected override void DrawComponent(Microsoft.Xna.Framework.GameTime time, ISpriteBatch batch)
        {
            tableView.Draw(time, batch);
        }

        public override void Setup(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void SelectCell(int row, int column)
        {
            lastSelectedRow = row;
            lastSelectedColumns = column;
            tableView.SetCellSelection(row, column, true);

            AdjustColumn(column);
            AdjustRows(row);
        }
        private void AdjustRows(int row)
        {
            if (VisibleRows == null || VisibleRows > tableView.Rows)
                return;

            int endRow = tableView.EndIndex == null ? tableView.Rows - 1 : tableView.EndIndex.Value.Row;
            int endColumn = tableView.EndIndex == null ? tableView.Columns - 1 : tableView.EndIndex.Value.Column;

            int startRow = tableView.StartIndex == null ? tableView.Rows - 1 : tableView.StartIndex.Value.Row;
            int startColumn = tableView.StartIndex == null ? tableView.Columns - 1 : tableView.StartIndex.Value.Column;


            if (row <= endRow && row >= startRow)
                return;

            var endIdx = new TableIndex(0, endColumn);
            var startIdx = new TableIndex(0, startColumn);

            if (row > endRow)
            {
                endIdx.Row = row;
                startIdx.Row = row - VisibleRows.Value + 1;
            }
            else if (row < startRow)
            {
                startIdx.Row = row;
                endIdx.Row = row + VisibleRows.Value - 1;
            }

            tableView.StartIndex = startIdx;
            tableView.EndIndex = endIdx;

        }

        private void AdjustColumn(int column)
        {
            if (VisibleColumns == null || VisibleColumns > tableView.Columns)
                return;

            int endRow = tableView.EndIndex == null ? tableView.Rows - 1 : tableView.EndIndex.Value.Row;
            int endColumn = tableView.EndIndex == null ? tableView.Columns - 1 : tableView.EndIndex.Value.Column;

            int startRow = tableView.StartIndex == null ? tableView.Rows - 1 : tableView.StartIndex.Value.Row;
            int startColumn = tableView.StartIndex == null ? tableView.Columns - 1 : tableView.StartIndex.Value.Column;


            if (column <= endColumn && column >= startColumn)
                return;

            var endIdx = new TableIndex(endRow, 0);
            var startIdx = new TableIndex(startColumn, 0);

            if (column > endColumn)
            {
                endIdx.Column = column;
                startIdx.Column = column - VisibleColumns.Value + 1;
            }
            else if (column < startColumn)
            {
                startIdx.Column = column;
                endIdx.Column = column + VisibleColumns.Value - 1;
            }

            tableView.StartIndex = startIdx;
            tableView.EndIndex = endIdx;
        }
    }
}