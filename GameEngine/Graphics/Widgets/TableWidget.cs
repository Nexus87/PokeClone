using GameEngine.Graphics.Views;
using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Diagnostics;

namespace GameEngine.Graphics.Widgets
{
    public class TableWidget<T> : AbstractGraphicComponent, IWidget
    {
        private bool isVisible;

        /// <summary>
        /// The current column of the selected cell
        /// </summary>
        private int cursorColumn = 0;
        /// <summary>
        /// The current row of the selected cell
        /// </summary>
        private int cursorRow = 0;

        private ITableView<T> tableView;

        public TableWidget(int? visibleRows, int? visibleColumns, ITableView<T> view, PokeEngine game)
            : base(game)
        {
            VisibleRows = visibleRows;
            VisibleColumns = visibleColumns;
            tableView = view;

            SetStartCell(0, 0);

            view.OnTableResize += TableResizeHandler;
        }

        private void TableResizeHandler(object sender, TableResizeEventArgs e)
        {
            throw new NotImplementedException();
            // First fix the cursor
            if (cursorColumn >= e.Columns)
                cursorColumn = e.Columns - 1;
            if (cursorRow >= e.Rows)
                cursorRow = e.Rows - 1;

            // If the cursor has changed, we also should change the selection
            tableView.SetCellSelection(cursorRow, cursorColumn, true);

            int visibleRows = (EndIndex.Row - StartIndex.Row) + 1;
            int visibleColumns = (EndIndex.Column - StartIndex.Column) + 1;
            
            // If we still display the right number of rows/columns, nothing needs to be done.
            if (visibleRows == realVisibleRows && VisibleColumns == realVisibleColumns)
                return;

            if (visibleRows != realVisibleRows)
            {
                // If we have enought rows from the start, we can use the current start index
                if (StartIndex.Row + realVisibleRows < Rows)
                {
                    SetStartCell(StartIndex.Row, StartIndex.Column);
                }
                else
                {
                    SetEndCell(EndIndex.Row, EndIndex.Column);
                }
            }

            // At this point, 
        }

        public TableWidget(PokeEngine game)
            : this(
            null, null,
            new TableView<T>(
                new DefaultTableModel<T>(),
                new DefaultTableRenderer<T>(game, DefaultCreator),
                new DefaultTableSelectionModel(),
                game),
            game)
        { }

        /// <summary>
        /// This event is called, if a cell is selected, pressing the Select key
        /// </summary>
        public event EventHandler<SelectionEventArgs<T>> ItemSelected = delegate { };

        /// <summary>
        /// This event is called, if the Back key is pressed
        /// </summary>
        public event EventHandler OnExitRequested = delegate { };

        public event EventHandler<VisibilityChangedEventArgs> OnVisibilityChanged = delegate { };

        /// <summary>
        /// Number of total columns in the table
        /// </summary>
        public int Columns { get { return tableView.Columns; } }

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

        public ITableModel<T> Model
        {
            get { return tableView.Model; }
            set { tableView.Model = value; }
        }

        /// <summary>
        /// Total number of rows in the table
        /// </summary>
        public int Rows { get { return tableView.Rows; } }

        public ITableView<T> TableView
        {
            get { return tableView; }
            private set
            {
                value.CheckNull("value");
                tableView = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The maximum number of columns that are displayed.
        /// null is equivalent to Columns
        /// </summary>
        public int? VisibleColumns { get; private set; }

        /// <summary>
        /// The maximum number of rows that are displayed.
        /// null is equivalent to Rows
        /// </summary>
        public int? VisibleRows { get; private set; }

        /// <summary>
        /// Property to easily access TableView's EndIndex property.
        /// </summary>
        /// <remarks> This property expects the EndIndex to be not null</remarks>
        private TableIndex EndIndex { get { return tableView.EndIndex.Value; } }

        /// <summary>
        /// Holds the number of visible columns, even if the VisibleColumns property is null.
        /// </summary>
        private int realVisibleColumns { get { return VisibleColumns.HasValue ? VisibleColumns.Value : tableView.Columns; } }

        /// <summary>
        /// Holds the number of visible rows, even if the VisibleRowss property is null.
        /// </summary>
        private int realVisibleRows { get { return VisibleRows.HasValue ? VisibleRows.Value : tableView.Rows; } }

        /// <summary>
        /// Property to easily access TableView's StartIndex property.
        /// </summary>
        /// <remarks> This property expects the StartIndex to be not null</remarks>
        private TableIndex StartIndex { get { return tableView.StartIndex.Value; } }

        /// <summary>
        /// This Method handles the Input.
        /// Pressing Select or Back key will trigger the corresponding events, which are
        /// ItemSelected and OnExitRequested. The ItemSelectedEventArgs will hold the data of the
        /// current selected cell.
        /// Pressing one of the direction keys will move the cursor in this same direction.
        /// This will also result in a selection of the cell. If the newly selected cell is
        /// outside of the current viewport, it will make one move, so that the cell is visible.
        /// No exception is thrown if the resulting position is not valid, for example moving
        /// left at position (0, 0). In this case, the handler will still return true.
        /// </summary>
        /// <param name="key">Input key</param>
        /// <returns>True if the key was handled</returns>
        public bool HandleInput(CommandKeys key)
        {
            switch (key)
            {
                case CommandKeys.Select:
                    ItemSelected(this, new SelectionEventArgs<T>(Model[cursorRow, cursorColumn]));
                    break;

                case CommandKeys.Back:
                    OnExitRequested(this, EventArgs.Empty);
                    break;

                case CommandKeys.Right:
                    SetCursorColumn(cursorColumn + 1);
                    break;

                case CommandKeys.Left:
                    SetCursorColumn(cursorColumn - 1);
                    break;

                case CommandKeys.Up:
                    SetCursorRow(cursorRow - 1);
                    break;

                case CommandKeys.Down:
                    SetCursorRow(cursorRow + 1);
                    break;

                default:
                    return false;
            }
            return true;
        }

        /// <summary>
        /// This method will select the cell with the given index (row, column)
        /// and set the cursor to it. It will also move the viewport. If the
        /// number of visible columns/rows allows it, the cell (row, column)
        /// will be at the top left of the table. Otherwise this cell will be as
        /// close as possible to the top left corner.
        /// </summary>
        /// <param name="row">Row to be selected</param>
        /// <param name="column">Column to be selected</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// If row/column is negative or greater than the number of rows/columns
        /// in the table
        /// </exception>
        public void SelectCell(int row, int column)
        {
            if (row >= Rows || column >= Columns || row < 0 || column < 0)
                throw new ArgumentOutOfRangeException("row/column out of bound");

            cursorRow = row;
            cursorColumn = column;

            tableView.SetCellSelection(row, column, true);

            // These indexes should already be set in TableWidget's constructor
            Debug.Assert(tableView.EndIndex.HasValue);
            Debug.Assert(tableView.StartIndex.HasValue);

            if (row > EndIndex.Row || row < StartIndex.Row ||
                column > EndIndex.Column || column < StartIndex.Column)
                SetStartCell(row, column);
        }

        public override void Setup(ContentManager content)
        {
            tableView.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            tableView.Draw(time, batch);
        }

        protected override void Update()
        {
            tableView.XPosition = XPosition;
            tableView.YPosition = YPosition;
            tableView.Width = Width;
            tableView.Height = Height;
        }

        /// <summary>
        /// Default creator function for the DefaultTableCellRenderer
        /// </summary>
        /// <returns>Created XNASpriteFont instance</returns>
        private static XNASpriteFont DefaultCreator()
        {
            return new XNASpriteFont();
        }

        private void SetCursorRow(int row)
        {
            // Only called from HandleInput. No reason to throw an exception
            if (row >= Rows || row < 0)
                return;

            // tableViews selection model will take care of deselecting other
            // cells
            tableView.SetCellSelection(row, cursorColumn, true);
            cursorRow = row;

            // We may have to adjust the viewport
            if (row > EndIndex.Row)
                SetEndCell(row, EndIndex.Column);
            else if (row < StartIndex.Row)
                SetStartCell(row, StartIndex.Column);
        }

        private void SetCursorColumn(int column)
        {
            // Only called from HandleInput. No reason to throw an exception
            if (column >= Columns || column < 0)
                return;

            // tableViews selection model will take care of deselecting other
            // cells
            tableView.SetCellSelection(cursorRow, column, true);
            cursorColumn = column;

            // We may have to adjust the viewport
            if (column > EndIndex.Column)
                SetEndCell(EndIndex.Row, column);
            else if (column < StartIndex.Column)
                SetStartCell(StartIndex.Row, column);
        }

        private void SetEndCell(int row, int column)
        {
            // As with SetStartCell we only take care, that the cell (row,
            // column) is visible and that we have the right number of visible
            // cells.

            int startRow = Math.Max(0, row - (realVisibleRows - 1));
            int startColumn = Math.Max(0, column - (realVisibleColumns - 1));

            int endRow = Math.Min(tableView.Rows - 1, startRow + realVisibleRows - 1);
            int endColumn = Math.Min(tableView.Columns - 1, startColumn + realVisibleColumns - 1);

            tableView.StartIndex = new TableIndex(startRow, startColumn);
            tableView.EndIndex = new TableIndex(endRow, endColumn);
        }

        private void SetStartCell(int row, int column)
        {
            // We don't really care, if the table starts with the cell in given
            // as parameters. It is more important, that (row, cell) is visible
            // and we show the number of rows/columns determined by
            // VisibleRows/Columns

            // Therefore we start with the end index
            int endRow = Math.Min(tableView.Rows - 1, row + realVisibleRows - 1);
            int endColumn = Math.Min(tableView.Columns - 1, column + realVisibleColumns - 1);

            // And go back to set the start index
            int startRow = Math.Max(0, endRow - (realVisibleRows - 1));
            int startColumn = Math.Max(0, endColumn - (realVisibleColumns - 1));

            tableView.StartIndex = new TableIndex(startRow, startColumn);
            tableView.EndIndex = new TableIndex(endRow, endColumn);
        }
    }
}