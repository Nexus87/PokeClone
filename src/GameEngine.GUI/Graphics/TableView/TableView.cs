using System;
using System.Diagnostics;
using GameEngine.Graphics.General;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.TableView
{
    [GameType]
    public class TableView<T> : AbstractGraphicComponent, ITableView<T>
    {
        private readonly ITableModel<T> model;
        private readonly ITableRenderer<T> renderer;
        private readonly ITableSelectionModel selectionModel;
        private readonly ITableGrid tableGrid;

        public TableView(ITableModel<T> model, ITableRenderer<T> renderer, ITableSelectionModel selectionModel)
            : this(model, renderer, selectionModel, new TableGrid())
        {}

        //grid argument is for testing purpose
        public TableView(ITableModel<T> model, ITableRenderer<T> renderer, ITableSelectionModel selectionModel, ITableGrid grid)
        {
            renderer.CheckNull("renderer");
            model.CheckNull("model");
            selectionModel.CheckNull("selectionModel");

            tableGrid = grid;
            tableGrid.StartIndex = null;
            tableGrid.EndIndex = null;

            this.renderer = renderer;
            this.model = model;
            this.selectionModel = selectionModel;
            
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
            add { selectionModel.SelectionChanged += value; }
            remove { selectionModel.SelectionChanged -= value; }
        }

        public int Columns { get { return model.Columns; } }
        public int Rows { get { return model.Rows; } }

        public ITableModel<T> Model { get { return model; } }
        public TableIndex? EndIndex
        {
            get { return tableGrid.EndIndex; }
            set { tableGrid.EndIndex = value; }
        }

        public TableIndex? StartIndex
        {
            get { return tableGrid.StartIndex; }
            set { tableGrid.StartIndex = value; }
        }

        private void CellIsInBound(int row, int column)
        {
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException("row", "value is: " + row);
            if (column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("column", "value is: " + column);
        }

        private void ModelSizeChanged()
        {
            tableGrid.Rows = model.Rows;
            tableGrid.Columns = model.Columns;
            Invalidate();
        }

        public bool SetCellSelection(int row, int column, bool isSelected)
        {
            CellIsInBound(row, column);

            if (isSelected)
                return selectionModel.SelectIndex(row, column);
            else
                return selectionModel.UnselectIndex(row, column);
        }

        public override void Setup()
        {
            ModelSizeChanged();
            FillTableGrid();

            model.DataChanged += DataChangedHandler;
            selectionModel.SelectionChanged += SelectionChangedHandler;
        }

        private void FillTableGrid()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    tableGrid.SetComponentAt(i, j, GetComponent(i, j));
            }
        }

        protected override void Update()
        {
            tableGrid.SetCoordinates(XPosition, YPosition, Width, Height);
            FillTableGrid();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            tableGrid.Draw(time, batch);
        }

        private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            var component = tableGrid.GetComponentAt(e.Row, e.Column);
            Debug.Assert(component != null);
            SetComponentSelection(component, e.IsSelected);
        }

        private static void SetComponentSelection(ISelectableGraphicComponent component, bool isSelected)
        {
            if (isSelected)
                component.Select();
            else
                component.Unselect();
        }

        private void DataChangedHandler(object sender, DataChangedEventArgs<T> e)
        {
            tableGrid.SetComponentAt(e.Row, e.Column, GetComponent(e.Row, e.Column, e.NewData));
        }

        private void SizeChangedHandler(object sender, TableResizeEventArgs e)
        {
            ModelSizeChanged();
            TableResized(this, e);
        }

        private ISelectableGraphicComponent GetComponent(int row, int column)
        {
            var data = model.DataAt(row, column);
            return GetComponent(row, column, data);
        }

        private ISelectableGraphicComponent GetComponent(int row, int column, T data)
        {
            var isSelected = selectionModel.IsSelected(row, column);
            return renderer.GetComponent(row, column, data, isSelected);
        }
    }
}