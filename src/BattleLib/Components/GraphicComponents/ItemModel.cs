using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using GameEngine.GUI.Graphics.TableView;

namespace BattleLib.Components.GraphicComponents
{
    public class ItemModel : ITableModel<Item>
    {
        private int oldRowCount;
        private readonly IReadOnlyList<Item> items;

        public event EventHandler<DataChangedEventArgs<Item>> DataChanged
        {
            add {  }
            remove {  }
        }

        public event EventHandler<TableResizeEventArgs> SizeChanged;

        public ItemModel(Client player)
        {
            items = player.Items;
            oldRowCount = items.Count;

            player.ItemUsed += ItemUsedHandler;
        }

        private void ItemUsedHandler(object sender, EventArgs e)
        {
            if (oldRowCount != items.Count)
            {
                oldRowCount = items.Count;
                SizeChanged(this, new TableResizeEventArgs(Rows, Columns));
            }


        }

        public int Rows
        {
            get { return items.Count(); }
        }

        public int Columns
        {
            get { return 1; }
        }

        public Item DataAt(int row, int column)
        {
            if (column != 1 || row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException();

            return items[row];
        }

        public string DataStringAt(int row, int column)
        {
            return DataAt(row, column).ToString();
        }

        public bool SetDataAt(Item data, int row, int column)
        {
            throw new InvalidOperationException("Model is read only");
        }
    }
}
