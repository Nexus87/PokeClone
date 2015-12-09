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
            SelectedRow = 0;
            SelectedColumn = 0;
            items = new ItemBox[model.Rows, model.Columns];

            layout = new TableLayout(Rows, Columns);
            layout.Init(this);
        }

        private void model_DataChanged(object sender, DataChangedArgs<T> e)
        {
            if (items[e.row, e.column] == null)
            {
                var item = new ItemBox(new SpriteFontClass());
                if (content != null)
                    item.Setup(content);
                items[e.row, e.column] = item;
            }

            items[e.row, e.column].Text = model.DataStringAt(e.row, e.column);
        }

        public void SelectItem(int row, int column)
        {
            if (row == SelectedRow && column == SelectedColumn)
                return;

            if (row >= model.Rows || column >= model.Columns)
                return;

            // Can't select a non existing entry
            if (items[row, column] == null)
                return;

            SelectedRow = row;
            SelectedColumn = column;

            int oldRow = startRow;
            int oldColumn = startColumn;
            // row needs to be between [startRow, startRow + layout.Rows[
            while (row >= startRow + layout.Rows)
                startRow++;
            while (row < startRow)
                startRow--;

            while (column >= startColumn + layout.Columns)
                startColumn++;
            while (column < startColumn)
                startColumn--;

            if (oldRow != startRow || oldColumn != startColumn)
                Invalidate();
        }

        public int SelectedRow { get; private set; }
        public int SelectedColumn { get; private set; } 

        public int Rows { get {  return Math.Min(VisibleRows, model.Rows); } }
        public int Columns { get {  return Math.Min(VisibleColumns, model.Columns); } }

        public override void Setup(ContentManager content)
        {
            this.content = content;
            foreach (var item in items)
                if (item != null)
                    item.Setup(content);

        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            layout.Draw(time, batch);
        }

        protected override void Update()
        {
            SetSelectedItem();
            if (layout.Rows > Rows || layout.Columns > Columns)
            {
                layout = new TableLayout(Rows, Columns);
                layout.Init(this);
            }
            FillLayout();
        }

        private void SetSelectedItem()
        {
            if (SelectedColumn == -1)
                return;

            var item = items[SelectedRow, SelectedColumn];

            // should not happen since this is handled in SelectItem
            if (item == null)
                return;

            if (selectedItem != null)
                selectedItem.IsSelected = false;
            item.IsSelected = true;
            selectedItem = item;
        }

        private void FillLayout()
        {
            for (int i = 0; i < layout.Rows; i++)
            {
                for (int j = 0; j < layout.Columns; j++)
                    layout.SetComponent(i, j, items[startRow + i, startColumn + j]);
            }
        }

        void model_SizeChanged(object sender, SizeChangedArgs e)
        {
            var newItems = new ItemBox[e.newRows, e.newColumns];
            items.Copy(newItems);
            items = newItems;

            // It is not possible to 1 column but no row
            if (SelectedColumn == -1 && Columns != 0)
            {
                SelectedColumn = SelectedRow = 0;
            }
            if (Columns == 0)
            {
                SelectedColumn = SelectedRow = -1;
            }
        }

    }
}
