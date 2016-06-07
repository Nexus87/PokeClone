﻿using Base;
using GameEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleLib.GraphicComponents
{
    public class ItemModel : ITableModel<Item>
    {
        private int oldRowCount;
        private Client player;
        private IReadOnlyList<Item> items;

        public event EventHandler<DataChangedEventArgs<Item>> DataChanged;
        public event EventHandler<TableResizeEventArgs> SizeChanged;

        public ItemModel(Client player)
        {
            this.player = player;
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