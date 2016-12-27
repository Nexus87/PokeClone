using System;
using System.Diagnostics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics.TableView
{
    public delegate IGraphicComponent TableCellFactory<in T>(int row, int column, T value);

    [GameType]
    public class TableView<T> : AbstractGraphicComponent, ITableView<T>
    {
        public TableCellFactory<T> Factory
        {
            get { return _factory; }
            set
            {
                if (_factory == value)
                    return;
                _factory = value;
                Invalidate();
            }
        }

        private readonly ITableSelectionModel _selectionModel;
        private readonly ITableGrid _tableGrid;
        private TableCellFactory<T> _factory;

        public TableView(ITableModel<T> model, ITableSelectionModel selectionModel, IGameTypeRegistry registry)
            : this(model, selectionModel, registry, new TableGrid())
        {
        }

        //grid argument is for testing purpose
        public TableView(ITableModel<T> model, ITableSelectionModel selectionModel, IGameTypeRegistry registry, ITableGrid grid)
        {
            model.CheckNull(nameof(model));
            selectionModel.CheckNull(nameof(selectionModel));

            _tableGrid = grid;
            _tableGrid.StartIndex = null;
            _tableGrid.EndIndex = null;

            Factory = (row, column, data) =>
            {
                var component = registry.ResolveType<TextBox>();
                component.Text = data.ToString();
                return component;
            };

            Model = model;
            _selectionModel = selectionModel;

            // We need to handle the SizeChanged event right from the start so that
            // we can keep tableGrid's Rows and Columns properties up to date.
            // Otherwise it can't do the validity checks for Start- and EndIndex.
            model.SizeChanged += SizeChangedHandler;
            // The other event handler need tableGrid filled with components

            ModelSizeChanged();
        }

        public event EventHandler<TableResizeEventArgs> TableResized = delegate { };

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged
        {
            add { _selectionModel.SelectionChanged += value; }
            remove { _selectionModel.SelectionChanged -= value; }
        }

        public int Columns => Model.Columns;
        public int Rows => Model.Rows;

        public ITableModel<T> Model { get; }

        public TableIndex? EndIndex
        {
            get { return _tableGrid.EndIndex; }
            set { _tableGrid.EndIndex = value; }
        }

        public TableIndex? StartIndex
        {
            get { return _tableGrid.StartIndex; }
            set { _tableGrid.StartIndex = value; }
        }

        private void CellIsInBound(int row, int column)
        {
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException(nameof(row), "value is: " + row);
            if (column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException(nameof(column), "value is: " + column);
        }

        private void ModelSizeChanged()
        {
            _tableGrid.Rows = Model.Rows;
            _tableGrid.Columns = Model.Columns;
            Invalidate();
        }

        public bool SetCellSelection(int row, int column, bool isSelected)
        {
            CellIsInBound(row, column);

            if (isSelected)
                return _selectionModel.SelectIndex(row, column);
            else
                return _selectionModel.UnselectIndex(row, column);
        }

        public override void Setup()
        {
            ModelSizeChanged();
            FillTableGrid();

            Model.DataChanged += DataChangedHandler;
            _selectionModel.SelectionChanged += SelectionChangedHandler;
        }

        private void FillTableGrid()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                    _tableGrid.SetComponentAt(i, j, GetComponent(i, j));
            }
        }

        protected override void Update()
        {
            _tableGrid.SetCoordinates(Area.X, Area.Y, Area.Width, Area.Height);
            FillTableGrid();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _tableGrid.Draw(time, batch);
        }

        private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            var component = _tableGrid.GetComponentAt(e.Row, e.Column);
            Debug.Assert(component != null);
            SetComponentSelection(component, e.IsSelected);
        }

        private static void SetComponentSelection(IGraphicComponent component, bool isSelected)
        {
            component.IsSelected = isSelected;
        }

        private void DataChangedHandler(object sender, DataChangedEventArgs<T> e)
        {
            _tableGrid.SetComponentAt(e.Row, e.Column, GetComponent(e.Row, e.Column, e.NewData));
        }

        private void SizeChangedHandler(object sender, TableResizeEventArgs e)
        {
            ModelSizeChanged();
            TableResized(this, e);
        }

        private IGraphicComponent GetComponent(int row, int column)
        {
            var data = Model.DataAt(row, column);
            return GetComponent(row, column, data);
        }

        private IGraphicComponent GetComponent(int row, int column, T data)
        {
            var isSelected = _selectionModel.IsSelected(row, column);
            var component = Factory(row, column, data);
            component.IsSelected = isSelected;
            return component;
        }
    }
}