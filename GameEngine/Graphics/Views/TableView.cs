using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework.Content;
using System;

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

    public class TableView<T> : ForwardingGraphicComponent<Container>, ITableView
    {
        private IItemModel<T> model;
        private ITableRenderer<T> renderer;
        private ITableSelectionModel selectionModel;

        public TableView(IItemModel<T> model, ITableRenderer<T> renderer, ITableSelectionModel selectionModel, PokeEngine game)
            : base(new Container(game), game)
        {
            renderer.CheckNull("renderer");
            model.CheckNull("model");
            game.CheckNull("game");
            selectionModel.CheckNull("selectionModel");

            this.renderer = renderer;
            this.selectionModel = selectionModel;
            SetModel(model);
            InnerComponent.Layout = new GridLayout(Rows, Columns);
        }

        

        public int Columns { get { return model.Columns; } }

        public IItemModel<T> Model
        {
            get { return model; }
            set
            {
                value.CheckNull("value");

                bool sizeChanged = value.Rows != model.Rows || value.Columns != model.Columns;

                SetModel(value);
                Invalidate();
            }
        }

        public int Rows { get { return model.Rows; } }


        public void SetCellSelection(int row, int column, bool isSelected)
        {
            throw new NotImplementedException();
        }

        public override void Setup(ContentManager content)
        {
            base.Setup(content);
        }

        protected override void Update()
        {
            FillLayout();
        }

        private void FillLayout()
        {
            var container = InnerComponent;
            container.RemoveAllComponents();

            throw new NotImplementedException();
        }

        private void model_DataChanged(object sender, DataChangedEventArgs<T> e)
        {
            Invalidate();
            throw new NotImplementedException();
        }

        private void model_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Invalidate();
            throw new NotImplementedException();
        }

        private void SetModel(IItemModel<T> value)
        {
            if (model != null)
            {
                model.DataChanged -= model_DataChanged;
                model.SizeChanged -= model_SizeChanged;
            }

            model = value;
            model.DataChanged += model_DataChanged;
            model.SizeChanged += model_SizeChanged;

            Invalidate();
        }

        public event EventHandler<TableResizeEventArgs> OnTableResize;

        int ITableView.Columns
        {
            get
            {
                return model.Columns;
            }
        }

        public TableIndex? StartIndex
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public TableIndex? EndIndex
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}