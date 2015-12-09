using GameEngine.Graphics.Views;
using GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;

namespace GameEngine.Graphics.Views
{
    public class SelectionEventArgs<T> : EventArgs
    {
        public T SelectedData;
    }

    public class TableView<T> : InternalTableView<T, XNASpriteFont>
    {
        public TableView(IItemModel<T> model) : base(model) { }
    }

    public class InternalTableView<T, SpriteFontClass> : AbstractGraphicComponent where SpriteFontClass : ISpriteFont, new()
    {
        private ContentManager content;
        private ItemBox[,] items;
        private TableLayout layout;
        private IItemModel<T> model;
        private ItemBox selectedItem;

        private int startColumn = 0;
        private int startRow = 0;
        public int VisibleColumns { get { return 8; } }
        public int VisibleRows { get { return 8; } }

        public InternalTableView(IItemModel<T> model)
        {
            this.model = model;
            model.DataChanged += model_DataChanged;
            model.SizeChanged += model_SizeChanged;
            items = new ItemBox[model.Rows, model.Columns];
            layout = new TableLayout(Math.Min(model.Rows, VisibleRows), Math.Min(model.Columns, VisibleColumns));
            layout.Init(this);
        }

        private void model_DataChanged(object sender, DataChangedArgs<T> e)
        {
            var newData = e.newData;
        }

        public void SelectItem(int row, int column)
        {
            if (selectedItem != null)
                selectedItem.IsSelected = false;

            if (row >= model.Rows || column >= model.Columns)
                return;
            if (row >= items.GetLength(0) || column >= items.GetLength(1))
                return;

            var oldRow = startRow;
            var oldColumn = startColumn;

            // row needs to be between [startRow, startRow + layout.Rows[
            while (row >= startRow + layout.Rows)
                startRow++;
            while (row < startRow)
                startRow--;

            while (column >= startColumn + layout.Columns)
                startColumn++;
            while (column < startColumn)
                startColumn--;

            selectedItem = items[row, column];

            if (selectedItem != null)
                selectedItem.IsSelected = true;

            if (oldColumn != startColumn || oldRow != startRow)
                FillLayout();
        }

        public int SelectedRow
        {
            get { throw new NotImplementedException(); }
        }

        public int SelectedColumn
        {
            get { throw new NotImplementedException(); }
        }

        public int Rows
        {
            get
            {

                throw new NotImplementedException();
            }
        }

        public int Columns
        {
            get
            {

                throw new NotImplementedException();
            }
        }
        public override void Setup(ContentManager content)
        {
            this.content = content;

        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            layout.Draw(time, batch);
        }

        protected override void Update()
        {
            base.Update();
            InitTable();
            foreach (var item in items)
                item.Setup(content);

            layout.Setup(content);
            FillLayout();

        }

        private void FillLayout()
        {
            for (int i = 0; i < layout.Rows; i++)
            {
                for (int j = 0; j < layout.Columns; j++)
                    layout.SetComponent(i, j, items[startRow + i, startColumn + j]);
            }
        }

        private void InitTable()
        { 
            layout.Init(this);
        }

        void model_SizeChanged(object sender, SizeChangedArgs e)
        {
            var newItems = new ItemBox[e.newRows, e.newColumns];
            items.Copy(newItems);
            items = newItems;
        }

    }
}
