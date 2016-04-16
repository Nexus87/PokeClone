using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameEngine.Graphics.Views
{
    public class SelectionEventArgs<T> : EventArgs
    {
        public SelectionEventArgs(T selectedData)
        {
            SelectedData = selectedData;
        }

        public T SelectedData { get; private set; }
    }

    public class TableView<T> : AbstractGraphicComponent, ITableView<T>
    {
        private ITableModel<T> model;
        private ITableRenderer<T> renderer;
        private ITableSelectionModel selectionModel;
        private TableGrid tableGrid;

        private bool autoResizeEnd = true;
        private bool autoResizeStart = true;
        private bool isSetup = false;

        public TableView(ITableModel<T> model, ITableRenderer<T> renderer, ITableSelectionModel selectionModel, PokeEngine game)
            : base(game)
        {
            renderer.CheckNull("renderer");
            model.CheckNull("model");
            game.CheckNull("game");
            selectionModel.CheckNull("selectionModel");

            tableGrid = new TableGrid(model.Rows, model.Columns, game);
            tableGrid.StartIndex = null;
            tableGrid.EndIndex = null;

            this.renderer = renderer;
            this.model = model;
            this.selectionModel = selectionModel;
        }

        public event EventHandler<TableResizeEventArgs> OnTableResize = delegate { };
        public event EventHandler<SelectionChangedEventArgs> SelectionChanged
        {
            add { selectionModel.SelectionChanged += value; }
            remove { selectionModel.SelectionChanged -= value; }
        }

        public int Columns { get { return model.Columns; } }
        public int Rows { get { return model.Rows; } }

        public TableIndex? EndIndex
        {
            get { return tableGrid.EndIndex; }
            set
            {
                tableGrid.EndIndex = value;
            }
        }
        public TableIndex? StartIndex
        {
            get { return tableGrid.StartIndex; }
            set
            {
                tableGrid.StartIndex = value;
                    
            }
        }

        private void CellIsInBound(int row, int column)
        {
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException("row", "value is: " + row);
            if (column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("column", "value is: " + column);
        }

        public ITableModel<T> Model
        {
            get { return model; }
            set
            {
                value.CheckNull("value");
                SetModel(value);
            }
        }

        private void SetModel(ITableModel<T> value)
        {
            tableGrid.Rows = value.Rows;
            tableGrid.Columns = value.Columns;
            if (!isSetup)
            {
                model = value;
                return;
            }

            model.DataChanged -= model_DataChanged;
            model.SizeChanged -= model_SizeChanged;

            model = value;
            SubscribeToModelEvents();
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
            SubscribeToModelEvents();
            SubscribeToSelectionEvents();

            FillTableGrid();

            isSetup = true;
        }

        private void SubscribeToModelEvents()
        {
            model.DataChanged += model_DataChanged;
            model.SizeChanged += model_SizeChanged;
        }

        private void SubscribeToSelectionEvents()
        {
            selectionModel.SelectionChanged += SelectionChangedHandler;

        }

        private void FillTableGrid()
        {
            tableGrid.Rows = Rows;
            tableGrid.Columns = Columns;

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

        protected override void DrawComponent(GameTime time, Wrapper.ISpriteBatch batch)
        {
            tableGrid.Draw(time, batch);
        }


        private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            int row = e.Row;
            int column = e.Column;

            var component = tableGrid.GetComponentAt(row, column);

            Debug.Assert(component != null);

            if (e.IsSelected)
                component.Select();
            else
                component.Unselect();
        }

        private void model_DataChanged(object sender, DataChangedEventArgs<T> e)
        {
            tableGrid.SetComponentAt(e.Row, e.Column, GetComponent(e.Row, e.Column, e.NewData));
        }

        private void model_SizeChanged(object sender, TableResizeEventArgs e)
        {
            OnTableResize(this, e);
            tableGrid.Rows = e.Rows;
            tableGrid.Columns = e.Columns;
            Invalidate();
        }

        private ISelectableGraphicComponent GetComponent(int row, int column)
        {
            var data = model[row, column];
            return GetComponent(row, column, data);
        }

        private ISelectableGraphicComponent GetComponent(int row, int column, T data)
        {
            bool isSelected = selectionModel.IsSelected(row, column);
            return renderer.GetComponent(row, column, data, isSelected);
        }
    }
}