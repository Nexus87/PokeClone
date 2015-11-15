using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Graphics
{
    public class SelectionEventArgs<T> : EventArgs
    {
        public T SelectedData;
    }

    public class TableView<T> : AbstractGraphicComponent, IWidget
    {
        public event EventHandler<SelectionEventArgs<T>> ItemSelected;

        public IItemModel<T> Model { get; set; }
        public ISelectionHandler Handler
        {
            set
            {
                if (handler != null)
                {
                    handler.ItemSelected -= handler_ItemSelected;
                    handler.SelectionChanged -= handler_SelectionChanged;
                }

                handler = value;
                if (handler == null)
                    return;

                handler.ItemSelected += handler_ItemSelected;
                handler.SelectionChanged += handler_SelectionChanged;
                handler_SelectionChanged(null, null);
            }
        }

        void handler_SelectionChanged(object sender, EventArgs e)
        {
            if (selectedItem != null)
                selectedItem.IsSelected = false;

            var newSelection = handler.SelectedIndex;
            var oldRow = startRow;
            var oldColumn = startColumn;

            // newSelection.Item1 needs to be between [startRow, startRow + layout.Rows[
            while(newSelection.Item1 >= startRow + layout.Rows)
                startRow++;
            while (newSelection.Item1 < startRow)
                startRow--;

            while (newSelection.Item2 >= startColumn + layout.Columns)
                startColumn++;
            while (newSelection.Item2 < startColumn)
                startColumn--;

            selectedItem = items[newSelection.Item1, newSelection.Item2];

            if (selectedItem != null)
                selectedItem.IsSelected = true;

            if (oldColumn != startColumn || oldRow != startRow)
                FillLayout();
        }

        void handler_ItemSelected(object sender, EventArgs e)
        {
            var selection = handler.SelectedIndex;
            var Item = Model.DataAt(selection.Item1, selection.Item2);

            if (ItemSelected != null)
                ItemSelected(this, new SelectionEventArgs<T> { SelectedData = Item });
        }

        private ISelectionHandler handler;
        private TableLayout layout;

        private int visibleRows = 8;
        private int visibleColumns = 8;

        private int startRow = 0;
        private int startColumn = 0;

        private ItemBox[,] items;
        private ItemBox selectedItem;

        public TableView(IItemModel<T> model) 
        {
            this.Model = model;
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

        public override void Setup(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            foreach (var item in items)
                item.Setup(content);
            
            layout.Setup(content);
            FillLayout();
        }

        protected override void DrawComponent(Microsoft.Xna.Framework.GameTime time, Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
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

        public void HandleInput(Keys key)
        {
            if (handler != null)
                handler.HandleInput(key);
        }
    }
}
