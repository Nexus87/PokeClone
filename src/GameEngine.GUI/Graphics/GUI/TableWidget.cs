using System;
using System.Diagnostics;
using GameEngine.Globals;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Graphics.TableView;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics.GUI
{
    public class TableWidget<T> : AbstractGraphicComponent, IWidget
    {
        /// <summary>
        /// The current column of the selected cell
        /// </summary>
        public int CursorColumn;

        /// <summary>
        /// The current row of the selected cell
        /// </summary>
        public int CursorRow;

        private readonly ITableView<T> _tableView;

        public TableWidget(int? visibleRows, int? visibleColumns, ITableModel<T> model, ITableRenderer<T> renderer, ITableSelectionModel selection) :
            this(visibleRows, visibleColumns, new TableView<T>(model, renderer, selection)) { }

        public TableWidget(int? visibleRows, int? visibleColumns, ITableView<T> view)
        {
            VisibleRows = visibleRows;
            VisibleColumns = visibleColumns;
            _tableView = view;

            view.TableResized += TableResizeHandler;
            SetStartCell(0, 0);
        }

        /// <summary>
        /// This event is called, if a cell is selected, pressing the Select key
        /// </summary>
        public event EventHandler<SelectionEventArgs<T>> ItemSelected = delegate { };

        /// <summary>
        /// This event is called, if the Back key is pressed
        /// </summary>
        public event EventHandler ExitRequested = delegate { };

        public ITableModel<T> Model => _tableView.Model;

        /// <summary>
        /// Number of total columns in the table
        /// </summary>
        private int Columns => _tableView.Columns;

        private int Rows => _tableView.Rows;

        /// <summary>
        /// The maximum number of columns that are displayed. null is equivalent
        /// to Columns
        /// </summary>
        private int? VisibleColumns { get; }

        /// <summary>
        /// The maximum number of rows that are displayed. null is equivalent to
        /// Rows
        /// </summary>
        private int? VisibleRows { get; }

        /// <summary>
        /// Property to easily access TableView's EndIndex property.
        /// </summary>
        /// <remarks>This property expects the EndIndex to be not null</remarks>
        private TableIndex EndIndex
        {
            get
            {
                Debug.Assert(_tableView.EndIndex != null, "_tableView.EndIndex != null");
                return _tableView.EndIndex.Value;
            }
        }

        /// <summary>
        /// Holds the number of visible columns, even if the VisibleColumns
        /// property is null.
        /// </summary>
        private int RealVisibleColumns => VisibleColumns ?? _tableView.Columns;

        /// <summary>
        /// Holds the number of visible rows, even if the VisibleRows property
        /// is null.
        /// </summary>
        private int RealVisibleRows => VisibleRows ?? _tableView.Rows;

        /// <summary>
        /// Property to easily access TableView's StartIndex property.
        /// </summary>
        /// <remarks>
        /// This property expects the StartIndex to be not null
        /// </remarks>
        private TableIndex StartIndex { get
        {
            Debug.Assert(_tableView.StartIndex != null, "_tableView.StartIndex != null");
            return _tableView.StartIndex.Value;
        } }

        /// <summary>
        /// This Method handles the Input. Pressing Select or Back key will
        /// trigger the corresponding events, which are ItemSelected and
        /// OnExitRequested. The ItemSelectedEventArgs will hold the data of the
        /// current selected cell. Pressing one of the direction keys will move
        /// the cursor in this same direction. This will also result in a
        /// selection of the cell. If the newly selected cell is outside of the
        /// current viewport, it will make one move, so that the cell is
        /// visible. No exception is thrown if the resulting position is not
        /// valid, for example moving left at position (0, 0). In this case, the
        /// handler will still return true.
        /// </summary>
        /// <param name="key">Input key</param>
        /// <returns>True if the key was handled</returns>
        public override void HandleKeyInput(CommandKeys key)
        {
            switch (key)
            {
                case CommandKeys.Select:
                    ItemSelected(this, new SelectionEventArgs<T>(Model.DataAt(CursorRow, CursorColumn)));
                    break;

                case CommandKeys.Back:
                    ExitRequested(this, EventArgs.Empty);
                    break;

                case CommandKeys.Right:
                    SetCursorColumn(CursorColumn + 1);
                    break;

                case CommandKeys.Left:
                    SetCursorColumn(CursorColumn - 1);
                    break;

                case CommandKeys.Up:
                    SetCursorRow(CursorRow - 1);
                    break;

                case CommandKeys.Down:
                    SetCursorRow(CursorRow + 1);
                    break;
            }
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
                throw new ArgumentOutOfRangeException(nameof(row));

            if (!_tableView.SetCellSelection(row, column, true))
                return;

            CursorRow = row;
            CursorColumn = column;

            FixViewport();
        }

        public override void Setup()
        {
            _tableView.Setup();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _tableView.Draw(time, batch);
        }

        protected override void Update()
        {
            _tableView.SetCoordinates(this);
        }

        private void TableResizeHandler(object sender, TableResizeEventArgs e)
        {
            FixCursor();

            // If there is nothing to display, reset everything
            if (e.Rows == 0 || e.Columns == 0)
            {
                _tableView.StartIndex = _tableView.EndIndex = new TableIndex(0, 0);
                return;
            }

            // If the cursor has changed, we also should change the selection
            _tableView.SetCellSelection(CursorRow, CursorColumn, true);
            ResizeViewport();
        }

        private void ResizeViewport()
        {
            int newStartRow = StartIndex.Row;
            int newStartColumn = StartIndex.Column;

            if (RowOutOfBound(newStartRow))
                newStartRow = ZeroIfNegative(Rows - RealVisibleRows);
            if (ColumnOutOfBound(newStartColumn))
                newStartColumn = ZeroIfNegative(Columns - RealVisibleColumns);

            SetStartCell(newStartRow, newStartColumn);
        }

        private int ZeroIfNegative(int p)
        {
            return Math.Max(0, p);
        }

        private bool ColumnOutOfBound(int column)
        {
            return column >= Columns;
        }

        private bool RowOutOfBound(int row)
        {
            return row >= Rows;
        }

        private void FixCursor()
        {
            if (RowOutOfBound(CursorRow))
                CursorRow = ZeroIfNegative(Rows - 1);

            if (ColumnOutOfBound(CursorColumn))
                CursorColumn = ZeroIfNegative(Columns - 1);
        }

        private void FixViewport()
        {
            // These indexes should already be set in TableWidget's constructor
            Debug.Assert(_tableView.EndIndex.HasValue);
            Debug.Assert(_tableView.StartIndex.HasValue);

            if (CursorRow >= EndIndex.Row || CursorColumn >= EndIndex.Column)
                SetEndCell(CursorRow, CursorColumn);
            else if (CursorRow < StartIndex.Row || CursorColumn < StartIndex.Column)
                SetStartCell(CursorRow, CursorColumn);
        }

        private void SetCursorRow(int row)
        {
            SetCursor(row, CursorColumn);
        }

        private void SetCursorColumn(int column)
        {
            SetCursor(CursorRow, column);
        }

        private void SetCursor(int row, int column)
        {
            // Only called from HandleKeyInput. No reason to throw an exception
            if (column >= Columns || column < 0)
                return;
            if (row >= Rows || row < 0)
                return;

            SelectCell(row, column);
        }

        private void SetEndCell(int row, int column)
        {
            var endRow = row + 1;
            var endColumn = column + 1;

            var startRow = Math.Max(0, endRow - RealVisibleRows);
            var startColumn = Math.Max(0, endColumn - RealVisibleColumns);

            _tableView.StartIndex = new TableIndex(startRow, startColumn);
            _tableView.EndIndex = new TableIndex(endRow, endColumn);
        }

        private void SetStartCell(int row, int column)
        {
            var startRow = row;
            var startColumn = column;

            var endRow = Math.Min(Rows, startRow + RealVisibleRows);
            var endColumn = Math.Min(Columns, startColumn + RealVisibleColumns);

            _tableView.StartIndex = new TableIndex(startRow, startColumn);
            _tableView.EndIndex = new TableIndex(endRow, endColumn);
        }
    }
}