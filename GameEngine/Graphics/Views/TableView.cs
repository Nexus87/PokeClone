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
        private int ViewportStartColumn
        {
            get { return startColumn; }
            set
            {
                if (startColumn == value)
                    return;
                startColumn = value;
                Invalidate();
            }
        }
        private int ViewportStartRow
        {
            get { return startRow; }
            set
            {
                if (startRow == value)
                    return;
                startRow = value;
                Invalidate();
            }
        }
        public int VisibleColumns { get { return 8; } }
        public int VisibleRows { get { return 8; } }

        public InternalTableView(IItemModel<T> model)
        {
            this.model = model;
            model.DataChanged += model_DataChanged;
            model.SizeChanged += model_SizeChanged;
            items = new ItemBox[model.Rows, model.Columns];

            layout = new TableLayout(ViewportRows, ViewportColumns);
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

        public bool SetCellSelection(int row, int column, bool isSelected)
        {

            if (row >= model.Rows || column >= model.Columns)
                return false;

            // Can't select a non existing entry
            if (items[row, column] == null)
                return false;

            items[row, column].IsSelected = isSelected;
            return true;
        }

        public bool IsCellSelected(int row, int column)
        {
            if (row >= model.Rows || column >= model.Columns)
                return false;

            return items[row, column] != null && items[row, column].IsSelected;
        }

        public int ViewportRows { get {  return Math.Min(VisibleRows, model.Rows); } }
        public int ViewportColumns { get {  return Math.Min(VisibleColumns, model.Columns); } }

        public int Rows { get { return model.Rows; } }
        public int Columns { get { return model.Columns; } }

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
            // At the moment there is no way to shrink the TableLayout
            if (layout.Rows > ViewportRows || layout.Columns > ViewportColumns)
            {
                layout = new TableLayout(ViewportRows, ViewportColumns);
                layout.Init(this);
            }

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

        void model_SizeChanged(object sender, SizeChangedArgs e)
        {
            var newItems = new ItemBox[e.newRows, e.newColumns];
            items.Copy(newItems);
            items = newItems;
        }

    }
}
