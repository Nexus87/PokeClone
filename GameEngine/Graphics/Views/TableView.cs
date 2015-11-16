using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Graphics
{
    public class SelectionEventArgs<T> : EventArgs
    {
        public T SelectedData;
    }

    public class TableView<T> : AbstractGraphicComponent
    {
        private ContentManager content;
        public IItemModel<T> Model
        {
            get { return model; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Model must not be null");
                if (model != null)
                {
                    model.SizeChanged -= model_SizeChanged;
                }
                model = value;
                model.SizeChanged += model_SizeChanged;
                model_SizeChanged(null, null);
            }
        }

        void model_SizeChanged(object sender, EventArgs e)
        {
            InitTable();
            // If the game is already running, content != null
            if (content != null)
                Setup(content);
        }

        private IItemModel<T> model;
        private TableLayout layout;

        private int visibleRows = 8;
        private int visibleColumns = 8;

        private int startRow = 0;
        private int startColumn = 0;

        private ItemBox[,] items;
        private ItemBox selectedItem;

        public void SelectItem(int row, int column)
        {
            if (selectedItem != null)
                selectedItem.IsSelected = false;

            if (row > Model.Rows || column > Model.Columns)
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

        public TableView(IItemModel<T> model) 
        {
            this.Model = model;
            InitTable();

        }

        private void InitTable()
        {
            layout = new TableLayout(Math.Min(model.Rows, visibleRows), Math.Min(model.Columns, visibleColumns));
            layout.Init(this);
            items = new ItemBox[model.Rows, model.Columns];
            InitItems();
        }

        private void InitItems()
        {
            for (int i = 0; i < Model.Rows; i++)
            {
                for (int j = 0; j < Model.Columns; j++)
                {
                    items[i, j] = new ItemBox(Model.DataStringAt(i, j));
                }
            }
        }

        public override void Setup(ContentManager content)
        {
            this.content = content;
            foreach (var item in items)
                item.Setup(content);
            
            layout.Setup(content);
            FillLayout();
        }

        protected override void DrawComponent(GameTime time, SpriteBatch batch)
        {
            layout.Draw(time, batch);
        }

        private void FillLayout()
        {
            for (int i = 0; i < layout.Rows; i++)
            {
                for (int j = 0; j < layout.Columns; j++)
                    layout.SetComponent(i, j, items[startRow + i, startColumn + j]);
            }
        }
    }
}
